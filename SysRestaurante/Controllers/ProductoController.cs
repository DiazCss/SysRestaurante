using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.ProductoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using SysRestaurante.Models;
using System.Security.Claims;
using System;
using System.Threading.Tasks;

namespace SysRestaurante.Controllers
{
    public class ProductoController : Controller
    {
        readonly IProductoBL productoBL;

        public ProductoController(IProductoBL pProductoBL)
        {
            productoBL = pProductoBL;
        }

        // GET: ProductoController
        public async Task<IActionResult> Index(ProductoBuscarDTOs pProducto = null)
        {
            if (pProducto == null)
                pProducto = new ProductoBuscarDTOs();
            if (pProducto.Take == 0)
                pProducto.Take = 10;
            var paginacion = await productoBL.BuscarAsync(pProducto);
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
                ProductoManDTOs productoMantDTO = new ProductoManDTOs();
                if (pAccion.SiTraerDatos())
                {
                    try
                    {
                        productoMantDTO = await productoBL.ObtenerPorIdAsync(new ProductoManDTOs { Id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                }
                return View(productoMantDTO);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoManDTOs pProducto)
        {
            int result = await productoBL.CreateAsync(pProducto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductoManDTOs pProducto)
        {
            int result = await productoBL.ModificarAsync(pProducto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductoManDTOs pProducto)
        {
            int result = await productoBL.EliminarAsync(pProducto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detail(ProductoManDTOs pProducto)
        {
            return RedirectToAction(nameof(Mant), new { id = pProducto.Id, Accion = (int)ActionsUI_Enums.MODIFICAR });
        }

         [HttpPost]
        public async Task<ActionResult> ObtenerProducto(ProductoManDTOs pProducto)
        {
        var producto = await productoBL.ObtenerPorNombreAsync(new ProductoManDTOs { Codigo = pProducto.Codigo });

            return Json(producto);
        }
    }
}
