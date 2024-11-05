using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysRestaurante.BL.DTOs.ClienteDTOs;

namespace SysRestaurante.BL.DTOs
{
    public class FacturaDTO
    {

        [Required(ErrorMessage = "El Id es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El PedidoId es obligatorio.")]
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "La fecha de emisión es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha debe ser válida.")]
        public DateTime FechaEmision { get; set; }

        [Required(ErrorMessage = "El ClienteId es obligatorio.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El método de pago no puede exceder los 50 caracteres.")]
        public string MetodoDePago { get; set; }

        [Required(ErrorMessage = "El número de factura es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El número de factura no puede exceder los 20 caracteres.")]
        public string NumeroFactura { get; set; }


        // En caso de querer incluir la relación con Pedido o Cliente:
        public PedidoDTO Pedido { get; set; }
        public ClienteMantDTO Cliente { get; set; }

        // Si deseas incluir los detalles de la factura en el DTO
         public ICollection<DetalleFacturaDTO> DetalleFacturas { get; set; }
    }
}
