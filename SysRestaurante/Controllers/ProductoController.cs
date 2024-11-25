using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.ProductoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

    public class ProductoController : Controller
    {
        readonly IProductoBL productoBL;
        readonly ICategoriaProductoBL categoriaProductoBL;

        public ProductoController(IProductoBL pProductoBL, ICategoriaProductoBL pCategoriaProductoBL)
        {
            productoBL = pProductoBL;
            categoriaProductoBL = pCategoriaProductoBL;
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        // GET: ProductoController
        public async Task<IActionResult> Index(ProductoBuscarDTOs pProducto = null)
        {
            if (pProducto == null)
                pProducto = new ProductoBuscarDTOs();
            if (pProducto.Take == 0)
                pProducto.Take = 10;
            var paginacion = await productoBL.BuscarAsync(pProducto);
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
        ProductoManDTOs productoMantDTO = new ProductoManDTOs();

        try
        {
          
            var categoriasProductos = await categoriaProductoBL.ObtenerTodosAsync();
            ViewBag.CategoriasProductos = categoriasProductos;

           
            if (pAccion.SiTraerDatos())
            {
                productoMantDTO = await productoBL.ObtenerPorIdAsync(new ProductoManDTOs { Id = id });
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
        }

        return View(productoMantDTO);
    }
    else
    {
        return RedirectToAction(nameof(Index));
    }
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]


        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(ProductoManDTOs pProducto)
{
    try
    {
        int result = await productoBL.CreateAsync(pProducto);
        TempData["Mensaje"] = "Producto creado exitosamente.";
        TempData["TipoMensaje"] = "success"; 
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear el producto: {ex.Message}";
        TempData["TipoMensaje"] = "error"; 
    }

    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(ProductoManDTOs pProducto)
{
    try
    {
        int result = await productoBL.ModificarAsync(pProducto);
        TempData["Mensaje"] = "Producto editado exitosamente.";
        TempData["TipoMensaje"] = "success"; 
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar el producto: {ex.Message}";
        TempData["TipoMensaje"] = "error"; 
    }

    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(ProductoManDTOs pProducto)
{
    try
    {
        int result = await productoBL.EliminarAsync(pProducto);
        TempData["Mensaje"] = "Producto eliminado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar el producto: {ex.Message}";
        TempData["TipoMensaje"] = "error"; 
    }

    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(ProductoManDTOs pProducto)
        {
            return RedirectToAction(nameof(Mant), new { id = pProducto.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }

         [HttpPost]
        public async Task<ActionResult> ObtenerProducto(ProductoManDTOs pProducto)
        {
        var producto = await productoBL.ObtenerPorNombreAsync(new ProductoManDTOs { Codigo = pProducto.Codigo });

            return Json(producto);
        }
    }
}
