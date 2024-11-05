using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.ClienteDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IClienteBL
    {
        public Task<int> CreateAsync(ClienteMantDTO pClienteDTO);
        public Task<int> ModificarAsync(ClienteMantDTO pClienteDTO);
        public Task<int> EliminarAsync(ClienteMantDTO pClienteDTO);
        public Task<ClienteMantDTO> ObtenerPorIdAsync(ClienteMantDTO pClienteDTO);
        public Task<PaginacionOutputDTO<List<ClienteMantDTO>>> BuscarAsync(ClienteMantDTO pClienteDTO);
        public Task<List<ClienteMantDTO>> ObtenerTodosAsync();
    }
}
