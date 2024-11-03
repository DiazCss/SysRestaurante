using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class Pedido
    {
        public int Id { get; set; }
        public int? ClienteId { get; set; }
        public DateTime? FechaHoraPedido { get; set; }
        public int? Estado { get; set; }
        public string Comentarios { get; set; }
        public int? MesaId { get; set; }
        public decimal? Total { get; set; }
        public DateTime? FechaHoraEntrega { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Mesa Mesa { get; set; } 
        public virtual ICollection<DetalleFactura> Facturas { get; set; } 
        public virtual ICollection<PedidoPlatillo> PedidoPlatillos { get; set; } 
    }


}
