using System;

namespace SysRestaurante.BL.DTOs.CategoriaPlatilloDTOs;

public class CategoriaPlatilloBuscarDTOs : PaginacionInputDTO
{
        public string Nombre_CategoriaPlatillo_Like { get; set; }
        public string Descripcion_CategoriaPlatillo_Like { get; set; }
}
