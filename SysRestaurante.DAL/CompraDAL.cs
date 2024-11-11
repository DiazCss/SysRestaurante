using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.CompraDTOs;
using SysRestaurante.BL.DTOs.ProductoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;

namespace SysRestaurante.DAL
{
    public class CompraDAL : IComprasBL 
    {
        readonly SysRestauranteDbContext dbContext;

        public CompraDAL(SysRestauranteDbContext context) => dbContext = context;

        #region filtros de busqueda
        internal IQueryable<Compra> QuerySelect(IQueryable<Compra> pQuery, CompraBuscarDTOs pCompra)
        {
            if (!string.IsNullOrWhiteSpace(pCompra.NumeroFactura_Compra_Like))
                pQuery = pQuery.Where(c => c.NumeroFactura.Contains(pCompra.NumeroFactura_Compra_Like));

            if (pCompra.Fecha_Compra_Like.HasValue)
                pQuery = pQuery.Where(c => c.Fecha.Date == pCompra.Fecha_Compra_Like.Value.Date);

            pQuery = pQuery.OrderByDescending(c => c.Id).AsQueryable();
            if (pCompra.Take > 0)
                pQuery = pQuery.Skip(pCompra.Skip).Take(pCompra.Take).AsQueryable();

            return pQuery;
        }
        #endregion

        public async Task<int> CreateAsync(CompraManDTOs pCompraMantDTOs)
        {
            var compra = new Compra
            {
                NumeroFactura = pCompraMantDTOs.NumeroFactura,
                Fecha = DateTime.Now,
                Iva = pCompraMantDTOs.Iva,
                Total = pCompraMantDTOs.Total,
                IdProveedor = pCompraMantDTOs.IdProveedor
            };
            dbContext.compras.Add(compra);
            await dbContext.SaveChangesAsync();


            foreach (var detalle in pCompraMantDTOs.DetalleCompras)
            {
                var detalleCompra = new DetalleCompra
                {
                    IdCompra = compra.Id, 
                    IdProducto = detalle.IdProducto,
                    PrecioUnitario = detalle.PrecioUnitario,
                    Cantidad = detalle.Cantidad,
                    SubTotal = detalle.PrecioUnitario * detalle.Cantidad
                };

                dbContext.detallecompra.Add(detalleCompra);
            }

            return await dbContext.SaveChangesAsync();

        }

