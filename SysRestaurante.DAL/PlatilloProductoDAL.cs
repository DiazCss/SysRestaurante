using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.PlatilloProductoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SysRestaurante.DAL
{
    internal class PlatilloProductoDAL : IPlatilloProductoBL
    {
        readonly SysRestauranteDbContext dbContext;
        public PlatilloProductoDAL(SysRestauranteDbContext context) => dbContext = context;

        #region filtros de busqueda
       internal IQueryable<PlatilloProducto> QuerySelect(IQueryable<PlatilloProducto> pQuery, PlatilloProductoBuscarDTO pPlatilloProducto)
{
    if (!string.IsNullOrWhiteSpace(pPlatilloProducto.NombrePlatillo_Like))
    {
        pQuery = pQuery.Where(s => s.Platillo.Nombre.Contains(pPlatilloProducto.NombrePlatillo_Like));
    }

    if (!string.IsNullOrWhiteSpace(pPlatilloProducto.NombreProducto_Like))
    {
        pQuery = pQuery.Where(s => s.Producto.Nombre.Contains(pPlatilloProducto.NombreProducto_Like));
    }

   
    if (pPlatilloProducto.IdPlatillo_Equal > 0)
    {
        pQuery = pQuery.Where(s => s.IdPlatillo == pPlatilloProducto.IdPlatillo_Equal);
    }

    if (pPlatilloProducto.IdProducto_Equal > 0)
    {
        pQuery = pQuery.Where(s => s.IdProducto == pPlatilloProducto.IdProducto_Equal);
    }

    pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
    
    if (pPlatilloProducto.Take > 0)
    {
        pQuery = pQuery.Skip(pPlatilloProducto.Skip).Take(pPlatilloProducto.Take).AsQueryable();
    }
    
    return pQuery;
}


        #endregion 

        public async Task<int> CreateAsync(PlatilloProductoMantDTO pPlatilloProductoMantDTO)
        {
            var platilloProducto = new PlatilloProducto
            {
                IdPlatillo = pPlatilloProductoMantDTO.IdPlatillo,
                IdProducto = pPlatilloProductoMantDTO.IdProducto,
                CantidadUsada = pPlatilloProductoMantDTO.CantidadUsada
            };
            dbContext.platilloProducto.Add(platilloProducto);
            return await dbContext.SaveChangesAsync(); 
        }

        public async Task<int> EliminarAsync(PlatilloProductoMantDTO pPlatilloProductoMantDTO)
        {
            var platilloProducto = await dbContext.platilloProducto
                .FirstOrDefaultAsync(pp => pp.Id == pPlatilloProductoMantDTO.Id);
            if (platilloProducto != null && platilloProducto.Id != 0)
            {
                dbContext.platilloProducto.Remove(platilloProducto);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(PlatilloProductoMantDTO pPlatilloProductoMantDTO)
        {
            var platilloProducto = await dbContext.platilloProducto
                .FirstOrDefaultAsync(s => s.Id == pPlatilloProductoMantDTO.Id);

            if (platilloProducto != null)
            {
                platilloProducto.IdPlatillo = pPlatilloProductoMantDTO.IdPlatillo;
                platilloProducto.IdProducto = pPlatilloProductoMantDTO.IdProducto;
                platilloProducto.CantidadUsada = pPlatilloProductoMantDTO.CantidadUsada;
                dbContext.Update(platilloProducto);

                return await dbContext.SaveChangesAsync();
            }
            else
            {
                return 0; 
            }
        }

     public async Task<PlatilloProductoMantDTO> ObtenerPorIdAsync(PlatilloProductoMantDTO pPlatilloProductoMantDTO)
{
    var platilloProducto = await dbContext.platilloProducto
        .Include(pp => pp.Platillo) 
                .Include(pp => pp.Producto)  
        .FirstOrDefaultAsync(s => s.Id == pPlatilloProductoMantDTO.Id);

    if (platilloProducto != null)
    {
        return new PlatilloProductoMantDTO
        {
            Id = platilloProducto.Id,
            IdPlatillo = platilloProducto.IdPlatillo,
            IdProducto = platilloProducto.IdProducto,
            CantidadUsada = platilloProducto.CantidadUsada,
            NombrePlatillo = platilloProducto.Platillo.Nombre, 
            NombreProducto = platilloProducto.Producto.Nombre 
        };
    }
    else
        return new PlatilloProductoMantDTO();
}


        public async Task<List<PlatilloProductoMantDTO>> ObtenerTodosAsync()
        {
            var platilloProductos = await dbContext.platilloProducto.ToListAsync();
            if (platilloProductos != null && platilloProductos.Count > 0)
            {
                var list = new List<PlatilloProductoMantDTO>();
                platilloProductos.ForEach(s => list.Add(new PlatilloProductoMantDTO
                {
                    Id = s.Id,
                    IdPlatillo = s.IdPlatillo,
                    IdProducto = s.IdProducto,
                    CantidadUsada = s.CantidadUsada
                }));
                return list;
            }
            else
                return new List<PlatilloProductoMantDTO>();
        }

       public async Task<PaginacionOutputDTO<List<PlatilloProductoMantDTO>>> BuscarAsync(PlatilloProductoBuscarDTO pPlatilloProductoBuscarDTO)
{
    var result = new PaginacionOutputDTO<List<PlatilloProductoMantDTO>>();
    result.Data = new List<PlatilloProductoMantDTO>();
    
    var select = dbContext.platilloProducto
        .Include(pp => pp.Platillo) 
        .Include(pp => pp.Producto)  
        .AsQueryable();

    select = QuerySelect(select, pPlatilloProductoBuscarDTO);

    var platilloProductos = await select.ToListAsync();
    if (platilloProductos.Count > 0)
    {
        if (pPlatilloProductoBuscarDTO.IsCount)
        {
            pPlatilloProductoBuscarDTO.Take = 0;
            var selectCount = dbContext.platilloProducto.AsQueryable();
            result.Count = await QuerySelect(selectCount, pPlatilloProductoBuscarDTO).CountAsync();
        }

        platilloProductos.ForEach(pp => result.Data.Add(new PlatilloProductoMantDTO
        {
            Id = pp.Id,
            IdPlatillo = pp.IdPlatillo,
            IdProducto = pp.IdProducto,
            CantidadUsada = pp.CantidadUsada,
            NombrePlatillo = pp.Platillo.Nombre, 
            NombreProducto = pp.Producto.Nombre  
        }));
    }
    return result;
}


        public Task<PlatilloProductoMantDTO> ObtenerPorIdPlatilloAsync(int idPlatillo)
        {
            throw new NotImplementedException();
        }
    }
}
