using Microsoft.AspNetCore.Mvc;
using SysRestaurante.BL.DTOs.PlatilloDTOs;
using SysRestaurante.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.BL.DTOs.PlatilloDTOs.SysRestaurante.BL.DTOs.PlatilloDTOs;
using SysRestaurante.BL.DTOs.FacturaDTOs;
using SysRestaurante.DAL;

namespace SysRestaurante.Controllers
{
    public class PedidoController : Controller
    {
        readonly IPlatilloBL platilloBL;
        readonly IFacturaBL facturaBL;


        public PedidoController(IPlatilloBL pPlatilloBL, IFacturaBL pFacturaBL)
        {
            platilloBL = pPlatilloBL;
            facturaBL = pFacturaBL;
        }
        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito( int id)
        {

            var platillo = await ObtenerPlatilloPorIdAsync(id);

            if (platillo == null)
                return NotFound();

            List<PlatilloIndexDTO> pedido = HttpContext.Session.GetObjectFromJson<List<PlatilloIndexDTO>>("Pedido") ?? new List<PlatilloIndexDTO>();

            pedido.Add(platillo);

            HttpContext.Session.SetObjectAsJson("Pedido", pedido);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult VerPedido()
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<PlatilloIndexDTO>>("Pedido") ?? new List<PlatilloIndexDTO>();
            return View(carrito);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarPedido()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Usuario");
            }

            var carrito = HttpContext.Session.GetObjectFromJson<List<PlatilloIndexDTO>>("Pedido") ?? new List<PlatilloIndexDTO>();

            if (carrito.Count == 0)
                return RedirectToAction("Index", "Home");

            // Crear la factura desde el carrito
            await GuardarFacturaAsync(carrito);

            // Limpiar el carrito después de confirmar el pedido
            HttpContext.Session.Remove("Pedido");

            return RedirectToAction("Index", "Home");
        }

        private async Task<PlatilloIndexDTO> ObtenerPlatilloPorIdAsync(int platilloId)
        {
            var platilloMantDTO = await platilloBL.ObtenerPorIdAsync(new PlatilloMantDTO { Id = platilloId });

            if (platilloMantDTO == null || platilloMantDTO.Id == 0)
                return null;

            return new PlatilloIndexDTO
            {
                Id = platilloMantDTO.Id,
                Nombre = platilloMantDTO.Nombre,
                Descripcion = platilloMantDTO.Descripcion,
                Precio = platilloMantDTO.Precio,
                Disponibilidad = platilloMantDTO.Disponibilidad == 0 ? "Disponible" : "No Disponible",
                Categoria = platilloMantDTO.IdCategoria.ToString() 
            };
        }

        private async Task GuardarFacturaAsync(List<PlatilloIndexDTO> carrito)
        {
            var clienteId = 1;

            var facturaDTO = new FacturaDTO
            {
                FechaEmision = DateTime.Now,
                MetodoDePago = "EFECTIVO", 
                NumeroFactura = "FAC" + DateTime.Now.Ticks.ToString(), 
                Total = carrito.Sum(p => p.Precio),
                DetallesFactura = carrito.Select(p => new DetalleFacturaDTO
                {
                    IdPlatillo = p.Id,
                    Cantidad = 1, 
                    PrecioUnitario = p.Precio,
                    Subtotal = p.Precio 
                }).ToList()
            };

            await facturaBL.CrearFacturaAsync(facturaDTO);
        }
    }
}
