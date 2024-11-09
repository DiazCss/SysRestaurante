using System;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.CategoriaPlatilloDTOs;

namespace SysRestaurante.BL.Interfaces;

public interface ICategoriaPlatilloBL
{
     Task<int> CreateAsync(CategoriaPlatilloMantDTOs pCategoriaPlatilloDTO);
        Task<int> ModificarAsync(CategoriaPlatilloMantDTOs pCategoriaPlatilloDTO);
        Task<int> EliminarAsync(CategoriaPlatilloMantDTOs pCategoriaPlatilloDTO);
        Task<CategoriaPlatilloMantDTOs> ObtenerPorIdAsync(CategoriaPlatilloMantDTOs pCategoriaPlatilloDTO);
        Task<PaginacionOutputDTO<List<CategoriaPlatilloMantDTOs>>> BuscarAsync(CategoriaPlatilloBuscarDTOs pCategoriaPlatilloDTO);
        Task<List<CategoriaPlatilloMantDTOs>> ObtenerTodosAsync();
}
