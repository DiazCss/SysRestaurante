using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class ClienteDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)] 
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)] 
        public string Email { get; set; }

        [Phone] 
        [MaxLength(50)] 
        public string Telefono { get; set; }

        // No es necesario incluir colecciones de facturas y pedidos en el DTO
        // a menos que se deseen incluir los detalles en la respuesta.
        // public virtual ICollection<FacturaDTO> Facturas { get; set; } 
        // public virtual ICollection<PedidoDTO> Pedidos { get; set; } 
    }
}
