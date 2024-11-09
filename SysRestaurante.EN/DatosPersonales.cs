using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class DatosPersonales
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
        public string Apellido { get; set; } 
        public string Email { get; set; } 
        public string Telefono { get; set; } 

        public List<Empleado> Empleados { get; set;}
        public List<Usuarios> Usuarios { get; set; }

    }
}
