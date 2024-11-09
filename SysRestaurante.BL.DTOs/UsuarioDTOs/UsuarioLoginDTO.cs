using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.UsuarioDTOs
{
    public class UsuarioLoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [StringLength(15, ErrorMessage = "La contraseña no puede exceder los 15 caracteres.")]
        public string Password { get; set; }
    }
}
