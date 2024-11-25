using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.CategoriaProductoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.Models;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

    public class CategoriaProductoController : Controller
    {

        readonly ICategoriaProductoBL categoriaProductoBL;

        public CategoriaProductoController(ICategoriaProductoBL pCategoriaProductoBL)
        {
            categoriaProductoBL = pCategoriaProductoBL;
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

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
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

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
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Create(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
{
    try
    {
        int result = await categoriaProductoBL.CreateAsync(pCategoriaProductoMantDTO);
        TempData["Mensaje"] = "Categoría de producto creada exitosamente.";
        TempData["TipoMensaje"] = "success"; 
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear la categoría de producto: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Edit(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
{
    try
    {
        int result = await categoriaProductoBL.ModificarAsync(pCategoriaProductoMantDTO);
        TempData["Mensaje"] = "Categoría de producto editada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar la categoría de producto: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Delete(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
{
    try
    {
        int result = await categoriaProductoBL.EliminarAsync(pCategoriaProductoMantDTO);
        TempData["Mensaje"] = "Categoría de producto eliminada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar la categoría de producto: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Detail(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
{
    return RedirectToAction(nameof(Mant), new { id = pCategoriaProductoMantDTO.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
}

    }
}
