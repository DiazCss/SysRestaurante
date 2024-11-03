using SysRestaurante.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SysRestaurante.BL.DTOs.PaginacionDTO;

namespace SysRestaurante.BL.Interfaces
{
    public interface IClienteBL
    {
        public Task<int> CreateAsync(ClienteDTO pClienteDTO);
        public Task<int> ModificarAsync(ClienteDTO pClienteDTO);
        public Task<int> EliminarAsync(ClienteDTO pClienteDTO);
        public Task<ClienteDTO> ObtenerPorIdAsync(ClienteDTO pClienteDTO);
        public Task<PaginacionOutputDTO<List<ClienteDTO>>> BuscarAsync(ClienteDTO pClienteDTO);
        public Task<List<ClienteDTO>> ObtenerTodosAsync();
    }
}
