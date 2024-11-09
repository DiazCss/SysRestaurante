using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.CategoriaPlatilloDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysRestaurante.DAL
{
    internal class CategoriaPlatilloDAL : ICategoriaPlatilloBL
    {
        private readonly SysRestauranteDbContext dbContext;

        public CategoriaPlatilloDAL(SysRestauranteDbContext context)
        {
            dbContext = context;
        }

        public async Task<int> CreateAsync(CategoriaPlatilloMantDTOs pCategoriaPlatilloDTO)
        {
            var categoriaPlatillo = new CategoriaPlatillo
            {
                Nombre = pCategoriaPlatilloDTO.Nombre,
                Descripcion = pCategoriaPlatilloDTO.Descripcion
            };

            dbContext.categoriaPlatillos.Add(categoriaPlatillo);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(CategoriaPlatilloMantDTOs pCategoriaPlatilloDTO)
        {
            var categoriaPlatillo = await dbContext.categoriaPlatillos
                .FirstOrDefaultAsync(c => c.Id == pCategoriaPlatilloDTO.Id);

            if (categoriaPlatillo != null)
            {
                dbContext.categoriaPlatillos.Remove(categoriaPlatillo);
                return await dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> ModificarAsync(CategoriaPlatilloMantDTOs pCategoriaPlatilloDTO)
        {
            var categoriaPlatillo = await dbContext.categoriaPlatillos
                .FirstOrDefaultAsync(c => c.Id == pCategoriaPlatilloDTO.Id);

            if (categoriaPlatillo != null)
            {
                categoriaPlatillo.Nombre = pCategoriaPlatilloDTO.Nombre;
                categoriaPlatillo.Descripcion = pCategoriaPlatilloDTO.Descripcion;
                dbContext.categoriaPlatillos.Update(categoriaPlatillo);
                return await dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<CategoriaPlatilloMantDTOs> ObtenerPorIdAsync(CategoriaPlatilloMantDTOs pCategoriaPlatilloDTO)
        {
            var categoriaPlatillo = await dbContext.categoriaPlatillos
                .FirstOrDefaultAsync(c => c.Id == pCategoriaPlatilloDTO.Id);

            if (categoriaPlatillo != null)
            {
                return new CategoriaPlatilloMantDTOs
                {
                    Id = categoriaPlatillo.Id,
                    Nombre = categoriaPlatillo.Nombre,
                    Descripcion = categoriaPlatillo.Descripcion
                };
            }
            else
            {
                return new CategoriaPlatilloMantDTOs();
            }
        }

        public async Task<List<CategoriaPlatilloMantDTOs>> ObtenerTodosAsync()
        {
            var categorias = await dbContext.categoriaPlatillos.ToListAsync();

            if (categorias != null && categorias.Count > 0)
            {
                return categorias.Select(c => new CategoriaPlatilloMantDTOs
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion
                }).ToList();
            }
            else
            {
                return new List<CategoriaPlatilloMantDTOs>();
            }
        }

        public async Task<PaginacionOutputDTO<List<CategoriaPlatilloMantDTOs>>> BuscarAsync(CategoriaPlatilloBuscarDTOs pCategoriaPlatilloBuscarDTO)
        {
            var result = new PaginacionOutputDTO<List<CategoriaPlatilloMantDTOs>>();
            result.Data = new List<CategoriaPlatilloMantDTOs>();

            var query = dbContext.categoriaPlatillos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pCategoriaPlatilloBuscarDTO.Nombre_CategoriaPlatillo_Like))
                query = query.Where(c => c.Nombre.Contains(pCategoriaPlatilloBuscarDTO.Nombre_CategoriaPlatillo_Like));

            query = query.OrderByDescending(c => c.Id).AsQueryable();

            if (pCategoriaPlatilloBuscarDTO.Take > 0)
                query = query.Skip(pCategoriaPlatilloBuscarDTO.Skip).Take(pCategoriaPlatilloBuscarDTO.Take);

            var categorias = await query.ToListAsync();
            if (categorias.Count > 0)
            {
                result.Data = categorias.Select(c => new CategoriaPlatilloMantDTOs
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion
                }).ToList();

                if (pCategoriaPlatilloBuscarDTO.IsCount)
                {
                    pCategoriaPlatilloBuscarDTO.Take = 0;
                    var countQuery = dbContext.categoriaPlatillos.AsQueryable();
                    result.Count = await countQuery.CountAsync();
                }
            }

            return result;
        }


        
    }
}
