using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Usuarios>? Usuario { get; set; }

    }
}
