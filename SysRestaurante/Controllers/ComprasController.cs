using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.CompraDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;
using System.Security.Claims;

namespace SysRestaurante.Controllers
{
    public class CompraController : Controller
    {
        readonly IComprasBL compraBL;
        readonly IProductoBL productoBL;
        readonly IProveedorBL proveedorBL;

        public CompraController(IComprasBL pCompraBL, IProductoBL pProductoBL, IProveedorBL pProveedorBL)
        {
            compraBL = pCompraBL;
            productoBL = pProductoBL;
            proveedorBL = pProveedorBL;
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

        try
        {
            
            var proveedores = await proveedorBL.ObtenerTodosAsync(); 
            ViewBag.Proveedores = proveedores;

           
            if (pAccion.SiTraerDatos())
            {
                compraMantDTO = await compraBL.ObtenerPorIdAsync(new CompraManDTOs { Id = id });
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
        }

        return View(compraMantDTO);
    }
    else
    {
        return RedirectToAction(nameof(Index));
    }
}


      [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(CompraManDTOs pCompra)
{
    try
    {
        int result = await compraBL.CreateAsync(pCompra);
        TempData["Mensaje"] = "Compra creada exitosamente.";
        TempData["TipoMensaje"] = "success"; 
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear la compra: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(CompraManDTOs pCompra)
{
    try
    {
        int result = await compraBL.ModificarAsync(pCompra);
        TempData["Mensaje"] = "Compra editada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar la compra: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(CompraManDTOs pCompra)
{
    try
    {
        int result = await compraBL.EliminarAsync(pCompra);
        TempData["Mensaje"] = "Compra eliminada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar la compra: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(CompraManDTOs pCompra)
        {
            return RedirectToAction(nameof(Mant), new { id = pCompra.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }

           public async Task<IActionResult>AutoCompleteProducto(string query)
        {
            var list = await productoBL.AutoCompleteProducto(query);
            return Json(list);
        }
    }
}
