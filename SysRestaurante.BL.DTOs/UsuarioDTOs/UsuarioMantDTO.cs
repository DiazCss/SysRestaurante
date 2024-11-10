using SysRestaurante.BL.DTOs.RolDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.UsuarioDTOs
{
    public class UsuarioMantDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public string Email { get; set; }

        [StringLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
        public string Telefono { get; set; }


        [Display(Name = "Rol")]
        [DefaultValue("2")]
        public int IdRol { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [StringLength(15, ErrorMessage = "La contraseña no puede exceder los 15 caracteres.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password y confirmar password deben de ser iguales")]
        [Display(Name = "Confirmar password")]
        public string ConfirmPassword { get; set; }

        public String Token { get; set; }

        public RolMantDTO? Rol { get; set; }

    }
}
