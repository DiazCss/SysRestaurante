using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.ProveedorDTOs
{
    public class ProveedorMantDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El número de teléfono es requerido.")]
        [StringLength(15, ErrorMessage = "El número de teléfono no puede exceder los 15 caracteres.")]
        public string Contacto { get; set; }

        [Required(ErrorMessage = "La dirección es requerida.")]
        [StringLength(100, ErrorMessage = "La dirección no puede exceder los 100 caracteres.")]
        public string Direccion { get; set; }
    }
}
