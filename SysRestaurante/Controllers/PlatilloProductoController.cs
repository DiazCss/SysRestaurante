using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.PlatilloProductoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;
using System;
using System.Threading.Tasks;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

    public class PlatilloProductoController : Controller
    {
        readonly IPlatilloProductoBL platilloProductoBL;
        readonly IPlatilloBL platilloBL;
        readonly IProductoBL productoBL;

        public PlatilloProductoController(IPlatilloProductoBL pPlatilloProductoBL, IPlatilloBL pPlatilloBL, IProductoBL pProductoBL)
        {
            platilloProductoBL = pPlatilloProductoBL;
            platilloBL = pPlatilloBL;
            productoBL = pProductoBL;

        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        // GET: PlatilloProductoController
        public async Task<IActionResult> Index(PlatilloProductoBuscarDTO pPlatilloProducto = null)
        {
            if (pPlatilloProducto == null)
                pPlatilloProducto = new PlatilloProductoBuscarDTO();
            if (pPlatilloProducto.Take == 0)
                pPlatilloProducto.Take = 10;
            var paginacion = await platilloProductoBL.BuscarAsync(pPlatilloProducto);
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

       
        PlatilloProductoMantDTO platilloProductoMantDTO = new PlatilloProductoMantDTO();

        try
        {
           
            var platillos = await platilloBL.ObtenerTodosAsync(); 
            ViewBag.Platillos = platillos;

            
            var productos = await productoBL.ObtenerTodosAsync(); 
            ViewBag.Productos = productos;

         
            if (pAccion.SiTraerDatos())
            {
                platilloProductoMantDTO = await platilloProductoBL.ObtenerPorIdAsync(new PlatilloProductoMantDTO { Id = id });
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
        }

        return View(platilloProductoMantDTO);
    }
    else
    {
        return RedirectToAction(nameof(Index));
    }
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]


        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(PlatilloProductoMantDTO pPlatilloProducto)
{
    try
    {
        int result = await platilloProductoBL.CreateAsync(pPlatilloProducto);

        TempData["Mensaje"] = "Platillo-Producto creado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear el Platillo-Producto: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }

    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(PlatilloProductoMantDTO pPlatilloProducto)
{
    try
    {
        int result = await platilloProductoBL.ModificarAsync(pPlatilloProducto);

        TempData["Mensaje"] = "Platillo-Producto editado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar el Platillo-Producto: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }

    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(PlatilloProductoMantDTO pPlatilloProducto)
{
    try
    {
        int result = await platilloProductoBL.EliminarAsync(pPlatilloProducto);

        TempData["Mensaje"] = "Platillo-Producto eliminado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar el Platillo-Producto: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }

    return RedirectToAction(nameof(Index));
}

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(PlatilloProductoMantDTO pPlatilloProducto)
        {
            return RedirectToAction(nameof(Mant), new { id = pPlatilloProducto.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }
    }
}
