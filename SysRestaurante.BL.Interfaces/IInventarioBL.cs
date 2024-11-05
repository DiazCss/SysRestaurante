using SysRestaurante.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IInventarioBL
    {
        public Task<int> CreateAsync(InventarioDTO pInventarioDTO);
        public Task<int> ModificarAsync(InventarioDTO pInventarioDTO);
        public Task<int> EliminarAsync(InventarioDTO pInventarioDTO);
        public Task<InventarioDTO> ObtenerPorIdAsync(InventarioDTO pInventarioDTO);
        public Task<PaginacionOutputDTO<List<InventarioDTO>>> BuscarAsync(InventarioDTO pInventarioDTO);
        public Task<List<InventarioDTO>> ObtenerTodosAsync();
    }

}
