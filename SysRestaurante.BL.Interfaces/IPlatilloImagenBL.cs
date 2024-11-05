using SysRestaurante.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IPlatilloImagenBL
    {
        public Task<int> CreateAsync(PlatilloImagenDTO pPlatilloImagenDTO);
        public Task<int> ModificarAsync(PlatilloImagenDTO pPlatilloImagenDTO);
        public Task<int> EliminarAsync(PlatilloImagenDTO pPlatilloImagenDTO);
        public Task<PlatilloImagenDTO> ObtenerPorIdAsync(int id);
        public Task<PaginacionOutputDTO<List<PlatilloImagenDTO>>> BuscarAsync(PlatilloImagenDTO pPlatilloImagenDTO);
        public Task<List<PlatilloImagenDTO>> ObtenerTodosAsync();
    }
}
