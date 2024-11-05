using SysRestaurante.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IMesasBL
    {
        public Task<int> CreateAsync(MesasDTO pMesaDTO);
        public Task<int> ModificarAsync(MesasDTO pMesaDTO);
        public Task<int> EliminarAsync(MesasDTO pMesaDTO);
        public Task<MesasDTO> ObtenerPorIdAsync(MesasDTO pMesaDTO);
        public Task<PaginacionOutputDTO<List<MesasDTO>>> BuscarAsync(MesasDTO pMesaDTO);
        public Task<List<MesasDTO>> ObtenerTodosAsync();
    }
}
