using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.PlatilloDTOs
{
    public class PlatilloBuscarDTO : PaginacionInputDTO 
    {
        public string Nombre_Platillo_Like { get; set; }
        public string IngredientePrincipal_Like { get; set; }
        public int? IdCategoria_Equal { get; set; }
        public byte? Disponibilidad_Equal { get; set; }
    }
}
