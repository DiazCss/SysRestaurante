using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.EmpleadoDTOs;
using SysRestaurante.BL.DTOs.ProveedorDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;

namespace SysRestaurante.Controllers
{
    public class ProveedorController : Controller
    {
        readonly IProveedorBL proveedorBL;

        public ProveedorController(IProveedorBL pProveedorBL)
        {
            proveedorBL = pProveedorBL;
        }
        // GET: ProveedorController
        public async Task<ActionResult> Index(ProveedorBuscarDTO pProveedor = null)
        {
            if (pProveedor == null)
                pProveedor = new ProveedorBuscarDTO();
            if (pProveedor.Take == 0)
                pProveedor.Take = 10;
            var paginacion = await proveedorBL.BuscarAync(pProveedor);
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
                ProveedorMantDTO proveedorMantDTO = new ProveedorMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        proveedorMantDTO = await proveedorBL.ObtenerPorIdAsync(new ProveedorMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(proveedorMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProveedorMantDTO pProveedor)
        {
            int result = await proveedorBL.CreateAsync(pProveedor);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProveedorMantDTO pProveedor)
        {
            int result = await proveedorBL.ModificarAsync(pProveedor);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ProveedorMantDTO pProveedor)
        {
            int result = await proveedorBL.EliminarAsync(pProveedor);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Detail(ProveedorMantDTO pProveedor)
        {
            return RedirectToAction(nameof(Index), new { id = pProveedor.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }
    }
}