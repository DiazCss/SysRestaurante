using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.MesaDTOs
{
    public class MesasBuscarDTO : PaginacionInputDTO
    {
        public string NumeroMesa_Like { get; set; }
    }
}
