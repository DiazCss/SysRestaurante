﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysRestaurante.EN
{
    public class Empleado
    {
        public int Id { get; set; }
   
        public DateTime FechaContratacion { get; set; }
        public string Puesto { get; set; }
        public decimal Salario { get; set; }
        public byte Estado { get; set; }
        public DatosPersonales DatosPersonal { get; set; }
    }

    public enum Estado_Empleado
    {
        Activo = 0,
        Inactivo = 1,
    }

}
