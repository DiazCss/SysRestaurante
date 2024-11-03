using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class MesasDTO
    {
        [Required]
        public int Id { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
        public string Descripcion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser un valor positivo.")]
        public int Capacidad { get; set; }

        [Required]
        public byte Disponibilidad { get; set; }
    }
}
