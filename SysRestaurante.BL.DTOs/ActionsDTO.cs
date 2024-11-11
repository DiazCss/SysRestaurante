using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.BL.DTOs
{
    public class ActionsDTO
    {
        /// <summary>
        /// Usar el enum de Actions_enum par saber la accion  a utilizar
        /// </summary>
        public byte Accion_aux { get; set; }

    }
    public enum Actions_enum
    {
        NUEVO = 1,
        MODIFICAR = 2,
        ELIMINAR = 3
    }
}
