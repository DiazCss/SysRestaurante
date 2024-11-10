using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.UsuarioDTOs;
using SysRestaurante.EN;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IUsuarioBL
    {
        public Task<int> CrearAsync(UsuarioMantDTO pUsuario);
        public Task<int> ModificarAsync(UsuarioMantDTO pUsuario);
        public Task<int> EliminarAsync(UsuarioMantDTO pUsuario);
        public Task<UsuarioMantDTO> ObtenerPorIdAsync(UsuarioMantDTO pUsuario);
        public Task<PaginacionOutputDTO<List<UsuarioMantDTO>>> BuscarAsync(UsuarioBuscarDTO pUsuario);

        public Task<UsuarioMantDTO> LoginAsync(UsuarioLoginDTO pUsuario);
        public Task<int> CambiarPasswordAsync(UsuarioCambiarPasswordDTO pUsuario);

        public Task<int> RegisterAsync(UsuarioRegisterDTO pUsuario);

        public Task<bool> ExisteEmailAsync(string email);



    }
}
