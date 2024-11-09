using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.UsuarioDTOs
{
    public class UsuarioBuscarDTO : PaginacionInputDTO
    {
        public string Nombre_Usuario_Like { get; set; }
        public string Apellido_Usuario_Like { get; set; }
        public string Email_Usuario_Like { get; set; }

        [Display(Name = "Rol")]
        public int IdRol_equal { get; set; }
    }
}
