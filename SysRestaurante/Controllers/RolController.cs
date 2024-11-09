using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.RolDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.Models;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class RolController : Controller
    {
        // GET: RolController
        readonly IRolBL rolBL;
        public RolController(IRolBL pRolBL)
        {
            rolBL = pRolBL;
        }
        public async Task<IActionResult> Index(RolBuscarDTO pRol = null)
        {
            if (pRol == null)
                pRol = new RolBuscarDTO();
            if (pRol.Take == 0)
                pRol.Take = 10;
            var paginacion = await rolBL.BuscarAsync(pRol);
            return View(paginacion.Data);
        }
        public async Task<IActionResult> Mant(int id, ActionsUI pAccion)
        {
            if (pAccion.EsValidAction())
            {
                ViewBag.ActionsUI = pAccion;
                ViewBag.Error = "";
                RolMantDTO rolMantDTO = new RolMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        rolMantDTO = await rolBL.ObtenerPorIdAsync(new RolMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(rolMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }
        // Opcional si otra pantalla necesitara un select
        public async Task<IActionResult> Select()
        {
            var roles = await rolBL.ObtenerTodosAsync();
            return PartialView(roles);
        }

        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolMantDTO pRol)
        {
            int result = await rolBL.CreateAsync(pRol);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RolMantDTO pRol)
        {
            int result = await rolBL.ModificarAsync(pRol);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RolMantDTO pRol)
        {
            int result = await rolBL.EliminarAsync(pRol);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(RolMantDTO pRol)
        {
            return RedirectToAction(nameof(Mant), new { id = pRol.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }
    }
}
