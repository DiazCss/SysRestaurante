using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class InventarioDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [MaxLength(255, ErrorMessage = "La descripción no debe exceder los 255 caracteres.")]
        public string Descripcion { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad disponible debe ser un valor positivo.")]
        public int CantidadDisponible { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "La unidad de medida no debe exceder los 50 caracteres.")]
        public string UnidadMedida { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad mínima debe ser un valor positivo.")]
        public int CantidadMinima { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El costo unitario debe ser un valor positivo.")]
        public decimal CostoUnitario { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaUltimaCompra { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaCaducidadLote { get; set; }
    }
}
