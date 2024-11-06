using SysRestaurante.BL.DTOs;
using SysRestaurante.BL.DTOs.MesaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IMesasBL
    {
        public Task<int> CreateAsync(MesasMantDTO pMesaDTO);
        public Task<int> ModificarAsync(MesasMantDTO pMesaDTO);
        public Task<int> EliminarAsync(MesasMantDTO pMesaDTO);
        public Task<MesasMantDTO> ObtenerPorIdAsync(MesasMantDTO pMesaDTO);
        public Task<PaginacionOutputDTO<List<MesasMantDTO>>> BuscarAsync(MesasBuscarDTO pMesaDTO);
        public Task<List<MesasMantDTO>> ObtenerTodosAsync();
    }
}
