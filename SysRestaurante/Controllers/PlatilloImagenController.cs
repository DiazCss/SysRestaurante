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
          readonly IPlatilloBL platilloBL;
        private readonly IPlatilloImagenBL platilloImagenBL;
        private readonly string imageFolderPath = "wwwroot/images/platillos"; 

        public PlatilloImagenController(IPlatilloImagenBL pPlatilloImagenBL, IPlatilloBL pPlatilloBL)
        {
            platilloImagenBL = pPlatilloImagenBL;
            platilloBL = pPlatilloBL;
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

        try
        {
           
            var platillos = await platilloBL.ObtenerTodosAsync();
            ViewBag.Platillos = platillos;

            
            if (pAccion.SiTraerDatos())
            {
                platilloImagenMantDTO = await platilloImagenBL.ObtenerPorIdAsync(new PlatilloImagenMantDTO { Id = id });
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
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
    try
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

        TempData["Mensaje"] = "Imagen del platillo creada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al crear la imagen del platillo: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }

    return RedirectToAction(nameof(Index));
}


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(PlatilloImagenMantDTO pPlatilloImagen, IFormFile imagenArchivo)
{
    try
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

        TempData["Mensaje"] = "Imagen del platillo editada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al editar la imagen del platillo: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }

    return RedirectToAction(nameof(Index));
}


      [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(PlatilloImagenMantDTO pPlatilloImagen)
{
    try
    {
        var platilloImagenDTO = await platilloImagenBL.ObtenerPorIdAsync(pPlatilloImagen);

        if (platilloImagenDTO != null && !string.IsNullOrEmpty(platilloImagenDTO.ImagenPlatillo))
        {
            var imagePath = Path.Combine("wwwroot", platilloImagenDTO.ImagenPlatillo);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        int result = await platilloImagenBL.EliminarAsync(pPlatilloImagen);

        TempData["Mensaje"] = "Imagen del platillo eliminada exitosamente.";
        TempData["TipoMensaje"] = "success";
    }
    catch (Exception ex)
    {
        TempData["Mensaje"] = $"Error al eliminar la imagen del platillo: {ex.Message}";
        TempData["TipoMensaje"] = "error";
    }

    return RedirectToAction(nameof(Index));
}


        public IActionResult Detail(PlatilloImagenMantDTO pPlatilloImagen)
        {
            return RedirectToAction(nameof(Mant), new { id = pPlatilloImagen.Id, pAccion = ActionsUI_Enums.VER });
        }
    }
}
