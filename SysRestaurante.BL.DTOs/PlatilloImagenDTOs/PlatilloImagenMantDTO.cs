using System.ComponentModel.DataAnnotations;

namespace SysRestaurante.BL.DTOs.PlatilloImagenDTOs
{
    public class PlatilloImagenMantDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID del platillo es obligatorio.")]
        public int IdPlatillo { get; set; }

        [Required(ErrorMessage = "La imagen del platillo es obligatoria.")]
        [StringLength(500, ErrorMessage = "La ruta de la imagen no puede exceder los 500 caracteres.")]
        public string ImagenPlatillo { get; set; } 
          public string NombrePlatillo { get; set; }
           public string DescripcionPlatillo { get; set; }
    }
}
