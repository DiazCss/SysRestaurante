using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class UsuarioDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La clave debe tener al menos 6 caracteres.")]
        public string Clave { get; set; }

        public RolDTO Rol { get; set; }
    }
}
