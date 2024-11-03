using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class Mesa
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Capacidad { get; set; }

        public byte[] Disponibilidad { get; set; }
    }

    public enum Disponibilidad_Mesas
    {
        disponible = 0,
        NoDisponible = 1,
    }
}
