using System;
using System.ComponentModel.DataAnnotations;
using SysRestaurante.EN;

namespace SysRestaurante.BL.DTOs.CompraDTOs
{
        public class CompraManDTOs
{
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo IdCompra es obligatorio.")]
        public int IdCompra { get; set; }

        [Required(ErrorMessage = "El campo IdProducto es obligatorio.")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario debe ser un valor positivo.")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un valor mayor a 0.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El subtotal es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El subtotal debe ser un valor positivo.")]
        public decimal SubTotal { get; set; }

        [Required(ErrorMessage = "El número de factura es obligatorio.")]
        [StringLength(20, ErrorMessage = "El número de factura no puede exceder los 20 caracteres.")]
        public string NumeroFactura { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El IVA es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El IVA debe ser un valor positivo.")]
        public decimal Iva { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El campo IdProveedor es obligatorio.")]
        public int IdProveedor { get; set; }

        public List<DetalleCompra> detalleComprass {get; set;}
}
}
