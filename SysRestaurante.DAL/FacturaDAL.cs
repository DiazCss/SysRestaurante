using SysRestaurante.BL.DTOs.FacturaDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.DAL
{
    public class FacturaDAL : IFacturaBL
    {
        private readonly SysRestauranteDbContext _context;

        public FacturaDAL(SysRestauranteDbContext context)
        {
            _context = context;
        }

        public async Task CrearFacturaAsync(FacturaDTO facturaDTO)
        {
            var factura = new Factura
            {
                FechaEmision = facturaDTO.FechaEmision,
                MetodoDePago = facturaDTO.MetodoDePago,
                NumeroFactura = facturaDTO.NumeroFactura,
                Total = facturaDTO.Total
            };

            _context.factura.Add(factura);
            await _context.SaveChangesAsync();

            foreach (var detalle in facturaDTO.DetallesFactura)
            {
                var detalleFactura = new DetalleFactura
                {
                    IdFactura = factura.Id,
                    IdPlatillo = detalle.IdPlatillo,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    Subtotal = detalle.Subtotal
                };
                _context.detalleFactura.Add(detalleFactura);
            }

            await _context.SaveChangesAsync();
        }

    }

}
