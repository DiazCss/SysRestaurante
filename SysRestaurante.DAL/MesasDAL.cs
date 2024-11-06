using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.EmpleadoDTOs;
using SysRestaurante.BL.DTOs.MesaDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.DAL
{
    internal class MesasDAL : IMesasBL
    {
        readonly SysRestauranteDbContext dbContext;
        public MesasDAL(SysRestauranteDbContext context) => dbContext = context;


        #region filtros de busqueda
        internal IQueryable<Mesas> QuerySelect(IQueryable<Mesas> pQuery, MesasBuscarDTO pMesaDTO)
        {
            if (!string.IsNullOrWhiteSpace(pMesaDTO.NumeroMesa_Like))
                pQuery = pQuery.Where(s => s.NumeroMesa.Contains(pMesaDTO.NumeroMesa_Like));

            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pMesaDTO.Take > 0)
                pQuery = pQuery.Skip(pMesaDTO.Skip).Take(pMesaDTO.Take).AsQueryable();
            return pQuery;
        }

        #endregion

        public async Task<int> CreateAsync(MesasMantDTO pMesasMantDTO)
        {
            var mesa = new Mesas
            {
                Id = pMesasMantDTO.Id,
                NumeroMesa = pMesasMantDTO.NumeroMesa,
                Descripcion = pMesasMantDTO.Descripcion,
                Capacidad = pMesasMantDTO.Capacidad,
                Disponibilidad = pMesasMantDTO.Disponibilidad,
            };
            dbContext.mesas.Add(mesa);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(MesasMantDTO pMesasMantDTO)
        {
            var mesa = await dbContext.mesas
             .FirstOrDefaultAsync(e => e.Id == pMesasMantDTO.Id);
            if (mesa != null && mesa.Id != 0)
            {
                dbContext.mesas.Remove(mesa);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(MesasMantDTO pMesasMantDTO)
        {
            var mesa = await dbContext.mesas.FirstOrDefaultAsync(s => s.Id == pMesasMantDTO.Id);

            if (mesa != null)
            {
                mesa.NumeroMesa = pMesasMantDTO.NumeroMesa;
                mesa.Descripcion = pMesasMantDTO.Descripcion;
                mesa.Capacidad = pMesasMantDTO.Capacidad;
                mesa.Disponibilidad = pMesasMantDTO.Disponibilidad;
                dbContext.Update(mesa);
                return await dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<MesasMantDTO> ObtenerPorIdAsync(MesasMantDTO pMesaMantDTO)
        {
            var mesa = await dbContext.mesas
         .FirstOrDefaultAsync(s => s.Id == pMesaMantDTO.Id);

            if (mesa != null)
            {
                return new MesasMantDTO
                {
                   Id = mesa.Id,
                   NumeroMesa = mesa.NumeroMesa,
                   Descripcion = mesa.Descripcion,
                   Capacidad = mesa.Capacidad,
                   Disponibilidad = mesa.Disponibilidad
                };
            }
            else
                return new MesasMantDTO();
        }

        public async Task<List<MesasMantDTO>> ObtenerTodosAsync()
        {
            var mesa = await dbContext.mesas.ToListAsync();
            if (mesa != null && mesa.Count > 0)
            {
                var list = new List<MesasMantDTO>();
                mesa.ForEach(s => list.Add(new MesasMantDTO
                {
                    NumeroMesa = s.NumeroMesa,
                    Descripcion = s.Descripcion,
                }));
                return list;
            }
            else
                return new List<MesasMantDTO>();
        }


        public async Task<PaginacionOutputDTO<List<MesasMantDTO>>> BuscarAsync(MesasBuscarDTO pMesasBuscarDTO)
        {
            var result = new PaginacionOutputDTO<List<MesasMantDTO>>();
            result.Data = new List<MesasMantDTO>();
            var select = dbContext.mesas.AsQueryable();

            select = QuerySelect(select, pMesasBuscarDTO);

            var mesa2 = await select.ToListAsync();
            if (mesa2.Count > 0)
            {
                if (pMesasBuscarDTO.IsCount)
                {
                    pMesasBuscarDTO.Take = 0;
                    var selectCount = dbContext.mesas.AsQueryable();
                    result.Count = await QuerySelect(selectCount, pMesasBuscarDTO).CountAsync();
                }

                mesa2.ForEach(m => result.Data.Add(new MesasMantDTO
                {
                    Id = m.Id,
                   NumeroMesa = m.NumeroMesa,
                   Descripcion = m.Descripcion,
                   Capacidad = m.Capacidad,
                   Disponibilidad =m.Disponibilidad,

                }));
            }
            return result;
        }


    }
}
