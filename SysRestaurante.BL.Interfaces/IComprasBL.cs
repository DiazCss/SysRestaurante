using System;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.CompraDTOs;

namespace SysRestaurante.BL.Interfaces;

public interface IComprasBL
{
        Task<int> CreateAsync(CompraManDTOs pCompraDTO);
        Task<int> ModificarAsync(CompraManDTOs pCompraDTO);
        Task<int> EliminarAsync(CompraManDTOs pCompraDTO);
        Task<CompraManDTOs> ObtenerPorIdAsync(CompraManDTOs pCompraDTO);
        Task<PaginacionOutputDTO<List<CompraManDTOs>>> BuscarAsync(CompraBuscarDTOs pCompraDTO);
        Task<List<CompraManDTOs>> ObtenerTodosAsync();
}
