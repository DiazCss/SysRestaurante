using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.ClienteDTOs
{
    public class ClienteBuscarDTO : PaginacionInputDTO
    {
       public string Nombre_Cliente_Like {  get; set; }
        public string Apellido_Cliente_Like {get; set; }
        public string Email_Cliente_Like { get; set;}

    }
}
