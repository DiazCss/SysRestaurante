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
        public int PedidoId { get; set; }
        public DateTime FechaEmision { get; set; }
        public int ClienteId { get; set; }
        public decimal Total { get; set; }
        public string MetodoDePago { get; set; }
        public string NumeroFactura { get; set; }

        public virtual Pedido Pedido { get; set; } 
        public virtual Cliente Cliente { get; set; } 
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } 
    }


}
