using System.ComponentModel.DataAnnotations;

namespace SysRestaurante.BL.DTOs
{
    public class MesasDTO
    {
        [Required(ErrorMessage = "El Id es obligatorio.")]
        public int Id { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La capacidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser un valor positivo.")]

        public int Capacidad { get; set; }

        [Required(ErrorMessage = "La disponibilidad es obligatoria.")]
        public byte Disponibilidad { get; set; }
    }
}
