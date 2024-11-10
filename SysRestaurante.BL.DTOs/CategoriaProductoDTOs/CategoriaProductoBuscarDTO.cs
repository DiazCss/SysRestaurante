using System;

namespace SysRestaurante.BL.DTOs.CategoriaProductoDTOs;

public class CategoriaProductoBuscarDTO : PaginacionInputDTO
{
    public string Nombre_CategoriaProducto_Like { get; set; }

}
