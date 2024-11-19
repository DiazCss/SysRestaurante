using System;

namespace SysRestaurante.EN
{
    public class Platillo
    {
        public int Id { get; set; }              
        public string Nombre { get; set; }       
        public string Descripcion { get; set; }  
        public decimal Precio { get; set; }      
        public byte Disponibilidad { get; set; }  
        public TimeSpan TiempoPreparacion { get; set; } 
        public string IngredientePrincipal { get; set; } 
        public DateTime FechaActualizacion { get; set; } 
        public int IdCategoria { get; set; }     

        public CategoriaPlatillo CategoriaPlatillos { get; set; }
         public ICollection<PlatilloImagen> PlatilloImagenes { get; set; } 

    }

    public enum Disponibilidad_Platillo
    {
        Disponible = 0,
        NoDisponible = 1,
    }
}
