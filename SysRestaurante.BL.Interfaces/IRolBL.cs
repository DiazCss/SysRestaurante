using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.RolDTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IRolBL
    {
        public Task<int> CreateAsync(RolMantDTO pRolDTO);
        public Task<int> ModificarAsync(RolMantDTO pRolDTO);
        public Task<int> EliminarAsync(RolMantDTO pRolDTO);
        public Task<RolMantDTO> ObtenerPorIdAsync(int id);
        public Task<PaginacionOutputDTO<List<RolMantDTO>>> BuscarAsync(RolMantDTO pRolDTO);
        public Task<List<RolMantDTO>> ObtenerTodosAsync();
    }
}
