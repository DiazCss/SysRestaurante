using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.CompraDTOs;
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

            var detallecompra = new DetalleCompra
            {
                IdCompra = compra.Id,
                IdProducto = pCompraMantDTOs.IdProducto,
                PrecioUnitario = pCompraMantDTOs.PrecioUnitario,
                Cantidad = pCompraMantDTOs.Cantidad,
                SubTotal = pCompraMantDTOs.SubTotal,
            };
            dbContext.detallecompra.Add(detallecompra);
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
                SetDetalleCompra(compra.DetalleCompras, pCompraMantDTOs);
                dbContext.Update(compra);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }
        private void SetDetalleCompra(ICollection<DetalleCompra> detalleCompra, CompraManDTOs pCompraManDTOs)
        {
            if (detalleCompra != null)
            {
                foreach (var detalleCompras in detalleCompra)
                {
                    detalleCompras.IdCompra = pCompraManDTOs.IdCompra;
                    detalleCompras.IdProducto = pCompraManDTOs.IdProducto;
                    detalleCompras.PrecioUnitario = pCompraManDTOs.PrecioUnitario;
                    detalleCompras.Cantidad = pCompraManDTOs.Cantidad;
                    detalleCompras.SubTotal = pCompraManDTOs.SubTotal;
                }
            }
        }

        public async Task<CompraManDTOs> ObtenerPorIdAsync(CompraManDTOs pCompraMantDTOs)
        {
            // var compra = await dbContext.compras
            // .Include(e => e.DetalleCompras) 
            //     .FirstOrDefaultAsync(c => c.Id == pCompraMantDTOs.Id);

            // if (compra != null)
            // {
            //     return new CompraManDTOs
            //     {
            //         Id = compra.Id,
            //         NumeroFactura = compra.NumeroFactura,
            //         Iva = compra.Iva,
            //         Fecha = compra.Fecha,
            //         Total = compra.Total,
            //         IdProveedor = compra.IdProveedor,
            //         IdCompra = compra.DetalleCompras.IdCompra,
            //         IdProducto = compra.DetalleCompras.IdProducto,
            //         PrecioUnitario = compra.DetalleCompras.PrecioUnitario,
            //         Cantidad = compra.DetalleCompras.Cantidad,
            //         SubTotal = compra.DetalleCompras.SubTotal,
            //     };
            // }
            // else
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
                    detalleComprass = c.DetalleCompras.Select(d => new DetalleCompra{
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
