using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.PlatilloDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysRestaurante.DAL
{
    internal class PlatilloDAL : IPlatilloBL
    {
        readonly SysRestauranteDbContext dbContext;
        public PlatilloDAL(SysRestauranteDbContext context) => dbContext = context;

        #region filtros de busqueda
        internal IQueryable<Platillo> QuerySelect(IQueryable<Platillo> pQuery, PlatilloBuscarDTO pPlatillo)
        {
            if (!string.IsNullOrWhiteSpace(pPlatillo.Nombre_Platillo_Like))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pPlatillo.Nombre_Platillo_Like));

            if (!string.IsNullOrWhiteSpace(pPlatillo.IngredientePrincipal_Like))
                pQuery = pQuery.Where(s => s.IngredientePrincipal.Contains(pPlatillo.IngredientePrincipal_Like));

            if (pPlatillo.IdCategoria_Equal.HasValue)
                pQuery = pQuery.Where(s => s.IdCategoria == pPlatillo.IdCategoria_Equal.Value);

            if (pPlatillo.Disponibilidad_Equal.HasValue)
                pQuery = pQuery.Where(s => s.Disponibilidad == pPlatillo.Disponibilidad_Equal.Value);

            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pPlatillo.Take > 0)
                pQuery = pQuery.Skip(pPlatillo.Skip).Take(pPlatillo.Take).AsQueryable();
            return pQuery;
        }
        #endregion

        public async Task<int> CreateAsync(PlatilloMantDTO pPlatilloMantDTO)
        {
            var platillo = new Platillo
            {
                Nombre = pPlatilloMantDTO.Nombre,
                Descripcion = pPlatilloMantDTO.Descripcion,
                Precio = pPlatilloMantDTO.Precio,
                Disponibilidad = pPlatilloMantDTO.Disponibilidad,
                TiempoPreparacion = pPlatilloMantDTO.TiempoPreparacion,
                IngredientePrincipal = pPlatilloMantDTO.IngredientePrincipal,
                FechaActualizacion = pPlatilloMantDTO.FechaActualizacion,
                IdCategoria = pPlatilloMantDTO.IdCategoria
            };
            dbContext.platillo.Add(platillo);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(PlatilloMantDTO pPlatilloMantDTO)
        {
            var platillo = await dbContext.platillo.FirstOrDefaultAsync(p => p.Id == pPlatilloMantDTO.Id);
            if (platillo != null)
            {
                dbContext.platillo.Remove(platillo);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(PlatilloMantDTO pPlatilloMantDTO)
        {
            var platillo = await dbContext.platillo.FirstOrDefaultAsync(p => p.Id == pPlatilloMantDTO.Id);
            if (platillo != null)
            {
                platillo.Nombre = pPlatilloMantDTO.Nombre;
                platillo.Descripcion = pPlatilloMantDTO.Descripcion;
                platillo.Precio = pPlatilloMantDTO.Precio;
                platillo.Disponibilidad = pPlatilloMantDTO.Disponibilidad;
                platillo.TiempoPreparacion = pPlatilloMantDTO.TiempoPreparacion;
                platillo.IngredientePrincipal = pPlatilloMantDTO.IngredientePrincipal;
                platillo.FechaActualizacion = pPlatilloMantDTO.FechaActualizacion;
                platillo.IdCategoria = pPlatilloMantDTO.IdCategoria;

                dbContext.platillo.Update(platillo);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<PlatilloMantDTO> ObtenerPorIdAsync(PlatilloMantDTO pPlatilloMantDTO)
        {
            var platillo = await dbContext.platillo.FirstOrDefaultAsync(p => p.Id == pPlatilloMantDTO.Id);
            if (platillo != null)
            {
                return new PlatilloMantDTO
                {
                    Id = platillo.Id,
                    Nombre = platillo.Nombre,
                    Descripcion = platillo.Descripcion,
                    Precio = platillo.Precio,
                    Disponibilidad = platillo.Disponibilidad,
                    TiempoPreparacion = platillo.TiempoPreparacion,
                    IngredientePrincipal = platillo.IngredientePrincipal,
                    FechaActualizacion = platillo.FechaActualizacion,
                    IdCategoria = platillo.IdCategoria
                };
            }
            else
                return new PlatilloMantDTO();
        }

        public async Task<List<PlatilloMantDTO>> ObtenerTodosAsync()
        {
            var platillos = await dbContext.platillo.ToListAsync();
            if (platillos != null && platillos.Count > 0)
            {
                var list = new List<PlatilloMantDTO>();
                platillos.ForEach(p => list.Add(new PlatilloMantDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Disponibilidad = p.Disponibilidad,
                    TiempoPreparacion = p.TiempoPreparacion,
                    IngredientePrincipal = p.IngredientePrincipal,
                    FechaActualizacion = p.FechaActualizacion,
                    IdCategoria = p.IdCategoria
                }));
                return list;
            }
            else
                return new List<PlatilloMantDTO>();
        }

        public async Task<PaginacionOutputDTO<List<PlatilloMantDTO>>> BuscarAsync(PlatilloBuscarDTO pPlatilloBuscarDTO)
        {
            var result = new PaginacionOutputDTO<List<PlatilloMantDTO>>();
            result.Data = new List<PlatilloMantDTO>();
            var select = dbContext.platillo.AsQueryable();

            select = QuerySelect(select, pPlatilloBuscarDTO);

            var platillos = await select.ToListAsync();
            if (platillos.Count > 0)
            {
                if (pPlatilloBuscarDTO.IsCount)
                {
                    pPlatilloBuscarDTO.Take = 0;
                    var selectCount = dbContext.platillo.AsQueryable();
                    result.Count = await QuerySelect(selectCount, pPlatilloBuscarDTO).CountAsync();
                }

                platillos.ForEach(p => result.Data.Add(new PlatilloMantDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Disponibilidad = p.Disponibilidad,
                    TiempoPreparacion = p.TiempoPreparacion,
                    IngredientePrincipal = p.IngredientePrincipal,
                    FechaActualizacion = p.FechaActualizacion,
                    IdCategoria = p.IdCategoria
                }));
            }
            return result;
        }

        public Task<PlatilloMantDTO> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
