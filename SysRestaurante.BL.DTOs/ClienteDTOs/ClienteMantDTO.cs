using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.ClienteDTOs
{
    public class ClienteMantDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [MaxLength(50, ErrorMessage = "El correo electrónico no puede exceder los 50 caracteres.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        [MaxLength(50, ErrorMessage = "El número de teléfono no puede exceder los 50 caracteres.")]
        public string Telefono { get; set; }

        // No es necesario incluir colecciones de facturas y pedidos en el DTO
        // a menos que se deseen incluir los detalles en la respuesta.
        // public virtual ICollection<FacturaDTO> Facturas { get; set; } 
        // public virtual ICollection<PedidoDTO> Pedidos { get; set; } 
    }
}
