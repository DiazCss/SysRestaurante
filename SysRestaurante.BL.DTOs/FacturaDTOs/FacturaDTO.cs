using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.FacturaDTOs
{
    public class FacturaDTO
    {
        public int Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal Total { get; set; }
        public string MetodoDePago { get; set; }
        public string NumeroFactura { get; set; }

        public List<DetalleFacturaDTO> DetallesFactura { get; set; }
    }

}
