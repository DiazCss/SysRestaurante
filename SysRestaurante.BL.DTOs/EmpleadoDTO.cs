using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class EmpleadoDTO
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

        [Required]
        public DateTime FechaContratacion { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El puesto no puede exceder los 50 caracteres.")]
        public string Puesto { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser un valor positivo.")]
        public decimal Salario { get; set; }

        [Required]
        public byte Estado { get; set; }
    }
}
