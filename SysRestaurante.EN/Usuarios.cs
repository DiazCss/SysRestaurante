using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class Usuarios
    {
        public int Id { get; set; }
        public int IdDatosPersonales { get; set; }
        public string Clave { get; set; }
        public int IdRol { get; set; }
        public DatosPersonales datosPersonales { get; set; }

        public Rol? rol { get; set; }

    }
}
