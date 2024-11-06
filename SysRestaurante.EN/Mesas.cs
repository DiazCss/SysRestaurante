using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class Mesas
    {
        
            public int Id { get; set; }
            public string NumeroMesa { get; set; }
            public string Descripcion { get; set; }
            public int Capacidad { get; set; }
            public byte Disponibilidad { get; set; }

        public enum Disponibilidad_Mesa
        {
            Disponible = 1,
            NoDisponible = 0,
        }


    }
}
