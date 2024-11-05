using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs.RolDTOs
{
    public class RolMantDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string Nombre { get; set; }
    }
}
