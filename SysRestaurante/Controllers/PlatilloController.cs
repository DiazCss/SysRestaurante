using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.PlatilloDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;
using System;
using System.Threading.Tasks;

namespace SysRestaurante.Controllers
{
    public class PlatilloController : Controller
    {
        readonly IPlatilloBL platilloBL;
        readonly ICategoriaPlatilloBL categoriaPlatilloBL;
        public PlatilloController(IPlatilloBL pPlatilloBL, ICategoriaPlatilloBL pcategoriaPlatilloBL)
        {
            platilloBL = pPlatilloBL;
            categoriaPlatilloBL = pcategoriaPlatilloBL;
        }

        // GET: PlatilloController
        public async Task<IActionResult> Index(PlatilloBuscarDTO pPlatillo = null)
        {
            if (pPlatillo == null)
                pPlatillo = new PlatilloBuscarDTO();
            if (pPlatillo.Take == 0)
                pPlatillo.Take = 10;

            var paginacion = await platilloBL.BuscarAsync(pPlatillo);
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View(paginacion.Data);
        }

      public async Task<IActionResult> Mant(int id, ActionsUI pAccion)
{
    if (pAccion.EsValidAction())
    {
        ViewBag.ActionsUI = pAccion;
        ViewBag.Error = "";

   
        PlatilloMantDTO platilloMantDTO = new PlatilloMantDTO();

        try
        {
            var categorias = await categoriaPlatilloBL.ObtenerTodosAsync(); 
            ViewBag.Categorias = categorias;

           
            if (pAccion.SiTraerDatos())
            {
                platilloMantDTO = await platilloBL.ObtenerPorIdAsync(new PlatilloMantDTO { Id = id });
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
        }

        return View(platilloMantDTO);
    }
    else
    {
        return RedirectToAction(nameof(Index));
    }
}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlatilloMantDTO pPlatillo)
        {
            int result = await platilloBL.CreateAsync(pPlatillo);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlatilloMantDTO pPlatillo)
        {
            int result = await platilloBL.ModificarAsync(pPlatillo);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PlatilloMantDTO pPlatillo)
        {
            int result = await platilloBL.EliminarAsync(pPlatillo);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(PlatilloMantDTO pPlatillo)
        {
            return RedirectToAction(nameof(Mant), new { id = pPlatillo.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }
    }
}
