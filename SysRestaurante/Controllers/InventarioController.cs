using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.InventarioDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.Models;

namespace SysRestaurante.Controllers
{
    public class InventarioController : Controller
    {
        readonly IInventarioBL inventarioBL;
        readonly IProductoBL productoBL;

        public InventarioController(IInventarioBL pInventarioBL, IProductoBL pProductoBL)
        {
            inventarioBL = pInventarioBL;
            productoBL = pProductoBL;
        }
        // GET: InventarioController
        public async Task<ActionResult> Index(InventarioBuscarDTO pInventario = null)
        {
            if (pInventario == null)
                pInventario = new InventarioBuscarDTO();
            if (pInventario.Take == 0)
                pInventario.Take = 10;
            var paginacion = await inventarioBL.BuscarAsync(pInventario);
            ViewBag.productos = await productoBL.ObtenerTodosAsync();
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
                ViewBag.producto = await productoBL.ObtenerTodosAsync();
                ViewBag.ActionsUI = pAccion;
                ViewBag.Error = "";
                InventarioMantDTO inventarioMantDTO = new InventarioMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        inventarioMantDTO = await inventarioBL.ObtenerPorIdAsync(new InventarioMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(inventarioMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Create(InventarioMantDTO pInventario)
{
    try
    {
        int result = await inventarioBL.CreateAsync(pInventario);
        TempData["Mensaje"] = "Inventario creado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear el inventario: {ex.Message}";
        TempData["TipoMensaje"] = "error"; 
    }
    return RedirectToAction(nameof(Index));
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Edit(InventarioMantDTO pInventario)
{
    try
    {
        int result = await inventarioBL.ModificarAsync(pInventario);
        TempData["Mensaje"] = "Inventario editado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar el inventario: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Delete(InventarioMantDTO pInventario)
{
    try
    {
        int result = await inventarioBL.EliminarAsync(pInventario);
        TempData["Mensaje"] = "Inventario eliminado exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar el inventario: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Detail(InventarioMantDTO pInventario)
        {
            return RedirectToAction(nameof(Mant), new { id = pInventario.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }

    }
}