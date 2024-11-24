using System;
using System.ComponentModel.DataAnnotations;

namespace SysRestaurante.BL.DTOs.ProductoDTOs
{
    public class ProductoManDTOs
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
        public string Nombre { get; set; }
        public string? Codigo { get; set; }

        [Required(ErrorMessage = "El contenido del empaque es obligatorio.")]
        [StringLength(50, ErrorMessage = "El contenido del empaque no debe exceder los 50 caracteres.")]
        public string ContenidoEmpaque { get; set; }

        [Required(ErrorMessage = "El ID de la categoría del producto es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la categoría debe ser un valor positivo.")]
        public int IdCategoriaProducto { get; set; }
         public string NombreCategoriaProducto { get; set; }
    }
}
