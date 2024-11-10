using System;
using System.ComponentModel.DataAnnotations;

namespace SysRestaurante.BL.DTOs.CategoriaProductoDTOs;

public class CategoriaProductoMantDTO
{
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "El Nombre es obligatorio.")]
    public string Nombre { get; set; }

    [StringLength(100, ErrorMessage = "El campo Descripción no puede tener más de 100 caracteres.")]
    public string Descripcion { get; set; }
}
