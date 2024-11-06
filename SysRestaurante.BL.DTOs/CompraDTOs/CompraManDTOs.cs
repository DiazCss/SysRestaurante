using System;
using System.ComponentModel.DataAnnotations;

namespace SysRestaurante.BL.DTOs.CompraDTOs;

public class CompraManDTOs
{

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El número de factura no puede exceder los 20 caracteres.")]
        public string NumeroFactura { get; set; }
        
        [Required]
        public DateTime Fecha {get; set;}

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El IVA debe ser un valor positivo.")]
        public decimal Iva { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        public decimal Total { get; set; }

        [Required]
        public int IdProveedor { get; set; }
}
