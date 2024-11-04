using SysRestaurante.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SysRestaurante.BL.DTOs.PaginacionDTO;

namespace SysRestaurante.BL.Interfaces
{
    public interface IPlatilloBL
    {
        public Task<int> CreateAsync(PlatilloDTO pPlatilloDTO);
        public Task<int> ModificarAsync(PlatilloDTO pPlatilloDTO);
        public Task<int> EliminarAsync(PlatilloDTO pPlatilloDTO);
        public Task<PlatilloDTO> ObtenerPorIdAsync(int id);
        public Task<PaginacionOutputDTO<List<PlatilloDTO>>> BuscarAsync(PlatilloDTO pPlatilloDTO);
        public Task<List<PlatilloDTO>> ObtenerTodosAsync();
    }
}
