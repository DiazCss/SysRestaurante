using SysRestaurante.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SysRestaurante.BL.DTOs.PaginacionDTO;

namespace SysRestaurante.BL.Interfaces
{
    public interface IEmpleadoBL
    {
        public Task<int> CreateAsync(EmpleadoDTO pEmpleadoDTO);
        public Task<int> ModificarAsync(EmpleadoDTO pEmpleadoDTO);
        public Task<int> EliminarAsync(EmpleadoDTO pEmpleadoDTO);
        public Task<EmpleadoDTO> ObtenerPorIdAsync(EmpleadoDTO pEmpleadoDTO);
        public Task<PaginacionOutputDTO<List<EmpleadoDTO>>> BuscarAsync(EmpleadoDTO pEmpleadoDTO);
        public Task<List<EmpleadoDTO>> ObtenerTodosAsync();
    }
}
