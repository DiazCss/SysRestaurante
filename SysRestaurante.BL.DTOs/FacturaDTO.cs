using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class FacturaDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaEmision { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(50)]
        public string MetodoDePago { get; set; }

        [Required]
        [MaxLength(20)]
        public string NumeroFactura { get; set; }


        // En caso de querer incluir la relación con Pedido o Cliente:
        // public PedidoDTO Pedido { get; set; }
        // public ClienteDTO Cliente { get; set; }

        // Si deseas incluir los detalles de la factura en el DTO
        // public ICollection<DetalleFacturaDTO> DetalleFacturas { get; set; }
    }
}
