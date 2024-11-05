using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.EmpleadoDTOs
{
    public class EmpleadoBuscarDTO : PaginacionInputDTO //Heredar atributos de la clase Base PaginacionDTO
    {
        public string Nombre_Empleado_Like { get; set; }
        public string Apellido_Empleado_Like { get; set; }
        public string Estado_Equal { get; set; }
    }

}
