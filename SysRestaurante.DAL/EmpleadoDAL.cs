using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.EmpleadoDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;


namespace SysRestaurante.DAL
{
    internal class EmpleadoDAL : IEmpleadoBL
    {
        readonly SysRestauranteDbContext dbContext;
        public EmpleadoDAL(SysRestauranteDbContext context) => dbContext = context;

        #region filtros de busqueda
        internal IQueryable<Empleado> QuerySelect(IQueryable<Empleado> pQuery, EmpleadoBuscarDTO pEmpleado)
        {
            if (!string.IsNullOrWhiteSpace(pEmpleado.Nombre_Empleado_Like))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pEmpleado.Nombre_Empleado_Like));

            if (!string.IsNullOrWhiteSpace(pEmpleado.Apellido_Empleado_Like))
                pQuery = pQuery.Where(s => s.Apellido.Contains(pEmpleado.Apellido_Empleado_Like));

            if (!string.IsNullOrWhiteSpace(pEmpleado.Estado_Equal))
                pQuery = pQuery.Where(s => s.Estado.Equals(pEmpleado.Estado_Equal));

            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pEmpleado.Take > 0)
                pQuery = pQuery.Skip(pEmpleado.Skip).Take(pEmpleado.Take).AsQueryable();
            return pQuery;
        }

        #endregion 

        public async Task<int> CreateAsync(EmpleadoMantDTO pEmpleadoMantDTO)
        {
            bool empleadoExistente = await dbContext.empleado.AnyAsync(c => c.Email == pEmpleadoMantDTO.Email);
            if (empleadoExistente)
            {
                return 0;
            }
            Empleado empleado = new Empleado()
            {
                Nombre = pEmpleadoMantDTO.Nombre,
                Apellido = pEmpleadoMantDTO.Apellido,
                Email= pEmpleadoMantDTO.Email,
                Telefono = pEmpleadoMantDTO.Telefono,
                Puesto = pEmpleadoMantDTO.Puesto,
                Estado = pEmpleadoMantDTO.Estado,
                Salario = pEmpleadoMantDTO.Salario,
                FechaContratacion = pEmpleadoMantDTO.FechaContratacion,

                
            };

            dbContext.Add(empleado);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(EmpleadoMantDTO pEmpleadoMantDTO)
        {
            
            var empleado = await dbContext.empleado.Where(s => s.Id == pEmpleadoMantDTO.Id)
                            .FirstOrDefaultAsync();
            if (empleado != null && empleado.Id != 0)
            {
                dbContext.empleado.Remove(empleado);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(EmpleadoMantDTO pEmpleadoMantDTO)
        {
            var empleado = await dbContext.empleado.Where(s => s.Id == pEmpleadoMantDTO.Id)
                        .FirstOrDefaultAsync();
            if (empleado != null && empleado.Id != 0)
            {

                empleado.Nombre = pEmpleadoMantDTO.Nombre;
               //Agregar atributos faltantes


                dbContext.Update(empleado);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<EmpleadoMantDTO> ObtenerPorIdAsync(EmpleadoMantDTO pEmpleadoMantDTO)
        {
            var empleado = await dbContext.empleado.Where(s => s.Id == pEmpleadoMantDTO.Id)
                 .FirstOrDefaultAsync();
            if (empleado != null && empleado.Id != 0)
            {
                return new EmpleadoMantDTO
                {
                    Id = empleado.Id,
                    Nombre = empleado.Nombre,
                    //Agregar atributos faltantes
                };
            }
            else
                return new EmpleadoMantDTO();
        }

        public async Task<List<EmpleadoMantDTO>> ObtenerTodosAsync()
        {
            var empleados = await dbContext.empleado.ToListAsync();
            if (empleados != null && empleados.Count > 0)
            {
                var list = new List<EmpleadoMantDTO>();
                empleados.ForEach(s => list.Add(new EmpleadoMantDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    //Agregar atributos necesarios
                }));
                return list;
            }
            else
                return new List<EmpleadoMantDTO>();
        }

        public async Task<PaginacionOutputDTO<List<EmpleadoMantDTO>>> BuscarAsync(EmpleadoBuscarDTO pEmpleadoBuscarDTO)
        {
            var result = new PaginacionOutputDTO<List<EmpleadoMantDTO>>();
            result.Data = new List<EmpleadoMantDTO>();
            var select = dbContext.empleado.AsQueryable();

            select = QuerySelect(select, pEmpleadoBuscarDTO);
         
            var clientes = await select.ToListAsync();
            if (clientes.Count > 0)
            {
                if (pEmpleadoBuscarDTO.IsCount)
                {
                    pEmpleadoBuscarDTO.Take = 0;
                    var selectCount = dbContext.empleado.AsQueryable();
                    result.Count = await QuerySelect(selectCount, pEmpleadoBuscarDTO).CountAsync();
                }

                clientes.ForEach(s => result.Data.Add(new EmpleadoMantDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    Email = s.Email,
                    Puesto = s.Puesto,
                    Salario = s.Salario,
                    FechaContratacion = s.FechaContratacion,
                    Estado = s.Estado
                }));
            }
            return result;
        }

    }
}
