using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.EmpleadoDTOs;
using SysRestaurante.BL.DTOs.MesaDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

    public class MesasController : Controller
    {
       readonly IMesasBL mesasbl;
        public MesasController(IMesasBL pMesaBL)
        {
            mesasbl = pMesaBL;
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        public async Task<IActionResult> Index(MesasBuscarDTO pMesa = null)
        {
            if (pMesa == null)
                pMesa = new MesasBuscarDTO();
            if (pMesa.Take == 0)
                pMesa.Take = 10;
            var paginacion = await mesasbl.BuscarAsync(pMesa);
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
                MesasMantDTO mesaMantDTO = new MesasMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        mesaMantDTO = await mesasbl.ObtenerPorIdAsync(new MesasMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(mesaMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(MesasMantDTO pMesa)
{
    try
    {
        int result = await mesasbl.CreateAsync(pMesa);
        TempData["Mensaje"] = "Mesa creada exitosamente.";
        TempData["TipoMensaje"] = "success"; 
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear la mesa: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(MesasMantDTO pMesa)
{
    try
    {
        int result = await mesasbl.ModificarAsync(pMesa);
        TempData["Mensaje"] = "Mesa editada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar la mesa: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(MesasMantDTO pMesa)
{
    try
    {
        int result = await mesasbl.EliminarAsync(pMesa);
        TempData["Mensaje"] = "Mesa eliminada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar la mesa: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }
    return RedirectToAction(nameof(Index));
}
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrador")]

        [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Detail(MesasMantDTO pMesa)
{
    return RedirectToAction(nameof(Mant), new { id = pMesa.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
}


    }
}
