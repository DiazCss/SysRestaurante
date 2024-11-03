using SysRestaurante.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SysRestaurante.BL.DTOs.PaginacionDTO;

namespace SysRestaurante.BL.Interfaces
{
    public interface IPedidoBL
    {
        public Task<int> CreateAsync(PedidoDTO pPedidoDTO);
        public Task<int> ModificarAsync(PedidoDTO pPedidoDTO);
        public Task<int> EliminarAsync(PedidoDTO pPedidoDTO);
        public Task<PedidoDTO> ObtenerPorIdAsync(PedidoDTO pPedidoDTO);
        public Task<PaginacionOutputDTO<List<PedidoDTO>>> BuscarAsync(PedidoDTO pPedidoDTO);
        public Task<List<PedidoDTO>> ObtenerTodosAsync();

        public Task<List<PlatilloDTO>> ObtenerPlatillosPorPedidoIdAsync(int pedidoId);
    }
}
