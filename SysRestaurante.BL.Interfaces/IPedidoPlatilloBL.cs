using SysRestaurante.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IPedidoPlatilloBL
    {
        public Task<int> CreateAsync(PedidoPlatilloDTO pPedidoPlatilloDTO);
        public Task<int> ModificarAsync(PedidoPlatilloDTO pPedidoPlatilloDTO);
        public Task<int> EliminarAsync(PedidoPlatilloDTO pPedidoPlatilloDTO);
        public Task<PedidoPlatilloDTO> ObtenerPorIdAsync(int id);
        public Task<PaginacionOutputDTO<List<PedidoPlatilloDTO>>> BuscarAsync(PedidoPlatilloDTO pPedidoPlatilloDTO);
        public Task<List<PedidoPlatilloDTO>> ObtenerTodosAsync();
    }
}
