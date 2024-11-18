using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.Models;
using System.Diagnostics;

namespace SysRestaurante.Controllers
{
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IPlatilloBL platilloBL;
        readonly ICategoriaPlatilloBL categoriaPlatilloBL;

        public HomeController(ILogger<HomeController> logger, IPlatilloBL pPlatilloBL, ICategoriaPlatilloBL pCategoriaPlatilloBL)
        {
            _logger = logger; 
            platilloBL = pPlatilloBL;
            categoriaPlatilloBL = pCategoriaPlatilloBL;
        }
        [AllowAnonymous]

        public async Task<IActionResult> Index()
        {
            var platillos = await platilloBL.ObtenerPlatillosIndexAsync();
            return View(platillos);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
