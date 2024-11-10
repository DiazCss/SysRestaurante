using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysRestaurante.BL.DTOs.InventarioDTOs;

public class InventarioMantDTO
{
        public int Id { get; set; }

    [Required]
    public int IdProducto { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "La cantidad disponible debe ser un número positivo.")]
    [Display(Name = "Cantidad Disponible")]
    public int CantidadDisponible { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "La cantidad mínima debe ser un número positivo.")]
    [Display(Name = "Cantidad Minima")]
    public int CantidadMinima { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Costo unitario")]
    [Range(0.0, double.MaxValue, ErrorMessage = "El costo unitario debe ser un número positivo.")]
    public decimal CostoUnitario { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Última Compra")]
    public DateTime FechaUltimaCompra { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Caducidad del Lote")]
    public DateTime FechaCaducidadLote { get; set; }
}
