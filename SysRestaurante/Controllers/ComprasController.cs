using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.CompraDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;
using System.Security.Claims;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class CompraController : Controller
    {
        readonly IComprasBL compraBL;

        public CompraController(IComprasBL pCompraBL)
        {
            compraBL = pCompraBL;
        }

        // GET: CompraController
        public async Task<IActionResult> Index(CompraBuscarDTOs pCompra = null)
        {
            if (pCompra == null)
                pCompra = new CompraBuscarDTOs();
            if (pCompra.Take == 0)
                pCompra.Take = 10;
            var paginacion = await compraBL.BuscarAsync(pCompra);
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
                CompraManDTOs compraMantDTO = new CompraManDTOs();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        compraMantDTO = await compraBL.ObtenerPorIdAsync(new CompraManDTOs { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                }
                return View(compraMantDTO);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompraManDTOs pCompra)
        {
            int result = await compraBL.CreateAsync(pCompra);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompraManDTOs pCompra)
        {
            int result = await compraBL.ModificarAsync(pCompra);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CompraManDTOs pCompra)
        {
            int result = await compraBL.EliminarAsync(pCompra);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(CompraManDTOs pCompra)
        {
            return RedirectToAction(nameof(Mant), new { id = pCompra.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }
    }
}
