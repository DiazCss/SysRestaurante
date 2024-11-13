using System;
using System.ComponentModel.DataAnnotations;

namespace SysRestaurante.BL.DTOs.PlatilloProductoDTOs
{
    public class PlatilloProductoMantDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del platillo es obligatorio.")]
        public int IdPlatillo { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad usada es obligatoria.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad usada debe ser un valor positivo.")]
        public decimal CantidadUsada { get; set; }
        
         public string NombrePlatillo { get; set; }
    public string NombreProducto { get; set; }
    }
}
