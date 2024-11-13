using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.PlatilloProductoDTOs; 
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IPlatilloProductoBL
    {
        public Task<int> CreateAsync(PlatilloProductoMantDTO pPlatilloProductoDTO);
        public Task<int> ModificarAsync(PlatilloProductoMantDTO pPlatilloProductoDTO);
        public Task<int> EliminarAsync(PlatilloProductoMantDTO pPlatilloProductoDTO);
        public Task<PlatilloProductoMantDTO> ObtenerPorIdPlatilloAsync(int idPlatillo);
        public Task<PlatilloProductoMantDTO> ObtenerPorIdAsync(PlatilloProductoMantDTO pPlatilloMantDTO);
        public Task<PaginacionOutputDTO<List<PlatilloProductoMantDTO>>> BuscarAsync(PlatilloProductoBuscarDTO pPlatilloProductoDTO);
        public Task<List<PlatilloProductoMantDTO>> ObtenerTodosAsync();
    }
}
