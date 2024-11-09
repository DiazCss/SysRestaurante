using Microsoft.EntityFrameworkCore;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.RolDTOs;
using SysRestaurante.BL.DTOs.UsuarioDTOs;
using SysRestaurante.BL.Interfaces;
using SysRestaurante.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.DAL
{
    internal class UsuarioDAL : IUsuarioBL
    {
        readonly SysRestauranteDbContext dbContext;
        public UsuarioDAL(SysRestauranteDbContext context) => dbContext = context;

        internal IQueryable<Usuarios> QuerySelect(IQueryable<Usuarios> pQuery, UsuarioBuscarDTO pUsuario)
        {
            if (!string.IsNullOrWhiteSpace(pUsuario.Nombre_Usuario_Like))
                pQuery = pQuery.Where(s => s.datosPersonales.Nombre.Contains(pUsuario.Nombre_Usuario_Like));
            if (!string.IsNullOrWhiteSpace(pUsuario.Apellido_Usuario_Like))
                pQuery = pQuery.Where(s => s.datosPersonales.Apellido.Contains(pUsuario.Apellido_Usuario_Like));
            if (!string.IsNullOrWhiteSpace(pUsuario.Email_Usuario_Like))
                pQuery = pQuery.Where(s => s.datosPersonales.Email.Contains(pUsuario.Email_Usuario_Like));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pUsuario.Take > 0)
                pQuery = pQuery.Skip(pUsuario.Skip).Take(pUsuario.Take).AsQueryable();
            return pQuery;
        }

        public async Task<PaginacionOutputDTO<List<UsuarioMantDTO>>> BuscarAsync(UsuarioBuscarDTO pUsuario)
        {
            var result = new PaginacionOutputDTO<List<UsuarioMantDTO>>();
            result.Data = new List<UsuarioMantDTO>();
            var select = dbContext.usuario.AsQueryable();

            // Aplicar los filtros con el método QuerySelect
            select = QuerySelect(select, pUsuario);

            // Incluir las relaciones necesarias
            select = select.Include(s => s.rol)
                           .Include(s => s.datosPersonales); // Asegúrate de cargar datosPersonales

            // Ejecutar la consulta
            var usuarios = await select.ToListAsync();

            if (usuarios.Count > 0)
            {
                if (pUsuario.IsCount)
                {
                    pUsuario.Take = 0;
                    var selectCount = dbContext.usuario.AsQueryable();
                    result.Count = await QuerySelect(selectCount, pUsuario).CountAsync();
                }

                // Transformar los resultados
                usuarios.ForEach(s => result.Data.Add(new UsuarioMantDTO
                {
                    Id = s.Id,
                    Nombre = s.datosPersonales.Nombre,
                    Apellido = s.datosPersonales.Apellido,
                    Email = s.datosPersonales.Email,
                    Telefono = s.datosPersonales.Telefono,
                    Password = s.Password,
                    IdRol = s.IdRol,
                    Rol = new RolMantDTO { Id = s.rol.Id, Nombre = s.rol.Nombre }
                }));
            }
            return result;
        }


        public async Task<int> CambiarPasswordAsync(UsuarioCambiarPasswordDTO pUsuario)
        {
            var usuario = await dbContext.usuario.Where(s => s.Id == pUsuario.Id && s.Password == pUsuario.PasswordActual).FirstOrDefaultAsync();
            if (usuario != null && usuario.Id != 0)
            {
                usuario.Password = pUsuario.Password;
                dbContext.Update(usuario);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> CrearAsync(UsuarioMantDTO pUsuario)
        {
            DatosPersonales datosPersonales = new DatosPersonales()
            {
                Nombre = pUsuario.Nombre,
                Apellido = pUsuario.Apellido,
                Telefono = pUsuario.Telefono,
                Email = pUsuario.Email

            };
            Usuarios usuario = new Usuarios()
            {
                IdRol = pUsuario.IdRol,
                Password = pUsuario.Password,
                datosPersonales = datosPersonales 
            };
            dbContext.Add(usuario);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(UsuarioMantDTO pUsuario)
        {
            var usuario = await dbContext.usuario.Where(s => s.Id == pUsuario.Id).Include(s => s.rol).FirstOrDefaultAsync();
            if (usuario != null && usuario.Id != 0)
            {
                dbContext.usuario.Remove(usuario);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<UsuarioMantDTO> LoginAsync(UsuarioLoginDTO pUsuario)
        {
            var find = await dbContext.usuario.Include(s => s.rol).Include(s => s.datosPersonales).FirstOrDefaultAsync(s => s.datosPersonales.Email == pUsuario.Email && s.Password == pUsuario.Password);
            if (find != null)
            {
                return new UsuarioMantDTO
                {
                    Nombre = find.datosPersonales.Nombre,
                    Apellido = find.datosPersonales.Apellido,
                    Email = find.datosPersonales.Email,
                    Id = find.Id,
                    IdRol = find.IdRol,
                };
            }
            else
                return new UsuarioMantDTO();
        }

        public async Task<int> ModificarAsync(UsuarioMantDTO pUsuario)
        {
            var usuario = await dbContext.usuario.Where(s => s.Id == pUsuario.Id).Include(s => s.rol).Include(d => d.datosPersonales).FirstOrDefaultAsync();
            if (usuario != null && usuario.Id != 0)
            {

                usuario.datosPersonales.Nombre = pUsuario.Nombre;
                usuario.datosPersonales.Apellido = pUsuario.Apellido;
                usuario.datosPersonales.Email = pUsuario.Email;
                usuario.datosPersonales.Telefono = pUsuario.Telefono;
                usuario.IdRol = pUsuario.IdRol;
                dbContext.Update(usuario);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<UsuarioMantDTO> ObtenerPorIdAsync(UsuarioMantDTO pUsuario)
        {
            var find = await dbContext.usuario.Where(s => s.Id == pUsuario.Id).Include(s => s.rol).Include(d => d.datosPersonales).FirstOrDefaultAsync();
            if (find != null && find.Id != 0)
            {
                return new UsuarioMantDTO
                {
                    Apellido = find.datosPersonales.Apellido,
                    Email = find.datosPersonales.Email,
                    Telefono = find.datosPersonales.Telefono,
                    Id = find.Id,
                    IdRol = find.IdRol,
                    Nombre = find.datosPersonales.Nombre,
                    Rol = new RolMantDTO { Id = find.rol.Id, Nombre = find.rol.Nombre }
                };
            }
            else
                return new UsuarioMantDTO();
        }





    }
}
