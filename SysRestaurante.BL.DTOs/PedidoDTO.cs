using System;
using System.ComponentModel.DataAnnotations;

namespace SysRestaurante.BL.DTOs
{
    public class PedidoDTO
    {
        [Required(ErrorMessage = "El Id es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ClienteId es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ClienteId debe ser un valor positivo.")]
        public int? ClienteId { get; set; }

        [Required(ErrorMessage = "La FechaHoraPedido es obligatoria.")]
        public DateTime? FechaHoraPedido { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Range(0, 255, ErrorMessage = "El estado debe estar entre 0 y 255.")]
        public byte Estado { get; set; }

        [StringLength(250, ErrorMessage = "Los comentarios no pueden exceder los 250 caracteres.")]
        public string Comentarios { get; set; }

        [Required(ErrorMessage = "El MesaId es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El MesaId debe ser un valor positivo.")]
        public int? MesaId { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        public decimal? Total { get; set; }

        [Required(ErrorMessage = "La FechaHoraEntrega es obligatoria.")]
        public DateTime? FechaHoraEntrega { get; set; }
    }
}