        public async Task<int> EliminarAsync(CompraManDTOs pCompraMantDTOs)
        {
            var compra = await dbContext.compras
            .Include(e => e.DetalleCompras)
                .FirstOrDefaultAsync(c => c.Id == pCompraMantDTOs.Id);

            if (compra != null)
            {
                dbContext.compras.Remove(compra);
                dbContext.detallecompra.RemoveRange(compra.DetalleCompras);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(CompraManDTOs pCompraMantDTOs)
        {
            var compra = await dbContext.compras
            .Include(e => e.DetalleCompras)
                .FirstOrDefaultAsync(c => c.Id == pCompraMantDTOs.Id);

            if (compra != null)
            {
                compra.NumeroFactura = pCompraMantDTOs.NumeroFactura;
                compra.Iva = pCompraMantDTOs.Iva;
                compra.Fecha = pCompraMantDTOs.Fecha;
                compra.Total = pCompraMantDTOs.Total;
                compra.IdProveedor = pCompraMantDTOs.IdProveedor;
                SetDetalleCompra(compra);
                dbContext.Update(compra);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }
        private List<DetalleCompraDTO> SetDetalleCompra(Compra pCompra)
        {
            if (pCompra.DetalleCompras != null)
            {
                var list = new List<DetalleCompraDTO>();

                pCompra.DetalleCompras.ForEach(s =>
                {
                    list.Add(new DetalleCompraDTO
                    {
                        Id = s.Id,
                        IdCompra = s.IdCompra,
                        IdProducto = s.IdProducto,
                        SubTotal = s.SubTotal,
                        PrecioUnitario = s.PrecioUnitario,
                        Cantidad = s.Cantidad,
                        // Asegurarse de que la propiedad Producto está cargada y mapeada
                        Producto = s.Producto != null ? new Producto
                        {
                            Id = s.Producto.Id,
                            Codigo = s.Producto.Codigo,
                            Nombre = s.Producto.Nombre,
                            ContenidoEmpaque = s.Producto.ContenidoEmpaque
                        } : null
                    });
                });

                return list;
            }
            else
            {
                return new List<DetalleCompraDTO>();
            }
        }



        public async Task<CompraManDTOs> ObtenerPorIdAsync(CompraManDTOs pCompraMantDTOs)
        {
            var compra = await dbContext.compras
            .Include(e => e.DetalleCompras)  // Incluir los detalles de compra
            .ThenInclude(dc => dc.Producto)  // Incluir el producto relacionado a cada detalle
            .FirstOrDefaultAsync(c => c.Id == pCompraMantDTOs.Id);
            

            if (compra != null)
            {
                return new CompraManDTOs
                {
                    Id = compra.Id,
                    NumeroFactura = compra.NumeroFactura,
                    Iva = compra.Iva,
                    Fecha = compra.Fecha,
                    Total = compra.Total,
                    IdProveedor = compra.IdProveedor,
                    DetalleCompras = SetDetalleCompra(compra)
                };
            }
            else
                return new CompraManDTOs();
        }

        public async Task<List<CompraManDTOs>> ObtenerTodosAsync()
        {
            var compras = await dbContext.compras.Include(c => c.DetalleCompras).ToListAsync();
            if (compras != null && compras.Count > 0)
            {
                var list = new List<CompraManDTOs>();
                compras.ForEach(c => list.Add(new CompraManDTOs
                {
                    Id = c.Id,
                    NumeroFactura = c.NumeroFactura,
                    Iva = c.Iva,
                    Fecha = c.Fecha,
                    Total = c.Total,
                    IdProveedor = c.IdProveedor,
                    DetalleCompras = c.DetalleCompras.Select(d => new DetalleCompraDTO{
                        IdCompra = d.IdCompra,
                        IdProducto = d.IdProducto,
                        PrecioUnitario = d.PrecioUnitario,
                        Cantidad = d.Cantidad,
                        SubTotal = d.SubTotal,
                    }).ToList()
                }));
                return list;
            }
            else
                return new List<CompraManDTOs>(); // Retorno una lista vacía si no hay registros
        }

        public async Task<PaginacionOutputDTO<List<CompraManDTOs>>> BuscarAsync(CompraBuscarDTOs pCompraBuscarDTO)
        {
            var result = new PaginacionOutputDTO<List<CompraManDTOs>>();
            result.Data = new List<CompraManDTOs>();
            var select = dbContext.compras
            .Include(e => e.DetalleCompras).AsQueryable();

            select = QuerySelect(select, pCompraBuscarDTO);

            var compras = await select.ToListAsync();
            if (compras.Count > 0)
            {
                if (pCompraBuscarDTO.IsCount)
                {
                    pCompraBuscarDTO.Take = 0;
                    var selectCount = dbContext.compras.Include(e => e.DetalleCompras).AsQueryable();
                    result.Count = await QuerySelect(selectCount, pCompraBuscarDTO).CountAsync();
                }

                foreach (var c in compras)
                {
                    foreach (var s in c.DetalleCompras)
                    {
                        result.Data.Add(new CompraManDTOs{
                            Id = c.Id,
                            NumeroFactura = c.NumeroFactura,
                            Fecha = c.Fecha,
                            Iva = c.Iva,
                            Total = c.Total,
                            IdProveedor = c.IdProveedor,
                            IdCompra = s.IdCompra,
                            IdProducto = s.IdProducto,
                            PrecioUnitario = s.PrecioUnitario,
                            Cantidad = s.Cantidad,
                            SubTotal = s.SubTotal,

                        });
                    }
                }
            }
            return result;
        }

        // Corregido el método duplicado, eliminando el que no estaba implementado.
    }
}
