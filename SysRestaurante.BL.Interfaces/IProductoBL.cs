using System;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.ProductoDTOs;

namespace SysRestaurante.BL.Interfaces;

public interface IProductoBL
{
     Task<int> CreateAsync(ProductoManDTOs pProductoManDTOs);
        Task<int> ModificarAsync(ProductoManDTOs pProductoManDTOs);
        Task<int> EliminarAsync(ProductoManDTOs pProductoManDTOs);
        Task<ProductoManDTOs> ObtenerPorIdAsync(ProductoManDTOs pProductoManDTOs);
        Task<PaginacionOutputDTO<List<ProductoManDTOs>>> BuscarAsync(ProductoBuscarDTOs pProductoManDTOs);
        Task<List<ProductoManDTOs>> ObtenerTodosAsync();
}
