using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.UsuarioDTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IUsuarioBL
    {
        public Task<int> CreateAsync(UsuarioMantDTO pUsuarioMantDTO);
        public Task<int> ModificarAsync(UsuarioMantDTO pUsuarioMantDTO);
        public Task<int> EliminarAsync(UsuarioMantDTO pUsuarioMantDTO);
        public Task<UsuarioMantDTO> ObtenerPorIdAsync(int id);
        public Task<PaginacionOutputDTO<List<UsuarioMantDTO>>> BuscarAsync(UsuarioBuscarDTO pUsuarioMantDTO);
        public Task<List<UsuarioMantDTO>> ObtenerTodosAsync();
    }
}
