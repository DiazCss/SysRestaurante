using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class ProveedorDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El contacto no debe exceder los 100 caracteres.")]
        public string Contacto { get; set; }

        [MaxLength(255, ErrorMessage = "La dirección no debe exceder los 255 caracteres.")]
        public string Direccion { get; set; }
    }
}
