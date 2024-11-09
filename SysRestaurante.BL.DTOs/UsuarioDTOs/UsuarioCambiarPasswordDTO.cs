using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.UsuarioDTOs
{
    public class UsuarioCambiarPasswordDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Password actual es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Password actual debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        [Display(Name = "Contraseña Actual")]
        public string PasswordActual { get; set; }
        [Required(ErrorMessage = "Password es obligatorio")]
        [DataType(DataType.Password)]
        [StringLength(80, ErrorMessage = "Password debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        [Display(Name = "Nueva Contraseña")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmar el password")]
        [StringLength(80, ErrorMessage = "Password debe estar entre 5 a 32 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password y confirmar password deben de ser iguales")]
        [Display(Name = "Confirmar Contraseña nueva")]
        public string ConfirmPassword { get; set; }
    }
}
