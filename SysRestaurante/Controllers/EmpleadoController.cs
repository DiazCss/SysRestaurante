using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.EmpleadoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;
using System.Security.Claims;

namespace SysRestaurante.Controllers
{
    public class EmpleadoController : Controller
    {
        readonly IEmpleadoBL empleadoBL;

        public EmpleadoController(IEmpleadoBL pEmpleadoBL)
        {
            empleadoBL = pEmpleadoBL;
        }
        // GET: EmpleadoController
        public async Task<IActionResult> Index(EmpleadoBuscarDTO pEmpleado = null)
        {
            if (pEmpleado == null)
                pEmpleado = new EmpleadoBuscarDTO();
            if (pEmpleado.Take == 0)
                pEmpleado.Take = 10;
            var paginacion = await empleadoBL.BuscarAsync(pEmpleado);
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
                EmpleadoMantDTO empleadoMantDTO = new EmpleadoMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        empleadoMantDTO = await empleadoBL.ObtenerPorIdAsync(new EmpleadoMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(empleadoMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpleadoMantDTO pEmpleado)
        {
            int result = await empleadoBL.CreateAsync(pEmpleado);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmpleadoMantDTO pEmpleado)
        {
            int result = await empleadoBL.ModificarAsync(pEmpleado);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmpleadoMantDTO pEmpleado)
        {
            int result = await empleadoBL.EliminarAsync(pEmpleado);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(EmpleadoMantDTO pEmpleado)
        {
            return RedirectToAction(nameof(Mant), new { id = pEmpleado.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }
    }
}
