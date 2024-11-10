using System;
using System.ComponentModel.DataAnnotations;

namespace SysRestaurante.BL.DTOs.CategoriaPlatilloDTOs
{
    public class CategoriaPlatilloMantDTOs
    {
        [Key] // Indica que este es el campo clave de la entidad
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
        public string Descripcion { get; set; }
    }
}
