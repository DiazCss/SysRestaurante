using SysRestaurante.BL.DTOs.RolDTOs;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SysRestaurante.DAL
{
    internal class RolDAL : IRolBL
    {
        readonly SysRestauranteDbContext dbContext;
        public RolDAL(SysRestauranteDbContext context) => dbContext = context;

        internal IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, RolBuscarDTO pRol)
        {
            if (!string.IsNullOrWhiteSpace(pRol.Nombre_Like))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pRol.Nombre_Like));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pRol.Take > 0)
                pQuery = pQuery.Skip(pRol.Skip).Take(pRol.Take).AsQueryable();
            return pQuery;
        }
        public async Task<PaginacionOutputDTO<List<RolMantDTO>>> BuscarAsync(RolBuscarDTO pRol)
        {
            var result = new PaginacionOutputDTO<List<RolMantDTO>>();
            result.Data = new List<RolMantDTO>();
            var select = dbContext.roles.AsQueryable();
            select = QuerySelect(select, pRol);
            var roles = await select.ToListAsync();
            if (roles.Count > 0)
            {
                if (pRol.IsCount)
                {
                    pRol.Take = 0;
                    var selectCount = dbContext.roles.AsQueryable();
                    result.Count = await QuerySelect(selectCount, pRol).CountAsync();
                }
                roles.ForEach(s => result.Data.Add(new RolMantDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre
                }));
            }
            return result;
        }

        public async Task<int> CreateAsync(RolMantDTO pRol)
        {
            Rol rol = new Rol()
            {
                Nombre = pRol.Nombre
            };
            dbContext.Add(rol);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(RolMantDTO pRol)
        {
            var rol = await dbContext.roles.FirstOrDefaultAsync(s => s.Id == pRol.Id);
            if (rol != null && rol.Id != 0)
            {
                dbContext.roles.Remove(rol);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;

        }

        public async Task<int> ModificarAsync(RolMantDTO pRol)
        {
            var rol = await dbContext.roles.FirstOrDefaultAsync(s => s.Id == pRol.Id);
            if (rol != null && rol.Id != 0)
            {
                rol.Nombre = pRol.Nombre;
                dbContext.Update(rol);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<RolMantDTO> ObtenerPorIdAsync(RolMantDTO pRol)
        {
            var rol = await dbContext.roles.FirstOrDefaultAsync(s => s.Id == pRol.Id);
            if (rol != null && rol.Id != 0)
            {
                return new RolMantDTO
                {
                    Id = rol.Id,
                    Nombre = rol.Nombre
                };
            }
            else
                return new RolMantDTO();
        }

        public async Task<List<RolMantDTO>> ObtenerTodosAsync()
        {
            var roles = await dbContext.roles.ToListAsync();
            if (roles != null && roles.Count > 0)
            {
                var list = new List<RolMantDTO>();
                roles.ForEach(s => list.Add(new RolMantDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre
                }));
                return list;
            }
            else
                return new List<RolMantDTO>();
        }
    }
}
