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
        public int IdPedido { get; set; }
        public int IdPlatillo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string? Comentario { get; set; }

        public Platillo? Platillo { get; set; }
    }
}
