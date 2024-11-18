using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal Total { get; set; }
        public string MetodoDePago { get; set; }
        public string NumeroFactura { get; set; }

    }

}
