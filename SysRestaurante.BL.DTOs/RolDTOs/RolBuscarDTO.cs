using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.RolDTOs
{
    public class RolBuscarDTO : PaginacionInputDTO
    {
        public string Nombre_Like { get; set; }
    }
}
