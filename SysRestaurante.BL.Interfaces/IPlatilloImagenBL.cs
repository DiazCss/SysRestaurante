using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.PlatilloImagenDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IPlatilloImagenBL
    {
        public Task<int> CreateAsync(PlatilloImagenMantDTO pPlatilloImagenDTO, string imagenPlatillo);
        public Task<int> ModificarAsync(PlatilloImagenMantDTO pPlatilloImagenDTO);
        public Task<int> EliminarAsync(PlatilloImagenMantDTO pPlatilloImagenDTO);
        public Task<PlatilloImagenMantDTO> ObtenerPorIdAsync(PlatilloImagenMantDTO pPlatilloImagenDTO);
        public Task<PaginacionOutputDTO<List<PlatilloImagenMantDTO>>> BuscarAsync(PlatilloImagenBuscarDTO pPlatilloImagenBuscarDTO);
        public Task<List<PlatilloImagenMantDTO>> ObtenerTodosAsync();
    }
}
