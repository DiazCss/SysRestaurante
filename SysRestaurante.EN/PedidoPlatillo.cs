using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class PedidoPlatillo
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int PlatilloId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Comentario { get; set; }

        public virtual Pedido Pedido { get; set; } 
        public virtual Platillo Platillo { get; set; } 
    }


}
