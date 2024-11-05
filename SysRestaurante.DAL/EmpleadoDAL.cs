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
               pQuery = pQuery.Where(s => s.DatosPersonal.Nombre.Contains(pEmpleado.Nombre_Empleado_Like));

            if (!string.IsNullOrWhiteSpace(pEmpleado.Apellido_Empleado_Like))
                pQuery = pQuery.Where(s => s.DatosPersonal.Apellido.Contains(pEmpleado.Apellido_Empleado_Like));

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
            //bool empleadoExistente = await dbContext.empleado.AnyAsync(c => c.Email == pEmpleadoMantDTO.Email);
            //if (empleadoExistente)
            //{
            //    return 0;
            //}
            var datosPersonales = new DatosPersonales
            {
                Nombre = pEmpleadoMantDTO.Nombre,
                Apellido = pEmpleadoMantDTO.Apellido,
                Email = pEmpleadoMantDTO.Email,
                Telefono = pEmpleadoMantDTO.Telefono
            };
            dbContext.datosPersonales.Add(datosPersonales);
            await dbContext.SaveChangesAsync();

            var empleado = new Empleado
            {
                Id = datosPersonales.Id, 
                Puesto = pEmpleadoMantDTO.Puesto,
                Estado = pEmpleadoMantDTO.Estado,
                Salario = pEmpleadoMantDTO.Salario,
                FechaContratacion = pEmpleadoMantDTO.FechaContratacion
            };
            dbContext.empleado.Add(empleado);
            return await dbContext.SaveChangesAsync(); 
        }

        public async Task<int> EliminarAsync(EmpleadoMantDTO pEmpleadoMantDTO)
        {

            var empleado = await dbContext.empleado
             .Include(e => e.DatosPersonal)
             .FirstOrDefaultAsync(e => e.Id == pEmpleadoMantDTO.Id);
            if (empleado != null && empleado.Id != 0)
            {
                dbContext.datosPersonales.Remove(empleado.DatosPersonal);
                dbContext.empleado.Remove(empleado);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(EmpleadoMantDTO pEmpleadoMantDTO)
        {
            var empleado = await dbContext.empleado
                .Include(e => e.DatosPersonal) 
                .FirstOrDefaultAsync(s => s.Id == pEmpleadoMantDTO.Id);

            if (empleado != null)
            {
                empleado.Puesto = pEmpleadoMantDTO.Puesto;
                empleado.Salario = pEmpleadoMantDTO.Salario;
                empleado.FechaContratacion = pEmpleadoMantDTO.FechaContratacion;
                empleado.Estado = pEmpleadoMantDTO.Estado;
                SetDatosPersonales(empleado.DatosPersonal, pEmpleadoMantDTO);
                dbContext.Update(empleado);

                return await dbContext.SaveChangesAsync();
            }
            else
            {
                return 0; 
            }
        }
        private void SetDatosPersonales(DatosPersonales datosPersonales, EmpleadoMantDTO pEmpleadoMantDTO)
        {
            if (datosPersonales != null)
            {
                datosPersonales.Nombre = pEmpleadoMantDTO.Nombre;
                datosPersonales.Apellido = pEmpleadoMantDTO.Apellido;
                datosPersonales.Email = pEmpleadoMantDTO.Email;
                datosPersonales.Telefono = pEmpleadoMantDTO.Telefono;
            }
        }
        public async Task<EmpleadoMantDTO> ObtenerPorIdAsync(EmpleadoMantDTO pEmpleadoMantDTO)
        {
            var empleado = await dbContext.empleado
         .Include(e => e.DatosPersonal) 
         .FirstOrDefaultAsync(s => s.Id == pEmpleadoMantDTO.Id);

            if (empleado != null)
            {
                return new EmpleadoMantDTO
                {
                    Id = empleado.Id,
                    Nombre = empleado.DatosPersonal.Nombre, 
                    Apellido = empleado.DatosPersonal.Apellido,
                    Telefono = empleado.DatosPersonal.Telefono,
                    Email = empleado.DatosPersonal.Email,
                    Salario = empleado.Salario,
                    Estado = empleado.Estado,
                    FechaContratacion = empleado.FechaContratacion,
                    Puesto = empleado.Puesto
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
                    Nombre = s.DatosPersonal.Nombre,
                    Apellido = s.DatosPersonal.Apellido,
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
            var select = dbContext.empleado
                .Include(e => e.DatosPersonal).AsQueryable();

            select = QuerySelect(select, pEmpleadoBuscarDTO);
         
            var empleados = await select.ToListAsync();
            if (empleados.Count > 0)
            {
                if (pEmpleadoBuscarDTO.IsCount)
                {
                    pEmpleadoBuscarDTO.Take = 0;
                    var selectCount = dbContext.empleado.Include(e => e.DatosPersonal).AsQueryable();
                    result.Count = await QuerySelect(selectCount, pEmpleadoBuscarDTO).CountAsync();
                }

                empleados.ForEach(e => result.Data.Add(new EmpleadoMantDTO
                {
                    Id = e.Id,
                    Nombre = e.DatosPersonal.Nombre, 
                    Apellido = e.DatosPersonal.Apellido,
                    Email = e.DatosPersonal.Email,
                    Telefono = e.DatosPersonal.Telefono,
                    Puesto = e.Puesto,
                    Salario = e.Salario,
                    FechaContratacion = e.FechaContratacion,
                    Estado = e.Estado
                }));
            }
            return result;
        }

    }
}
