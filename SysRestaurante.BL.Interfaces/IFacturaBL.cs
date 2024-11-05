using SysRestaurante.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.Interfaces
{
    public interface IFacturaBL
    {
        public Task<int> CreateAsync(FacturaDTO pFacturaDTO );
        public Task<int> CreateWithDetailsAsync(FacturaDTO pFacturaDTO, List<DetalleFacturaDTO> detalles);
        public Task<int> ModificarAsync(FacturaDTO pFacturaDTO);
        public Task<int> EliminarAsync(FacturaDTO pFacturaDTO);
        public Task<FacturaDTO> ObtenerPorIdAsync(FacturaDTO pFacturaDTO);
        public Task<PaginacionOutputDTO<List<FacturaDTO>>> BuscarAsync(FacturaDTO pFacturaDTO);
        public Task<List<FacturaDTO>> ObtenerTodosAsync();
    }
}
