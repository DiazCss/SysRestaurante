using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class Inventario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CantidadDisponible { get; set; }
        public string UnidadMedida { get; set; }
        public int CantidadMinima { get; set; }
        public decimal CostoUnitario { get; set; }
        public DateTime? FechaUltimaCompra { get; set; }
        public DateTime? FechaCaducidadLote { get; set; }
    }

}
