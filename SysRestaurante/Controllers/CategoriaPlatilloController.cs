using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.CategoriaPlatilloDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CategoriaPlatilloController : Controller
    {
        readonly ICategoriaPlatilloBL categoriaPlatilloBL;

        public CategoriaPlatilloController(ICategoriaPlatilloBL pCategoriaPlatilloBL)
        {
            categoriaPlatilloBL = pCategoriaPlatilloBL;
        }

        // GET: CategoriaPlatilloController
        public async Task<IActionResult> Index(CategoriaPlatilloBuscarDTOs pCategoriaPlatillo = null)
        {
            if (pCategoriaPlatillo == null)
                pCategoriaPlatillo = new CategoriaPlatilloBuscarDTOs();
            if (pCategoriaPlatillo.Take == 0)
                pCategoriaPlatillo.Take = 10;

            var paginacion = await categoriaPlatilloBL.BuscarAsync(pCategoriaPlatillo);

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
                CategoriaPlatilloMantDTOs categoriaPlatilloMantDTO = new CategoriaPlatilloMantDTOs();

                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        categoriaPlatilloMantDTO = await categoriaPlatilloBL.ObtenerPorIdAsync(new CategoriaPlatilloMantDTOs { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                }

                return View(categoriaPlatilloMantDTO);
            }
            else
                return RedirectToAction(nameof(Index));
        }

       [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(CategoriaPlatilloMantDTOs pCategoriaPlatillo)
{
    try
    {
        int result = await categoriaPlatilloBL.CreateAsync(pCategoriaPlatillo);
        TempData["Mensaje"] = "Categoría creada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear la categoría: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(CategoriaPlatilloMantDTOs pCategoriaPlatillo)
{
    try
    {
        int result = await categoriaPlatilloBL.ModificarAsync(pCategoriaPlatillo);
        TempData["Mensaje"] = "Categoría editada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar la categoría: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(CategoriaPlatilloMantDTOs pCategoriaPlatillo)
{
    try
    {
        int result = await categoriaPlatilloBL.EliminarAsync(pCategoriaPlatillo);
        TempData["Mensaje"] = "Categoría eliminada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar la categoría: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
 [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(CategoriaPlatilloMantDTOs pCategoriaPlatillo)
        {
            return RedirectToAction(nameof(Mant), new { id = pCategoriaPlatillo.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }
    }
}
