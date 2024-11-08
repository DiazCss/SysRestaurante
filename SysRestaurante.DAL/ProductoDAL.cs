using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.ProductoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysRestaurante.DAL
{
    internal class ProductoDAL : IProductoBL
    {
        private readonly SysRestauranteDbContext dbContext;

        public ProductoDAL(SysRestauranteDbContext context) => dbContext = context;

        #region Filtros de búsqueda
        internal IQueryable<Producto> QuerySelect(IQueryable<Producto> pQuery, ProductoBuscarDTOs pProducto)
        {
            if (!string.IsNullOrWhiteSpace(pProducto.Nombre_Producto_Like))
                pQuery = pQuery.Where(p => p.Nombre.Contains(pProducto.Nombre_Producto_Like));

            if (pProducto.IdCategoriaProducto_Compra_Like > 0)
                pQuery = pQuery.Where(p => p.IdCategoriaProducto == pProducto.IdCategoriaProducto_Compra_Like);

            pQuery = pQuery.OrderByDescending(p => p.Id);

            if (pProducto.Take > 0)
                pQuery = pQuery.Skip(pProducto.Skip).Take(pProducto.Take);

            return pQuery;
        }
        #endregion

        public async Task<int> CreateAsync(ProductoManDTOs pProductoManDTOs)
        {
            var producto = new Producto
            {
                Nombre = pProductoManDTOs.Nombre,
                ContenidoEmpaque = pProductoManDTOs.ContenidoEmpaque,
                IdCategoriaProducto = pProductoManDTOs.IdCategoriaProducto
            };
            dbContext.producto.Add(producto);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(ProductoManDTOs pProductoManDTOs)
        {
            var producto = await dbContext.producto.FirstOrDefaultAsync(p => p.Id == pProductoManDTOs.Id);
            if (producto != null)
            {
                dbContext.producto.Remove(producto);
                return await dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> ModificarAsync(ProductoManDTOs pProductoManDTOs)
        {
            var producto = await dbContext.producto.FirstOrDefaultAsync(p => p.Id == pProductoManDTOs.Id);
            if (producto != null)
            {
                producto.Nombre = pProductoManDTOs.Nombre;
                producto.ContenidoEmpaque = pProductoManDTOs.ContenidoEmpaque;
                producto.IdCategoriaProducto = pProductoManDTOs.IdCategoriaProducto;
                dbContext.Update(producto);

                return await dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductoManDTOs> ObtenerPorIdAsync(ProductoManDTOs pProductoManDTOs)
        {
            var producto = await dbContext.producto.FirstOrDefaultAsync(p => p.Id == pProductoManDTOs.Id);
            if (producto != null)
            {
                return new ProductoManDTOs
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    ContenidoEmpaque = producto.ContenidoEmpaque,
                    IdCategoriaProducto = producto.IdCategoriaProducto
                };
            }
            return new ProductoManDTOs();
        }

        public async Task<List<ProductoManDTOs>> ObtenerTodosAsync()
        {
            var productos = await dbContext.producto.ToListAsync();
            if (productos != null && productos.Count > 0)
            {
                var list = new List<ProductoManDTOs>();
                productos.ForEach(p => list.Add(new ProductoManDTOs
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    ContenidoEmpaque = p.ContenidoEmpaque,
                    IdCategoriaProducto = p.IdCategoriaProducto
                }));
                return list;
            }
            return new List<ProductoManDTOs>();
        }

        public async Task<PaginacionOutputDTO<List<ProductoManDTOs>>> BuscarAsync(ProductoBuscarDTOs pProductoBuscarDTO)
        {
            var result = new PaginacionOutputDTO<List<ProductoManDTOs>>
            {
                Data = new List<ProductoManDTOs>()
            };
            var select = dbContext.producto.AsQueryable();

            select = QuerySelect(select, pProductoBuscarDTO);

            var productos = await select.ToListAsync();
            if (productos.Count > 0)
            {
                if (pProductoBuscarDTO.IsCount)
                {
                    pProductoBuscarDTO.Take = 0;
                    var selectCount = dbContext.producto.AsQueryable();
                    result.Count = await QuerySelect(selectCount, pProductoBuscarDTO).CountAsync();
                }

                productos.ForEach(p => result.Data.Add(new ProductoManDTOs
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    ContenidoEmpaque = p.ContenidoEmpaque,
                    IdCategoriaProducto = p.IdCategoriaProducto
                }));
            }
            return result;
        }

    }
}
