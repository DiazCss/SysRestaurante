using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.UsuarioDTOs
{
    public class UsuarioBuscarDTO : PaginacionInputDTO
    {
        public string Nombre_Empleado_Like { get; set; }
        public string Apellido_Empleado_Like { get; set; }
        public string Email_Usuario_Like { get; set; }
    }
}
