using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class PedidoDTO
    {
        [Required]
        public int Id { get; set; }

        public int? ClienteId { get; set; }

        public DateTime? FechaHoraPedido { get; set; }

        [Required]
        public byte Estado { get; set; }

        [StringLength(250, ErrorMessage = "Los comentarios no pueden exceder los 250 caracteres.")]
        public string Comentarios { get; set; }

        public int? MesaId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        public decimal? Total { get; set; }

        public DateTime? FechaHoraEntrega { get; set; }
    }
}
