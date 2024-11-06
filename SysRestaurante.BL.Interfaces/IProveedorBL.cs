using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.ProveedorDTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
public interface IProveedorBL
    {
        public Task<int> CreateAsync(ProveedorMantDTO pProveedorDTO);
        public Task<int> ModificarAsync(ProveedorMantDTO pProveedorDTO);
        public Task<int> EliminarAsync(ProveedorMantDTO pProveedorDTO);
        public Task<ProveedorMantDTO> ObtenerPorIdAsync(ProveedorMantDTO pProveedorDTO);
        public Task<PaginacionOutputDTO<List<ProveedorMantDTO>>> BuscarAync(ProveedorBuscarDTO pProveedorDTO);
        public Task<List<ProveedorMantDTO>> ObtenerTodosAsync();
    }
}
