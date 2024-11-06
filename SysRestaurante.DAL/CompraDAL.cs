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
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(CompraManDTOs pCompraMantDTOs)
        {
            var compra = await dbContext.compras
                .FirstOrDefaultAsync(c => c.Id == pCompraMantDTOs.Id);

            if (compra != null)
            {
                dbContext.compras.Remove(compra);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(CompraManDTOs pCompraMantDTOs)
        {
            var compra = await dbContext.compras
                .FirstOrDefaultAsync(c => c.Id == pCompraMantDTOs.Id);

            if (compra != null)
            {
                compra.NumeroFactura = pCompraMantDTOs.NumeroFactura;
                compra.Iva = pCompraMantDTOs.Iva;
                compra.Fecha = pCompraMantDTOs.Fecha;
                compra.Total = pCompraMantDTOs.Total;
                compra.IdProveedor = pCompraMantDTOs.IdProveedor;

                dbContext.Update(compra);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<CompraManDTOs> ObtenerPorIdAsync(CompraManDTOs pCompraMantDTOs)
        {
            var compra = await dbContext.compras
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
                    IdProveedor = compra.IdProveedor
                };
            }
            else
                return null;
        }

        public async Task<List<CompraManDTOs>> ObtenerTodosAsync()
        {
            var compras = await dbContext.compras.ToListAsync();
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
                    IdProveedor = c.IdProveedor
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
            var select = dbContext.compras.AsQueryable();

            select = QuerySelect(select, pCompraBuscarDTO);

            var compras = await select.ToListAsync();
            if (compras.Count > 0)
            {
                if (pCompraBuscarDTO.IsCount)
                {
                    pCompraBuscarDTO.Take = 0;
                    var selectCount = dbContext.compras.AsQueryable();
                    result.Count = await QuerySelect(selectCount, pCompraBuscarDTO).CountAsync();
                }

                compras.ForEach(c => result.Data.Add(new CompraManDTOs
                {
                    Id = c.Id,
                    NumeroFactura = c.NumeroFactura,
                    Fecha = c.Fecha,
                    Iva = c.Iva,
                    Total = c.Total,
                    IdProveedor = c.IdProveedor
                }));
            }
            return result;
        }

        public Task<PaginacionOutputDTO<List<CompraManDTOs>>> BuscarAsync(CompraManDTOs pCompraDTO)
        {
            throw new NotImplementedException();
        }

        // Corregido el método duplicado, eliminando el que no estaba implementado.
    }
}
