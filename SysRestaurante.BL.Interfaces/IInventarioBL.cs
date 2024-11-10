using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.InventarioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
public interface IInventarioBL
    {
    public Task<int> CreateAsync(InventarioMantDTO pInventarioMantDTO);
    public Task<int> ModificarAsync(InventarioMantDTO pInventarioMantDTO);
    public Task<int> EliminarAsync(InventarioMantDTO pInventarioMantDTO);
    public Task<InventarioMantDTO> ObtenerPorIdAsync(InventarioMantDTO pInventarioMantDTO);
    public Task<PaginacionOutputDTO<List<InventarioMantDTO>>> BuscarAsync(InventarioBuscarDTO pInventarioBuscarDTO);
    public Task<List<InventarioMantDTO>> ObtenerTodosAsync();
    }

}
