using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.PlatilloImagenDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;

namespace SysRestaurante.Controllers
{
    public class PlatilloImagenController : Controller
    {
        private readonly IPlatilloImagenBL platilloImagenBL;
        private readonly string imageFolderPath = "wwwroot/images/platillos"; 

        public PlatilloImagenController(IPlatilloImagenBL pPlatilloImagenBL)
        {
            platilloImagenBL = pPlatilloImagenBL;
            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath); 
            }
        }

        // GET: PlatilloImagenController
    

public async Task<IActionResult> Index(PlatilloImagenBuscarDTO pPlatilloImagen = null, int? page = 1)
{
    int pageSize = 3; // Número de registros por página
    int pageNumber = page ?? 1;

    if (pPlatilloImagen == null)
        pPlatilloImagen = new PlatilloImagenBuscarDTO();

    var query = platilloImagenBL.ObtenerTodosAsync(); 

    // Aplicar paginación
    var paginacion = (await query).ToPagedList(pageNumber, pageSize);

    return View(paginacion);
}


        public async Task<IActionResult> Mant(int id, ActionsUI pAccion)
        {
            if (pAccion.EsValidAction())
            {
                ViewBag.ActionsUI = pAccion;
                ViewBag.Error = "";
                PlatilloImagenMantDTO platilloImagenMantDTO = new PlatilloImagenMantDTO();

                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        platilloImagenMantDTO = await platilloImagenBL.ObtenerPorIdAsync(new PlatilloImagenMantDTO { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                }
                return View(platilloImagenMantDTO);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(PlatilloImagenMantDTO pPlatilloImagen, IFormFile imagenArchivo)
{
    if (imagenArchivo != null && imagenArchivo.Length > 0)
    {
       
        var imageName = $"{pPlatilloImagen.IdPlatillo}_{Path.GetRandomFileName()}{Path.GetExtension(imagenArchivo.FileName)}";

        
        var imagePath = Path.Combine("wwwroot/images/platillos", imageName);

        
        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await imagenArchivo.CopyToAsync(stream);
        }

        
        pPlatilloImagen.ImagenPlatillo = Path.Combine("images/platillos", imageName).Replace("\\", "/");
    }

   
    int result = await platilloImagenBL.CreateAsync(pPlatilloImagen, pPlatilloImagen.ImagenPlatillo);
    return RedirectToAction(nameof(Index));
}


      [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(PlatilloImagenMantDTO pPlatilloImagen, IFormFile imagenArchivo)
{
    if (imagenArchivo != null && imagenArchivo.Length > 0)
    {
        
        var imageName = $"{pPlatilloImagen.IdPlatillo}_{Path.GetRandomFileName()}{Path.GetExtension(imagenArchivo.FileName)}";
        var imagePath = Path.Combine("wwwroot/images/platillos", imageName);

        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await imagenArchivo.CopyToAsync(stream);
        }

        
        pPlatilloImagen.ImagenPlatillo = Path.Combine("images/platillos", imageName).Replace("\\", "/");
    }

    int result = await platilloImagenBL.ModificarAsync(pPlatilloImagen);
    return RedirectToAction(nameof(Index));
}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PlatilloImagenMantDTO pPlatilloImagen)
        {
            var platilloImagenDTO = await platilloImagenBL.ObtenerPorIdAsync(pPlatilloImagen);

            if (platilloImagenDTO != null && !string.IsNullOrEmpty(platilloImagenDTO.ImagenPlatillo) && System.IO.File.Exists(platilloImagenDTO.ImagenPlatillo))
            {
               
                System.IO.File.Delete(platilloImagenDTO.ImagenPlatillo);
            }

            int result = await platilloImagenBL.EliminarAsync(pPlatilloImagen);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(PlatilloImagenMantDTO pPlatilloImagen)
        {
            return RedirectToAction(nameof(Mant), new { id = pPlatilloImagen.Id, pAccion = ActionsUI_Enums.VER });
        }
    }
}
