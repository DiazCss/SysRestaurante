using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.EmpleadoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IEmpleadoBL
    {
        public Task<int> CreateAsync(EmpleadoMantDTO pEmpleadoDTO);
        public Task<int> ModificarAsync(EmpleadoMantDTO pEmpleadoDTO);
        public Task<int> EliminarAsync(EmpleadoMantDTO pEmpleadoDTO);
        public Task<EmpleadoMantDTO> ObtenerPorIdAsync(EmpleadoMantDTO pEmpleadoDTO);
        public Task<PaginacionOutputDTO<List<EmpleadoMantDTO>>> BuscarAsync(EmpleadoBuscarDTO pEmpleadoDTO);
        public Task<List<EmpleadoMantDTO>> ObtenerTodosAsync();
    }
}
