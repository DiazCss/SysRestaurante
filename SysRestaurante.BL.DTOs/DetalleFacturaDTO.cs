using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class DetalleFacturaDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int FacturaId { get; set; }

        [Required]
        public int PlatilloId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor positivo.")]
        public int Cantidad { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que cero.")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El subtotal debe ser mayor que cero.")]
        public decimal Subtotal { get; set; }
    }
}
