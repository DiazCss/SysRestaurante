using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class RolDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El nombre no debe exceder los 50 caracteres.")]
        public string Nombre { get; set; }
    }
}
