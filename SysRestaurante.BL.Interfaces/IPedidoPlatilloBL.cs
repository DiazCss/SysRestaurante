using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.PedidoPlatilloDTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IPedidoPlatilloBL
    {
        public Task<int> CreateAsync(PedidoPlatilloManDTO pPedidoPlatilloDTO);
        public Task<int> ModificarAsync(PedidoPlatilloManDTO pPedidoPlatilloDTO);
        public Task<int> EliminarAsync(PedidoPlatilloManDTO pPedidoPlatilloDTO);
        public Task<PedidoPlatilloManDTO> ObtenerPorIdAsync(int id);
        public Task<PaginacionOutputDTO<List<PedidoPlatilloManDTO>>> BuscarAsync(PedidoPlatilloManDTO pPedidoPlatilloDTO);
        public Task<List<PedidoPlatilloManDTO>> ObtenerTodosAsync();
    }
}
