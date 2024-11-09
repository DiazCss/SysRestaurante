using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.UsuarioDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.Models;
using SysRestaurante.BL.DTOs.RolDTOs;
using System.Security.Claims;

namespace SysRestaurante.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class UsuarioController : Controller
    {
        readonly IUsuarioBL usuarioBL;
        readonly IRolBL rolBL;
        readonly Credencial credencial;
        public UsuarioController(IUsuarioBL pUsuarioBL, IRolBL pRolBL, Credencial pCredencial)
        {
            credencial = pCredencial;
            usuarioBL = pUsuarioBL;
            rolBL = pRolBL;
        }

        public async Task<IActionResult> Index(UsuarioBuscarDTO pUsuario = null)
        {
            credencial.Refrescar(User);
            if (pUsuario == null)
                pUsuario = new UsuarioBuscarDTO();
            if (pUsuario.Take == 0)
                pUsuario.Take = 10;
            var paginacion = await usuarioBL.BuscarAsync(pUsuario);
            ViewBag.Roles = await rolBL.ObtenerTodosAsync();
            return View(paginacion.Data);
        }

        public async Task<IActionResult> Mant(int id, ActionsUI pAccion)
        {
            if (pAccion.EsValidAction())
            {
                ViewBag.Roles = await rolBL.ObtenerTodosAsync();
                ViewBag.ActionsUI = pAccion;
                ViewBag.Error = "";
                UsuarioMantDTO usuarioMantDTO = new UsuarioMantDTO();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        usuarioMantDTO = await usuarioBL.ObtenerPorIdAsync(new UsuarioMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }

                }
                return View(usuarioMantDTO);

            }
            else
                return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string? pReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = pReturnUrl;
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UsuarioLoginDTO pUsuario, string? pReturnUrl = null)
        {
            try
            {
                credencial.Refrescar(User);
                UsuarioMantDTO usuarioAut = await usuarioBL.LoginAsync(pUsuario);
                if (usuarioAut != null && usuarioAut.Email == pUsuario.Email)
                {
                    usuarioAut.Rol = await rolBL.ObtenerPorIdAsync(new RolMantDTO { Id = usuarioAut.IdRol });
                    usuarioAut.Token = usuarioAut.Token == null ? "" : usuarioAut.Token;
                    var claims = new[] {
                    new Claim(ClaimTypes.Name, usuarioAut.Email),
                    new Claim("Id", usuarioAut.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuarioAut.Rol.Nombre) ,
                        new Claim(ClaimTypes.GroupSid, usuarioAut.Token)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    var result = User.Identity.IsAuthenticated;
                    if (!string.IsNullOrWhiteSpace(pReturnUrl))
                        return Redirect(pReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    throw new Exception("Credenciales incorrectas o usuario inactivo");
            }
            catch (Exception ex)
            {

                ViewBag.Url = pReturnUrl;
                ViewBag.Error = ex.Message;
                return View(new UsuarioLoginDTO { Email = pUsuario.Email });
            }
        }

        public async Task<IActionResult> CambiarPassword()
        {
            credencial.Refrescar(User);
            var usuarioFind = await usuarioBL.BuscarAsync(new UsuarioBuscarDTO { Email_Usuario_Like = User.Identity.Name, Take = 1 });
            var usuarioActual = new UsuarioCambiarPasswordDTO();
            if (usuarioFind.Data != null)
            {
                var usuario = usuarioFind.Data.FirstOrDefault();
                usuarioActual.Id = usuario.Id;
            }
            ViewBag.Error = "";
            return View(usuarioActual);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarPassword(UsuarioCambiarPasswordDTO pUsuario)
        {
            credencial.Refrescar(User);
            try
            {
                int result = await usuarioBL.CambiarPasswordAsync(pUsuario);
                if (result > 0)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Login", "Usuario");
                }
                else
                {
                    var usuarioFind = await usuarioBL.BuscarAsync(new UsuarioBuscarDTO { Email_Usuario_Like = User.Identity.Name, Take = 1 });
                    var usuarioActual = new UsuarioCambiarPasswordDTO();
                    if (usuarioFind.Data != null)
                    {
                        var usuario = usuarioFind.Data.FirstOrDefault();
                        usuarioActual.Id = usuario.Id;
                    }
                    ViewBag.Error = "La contraseña actual no es correcta";
                    return View(usuarioActual);
                }
            }
            catch (Exception ex)
            {
                var usuarioFind = await usuarioBL.BuscarAsync(new UsuarioBuscarDTO { Email_Usuario_Like = User.Identity.Name, Take = 1 });
                var usuarioActual = new UsuarioCambiarPasswordDTO();
                if (usuarioFind.Data != null)
                {
                    var usuario = usuarioFind.Data.FirstOrDefault();
                    usuarioActual.Id = usuario.Id;
                }


                ViewBag.Error = ex.Message;
                return View(usuarioActual);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioMantDTO pUsuario)
        {
            credencial.Refrescar(User);
            int result = await usuarioBL.CrearAsync(pUsuario);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioMantDTO pUsuario)
        {
            credencial.Refrescar(User);
            int result = await usuarioBL.ModificarAsync(pUsuario);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UsuarioMantDTO pUsuario)
        {
            credencial.Refrescar(User);
            int result = await usuarioBL.EliminarAsync(pUsuario);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(UsuarioMantDTO pUsuario)
        {
            return RedirectToAction(nameof(Mant), new { id = pUsuario.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }



    }
}
