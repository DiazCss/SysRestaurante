using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.CategoriaProductoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.Models;

namespace SysRestaurante.Controllers
{
    public class CategoriaProductoController : Controller
    {
        readonly ICategoriaProductoBL categoriaProductoBL;

        public CategoriaProductoController(ICategoriaProductoBL pCategoriaProductoBL)
        {
            categoriaProductoBL = pCategoriaProductoBL;
        }
        // GET: CategoriaProductoController
        public async Task<ActionResult> Index(CategoriaProductoBuscarDTO pCategoriaProducto = null)
        {
            if (pCategoriaProducto == null)
                pCategoriaProducto = new CategoriaProductoBuscarDTO();
            if (pCategoriaProducto.Take == 0)
                pCategoriaProducto.Take = 10;
            var paginacion = await categoriaProductoBL.BuscarAync(pCategoriaProducto);
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
                CategoriaProductoMantDTO pCategoriaProductoMantDTO = new CategoriaProductoMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        pCategoriaProductoMantDTO = await categoriaProductoBL.ObtenerPorIdAsync(new CategoriaProductoMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(pCategoriaProductoMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
        {
            int result = await categoriaProductoBL.CreateAsync(pCategoriaProductoMantDTO);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
        {
            int result = await categoriaProductoBL.ModificarAsync(pCategoriaProductoMantDTO);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
        {
            int result = await categoriaProductoBL.EliminarAsync(pCategoriaProductoMantDTO);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Detail(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
        {
            return RedirectToAction(nameof(Index), new { id = pCategoriaProductoMantDTO.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }
    }
}
