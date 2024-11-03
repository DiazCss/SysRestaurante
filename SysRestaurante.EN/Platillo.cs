using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class Platillo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Disponibilidad { get; set; }
        public TimeSpan TiempoPreparacion { get; set; }
        public string IngredientePrincipal { get; set; }
        public DateTime FechaActualizacion { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } 
        public virtual ICollection<PedidoPlatillo> PedidoPlatillos { get; set; } 
        public virtual ICollection<PlatilloImagen> PlatilloImagenes { get; set; } 
    }


}
