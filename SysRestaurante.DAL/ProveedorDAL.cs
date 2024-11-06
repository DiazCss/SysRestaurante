using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.ProveedorDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.DAL
{
    internal class ProveedorDAL : IProveedorBL
    {
        readonly SysRestauranteDbContext dbContext;
        public ProveedorDAL(SysRestauranteDbContext context) => dbContext = context;

        #region filtro de busqueda
        internal IQueryable<Proveedor> QuerySelect(IQueryable<Proveedor> pQuery,ProveedorBuscarDTO pProveedor)
        {
            if (!string.IsNullOrWhiteSpace(pProveedor.Nombre_Proveedor_Like))
                pQuery = pQuery.Where(p => p.Nombre.Contains(pProveedor.Nombre_Proveedor_Like));

            if (!string.IsNullOrWhiteSpace(pProveedor.Contacto_Proveedor_Like))
                pQuery = pQuery.Where(p => p.Contacto.Contains(pProveedor.Contacto_Proveedor_Like));

            if (!string.IsNullOrWhiteSpace(pProveedor.Direccion_Proveedor_Like))
                pQuery = pQuery.Where(p => p.Direccion.Contains(pProveedor.Direccion_Proveedor_Like));

            pQuery = pQuery.OrderByDescending(p => p.Id).AsQueryable();
            if (pProveedor.Take > 0)
                pQuery = pQuery.Skip(pProveedor.Skip).Take(pProveedor.Take).AsQueryable();
            return pQuery;
        }

        #endregion

        public async Task<int> CreateAsync(ProveedorMantDTO pProveedorMantDTO)
        {
            bool proveedorExistente = await dbContext.proveedor.AnyAsync(p => p.Nombre == pProveedorMantDTO.Nombre);
            if (proveedorExistente)
            {
                return 0;
            }
            Proveedor proveedor = new Proveedor()
            {
                Nombre = pProveedorMantDTO.Nombre,
                Contacto = pProveedorMantDTO.Contacto,
                Direccion = pProveedorMantDTO.Direccion
            };

            dbContext.Add(proveedor);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(ProveedorMantDTO pProveedorMantDTO)
        {
            var proveedor = await dbContext.proveedor.Where(p => p.Id == pProveedorMantDTO.Id)
                            .FirstOrDefaultAsync();
            if (proveedor != null && proveedor.Id != 0)
            {
                dbContext.proveedor.Remove(proveedor);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(ProveedorMantDTO pProveedorMantDTO)
        {
            var proveedor = await dbContext.proveedor.Where(p => p.Id == pProveedorMantDTO.Id)
                            .FirstOrDefaultAsync();
            if (proveedor != null && proveedor.Id != 0)
            {
                proveedor.Nombre = pProveedorMantDTO.Nombre;
                proveedor.Contacto = pProveedorMantDTO.Contacto;
                proveedor.Direccion = pProveedorMantDTO.Direccion;

                dbContext.Update(proveedor);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<ProveedorMantDTO> ObtenerPorIdAsync(ProveedorMantDTO proveedorMantDTOs)
        {
            var proveedor = await dbContext.proveedor.Where(p => p.Id == proveedorMantDTOs.Id)
                        .FirstOrDefaultAsync();
            if (proveedor != null && proveedor.Id != 0)
            {
                return new ProveedorMantDTO
                {
                    Id = proveedor.Id,
                    Nombre = proveedor.Nombre,
                    Contacto = proveedor.Contacto,
                    Direccion = proveedor.Direccion,
                };
            }
            else
                return new ProveedorMantDTO();
        }

        public async Task<List<ProveedorMantDTO>> ObtenerTodosAsync()
        {
            var proveedor = await dbContext.proveedor.ToListAsync();
            if (proveedor != null && proveedor.Count > 0)
            {
                var List = new List<ProveedorMantDTO>();
                proveedor.ForEach(p => List.Add(new ProveedorMantDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Contacto = p.Contacto,
                    Direccion = p.Direccion,
                }));
                return List;
            }
            else
                return new List<ProveedorMantDTO>();
        }

        public async Task<PaginacionOutputDTO<List<ProveedorMantDTO>>> BuscarAync(ProveedorBuscarDTO pProveedorBuscarDTO)
        {
            var result = new PaginacionOutputDTO<List<ProveedorMantDTO>>();
            result.Data = new List<ProveedorMantDTO>();
            var select = dbContext.proveedor.AsQueryable();

            select = QuerySelect(select, pProveedorBuscarDTO);

            var proveedor = await select.ToListAsync();
            if (proveedor.Count > 0)
            {
                if (pProveedorBuscarDTO.IsCount)
                {
                    pProveedorBuscarDTO.Take = 0;
                    var selectCount = dbContext.proveedor.AsQueryable();
                    result.Count = await QuerySelect(selectCount, pProveedorBuscarDTO).CountAsync();
                }

                proveedor.ForEach(p => result.Data.Add(new ProveedorMantDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Contacto = p.Contacto,
                    Direccion = p.Direccion
                }));
            }
            return result;
        }
    }
}