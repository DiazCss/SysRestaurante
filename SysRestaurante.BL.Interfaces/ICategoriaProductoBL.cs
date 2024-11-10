using System;
using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.CategoriaProductoDTOs;

namespace SysRestaurante.BL.Interfaces;

public interface ICategoriaProductoBL
{
    public Task<int> CreateAsync(CategoriaProductoMantDTO pCategoriaProductoMantDTO);
        public Task<int> ModificarAsync(CategoriaProductoMantDTO pCategoriaProductoMantDTO);
        public Task<int> EliminarAsync(CategoriaProductoMantDTO pCategoriaProductoMantDTO);
        public Task<CategoriaProductoMantDTO> ObtenerPorIdAsync(CategoriaProductoMantDTO pCategoriaProductoMantDTO);
        public Task<PaginacionOutputDTO<List<CategoriaProductoMantDTO>>> BuscarAync(CategoriaProductoBuscarDTO pCategoriaProductoBuscarDTO);
        public Task<List<CategoriaProductoMantDTO>> ObtenerTodosAsync();
}
