using System;
using SysRestaurante.EN;
using SysRestaurante.BL.DTOs.CategoriaProductoDTOs;
using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.Interfaces;


namespace SysRestaurante.DAL;

public class CategoriaProductoDAL : ICategoriaProductoBL
{
    readonly SysRestauranteDbContext dbContext;
    public CategoriaProductoDAL(SysRestauranteDbContext context) => dbContext = context;

        #region filtros de busqueda
        internal IQueryable<CategoriaProducto> QuerySelect(IQueryable<CategoriaProducto> pQuery, CategoriaProductoBuscarDTO pCategoriaProducto)
        {
            if (!string.IsNullOrWhiteSpace(pCategoriaProducto.Nombre_CategoriaProducto_Like))
            pQuery = pQuery.Where(c => c.Nombre.Contains(pCategoriaProducto.Nombre_CategoriaProducto_Like));

            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pCategoriaProducto.Take > 0)
                pQuery = pQuery.Skip(pCategoriaProducto.Skip).Take(pCategoriaProducto.Take).AsQueryable();
            return pQuery;
        }

        #endregion

    public async Task<int> CreateAsync(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
    {
        bool categoriaProductoExistente = await dbContext.categoriaProducto.AnyAsync(p => p.Nombre == pCategoriaProductoMantDTO.Nombre);
        if (categoriaProductoExistente)
        {
            return 0;
        }
        CategoriaProducto categoriaProducto = new CategoriaProducto()
        {
            Nombre = pCategoriaProductoMantDTO.Nombre,
            Descripcion = pCategoriaProductoMantDTO.Descripcion,
        };
        dbContext.Add(categoriaProducto);
        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> EliminarAsync(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
    {
        var categoriaProducto = await dbContext.categoriaProducto.Where(p => p.Id == pCategoriaProductoMantDTO.Id)
                        .FirstOrDefaultAsync();
        if (categoriaProducto != null && categoriaProducto.Id != 0)
        {
            dbContext.categoriaProducto.Remove(categoriaProducto);
            return await dbContext.SaveChangesAsync();
        }
        else
            return 0;
    }

    public async Task<int> ModificarAsync(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
    {
        var categoriaProductos = await dbContext.categoriaProducto.Where(p => p.Id == pCategoriaProductoMantDTO.Id)
                        .FirstOrDefaultAsync();
        if (categoriaProductos != null && categoriaProductos.Id != 0)
        {
            categoriaProductos.Nombre = pCategoriaProductoMantDTO.Nombre;
            categoriaProductos.Descripcion = pCategoriaProductoMantDTO.Descripcion;
            dbContext.Update(categoriaProductos);
            return await dbContext.SaveChangesAsync();
        }
        else
            return 0;
    }

    public async Task<CategoriaProductoMantDTO> ObtenerPorIdAsync(CategoriaProductoMantDTO pCategoriaProductoMantDTO)
    {
        var categoriaProductos = await dbContext.categoriaProducto.Where(p => p.Id == pCategoriaProductoMantDTO.Id)
                    .FirstOrDefaultAsync();
        if (categoriaProductos != null && categoriaProductos.Id != 0)
        {
            return new CategoriaProductoMantDTO
            {
                Id = categoriaProductos.Id,
                Nombre = categoriaProductos.Nombre,
                Descripcion = categoriaProductos.Descripcion,
            };
        }
        else
            return new CategoriaProductoMantDTO();
    }

    public async Task<List<CategoriaProductoMantDTO>> ObtenerTodosAsync()
    {
        var categoriaProductos = await dbContext.categoriaProducto.ToListAsync();
        if (categoriaProductos != null && categoriaProductos.Count > 0)
        {
            var List = new List<CategoriaProductoMantDTO>();
            categoriaProductos.ForEach(c => List.Add(new CategoriaProductoMantDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
            }));
            return List;
        }
        else
            return new List<CategoriaProductoMantDTO>();
    }

    public async Task<PaginacionOutputDTO<List<CategoriaProductoMantDTO>>> BuscarAync(CategoriaProductoBuscarDTO pCategoriaProductoBuscarDTO)
    {
        var result = new PaginacionOutputDTO<List<CategoriaProductoMantDTO>>();
        result.Data = new List<CategoriaProductoMantDTO>();
        var select = dbContext.categoriaProducto.AsQueryable();
        select = QuerySelect(select, pCategoriaProductoBuscarDTO);
        var proveedor = await select.ToListAsync();
        if (proveedor.Count > 0)
        {
            if (pCategoriaProductoBuscarDTO.IsCount)
            {
                pCategoriaProductoBuscarDTO.Take = 0;
                var selectCount = dbContext.categoriaProducto.AsQueryable();
                result.Count = await QuerySelect(selectCount, pCategoriaProductoBuscarDTO).CountAsync();
            }
            proveedor.ForEach(c => result.Data.Add(new CategoriaProductoMantDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            }));
        }
        return result;
    }
}
