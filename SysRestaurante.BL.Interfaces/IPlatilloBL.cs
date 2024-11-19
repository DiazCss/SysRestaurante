using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.PlatilloDTOs;
using SysRestaurante.BL.DTOs.PlatilloDTOs.SysRestaurante.BL.DTOs.PlatilloDTOs;
using SysRestaurante.EN;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IPlatilloBL
   {
       public Task<int> CreateAsync(PlatilloMantDTO pPlatilloMantDTO);
       public Task<int> ModificarAsync(PlatilloMantDTO pPlatilloMantDTO);
       public Task<int> EliminarAsync(PlatilloMantDTO pPlatilloMantDTO);
      public Task<PlatilloMantDTO> ObtenerPorIdAsync(PlatilloMantDTO pPlatilloMantDTO);
       public Task<PaginacionOutputDTO<List<PlatilloMantDTO>>> BuscarAsync(PlatilloBuscarDTO pPlatilloMantDTO);
       public Task<List<PlatilloMantDTO>> ObtenerTodosAsync();
        public Task<List<PlatilloIndexDTO>> ObtenerPlatillosIndexAsync();
        

    }
}
