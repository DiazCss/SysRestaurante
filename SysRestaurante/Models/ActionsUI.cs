namespace SysRestaurante.Models
{
    public class ActionsUI
    {
        public int Accion { get; set; }
        public bool EsValidAction()
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO ||
                Accion == (int)ActionsUI_Enums.MODIFICAR ||
                Accion == (int)ActionsUI_Enums.ELIMINAR ||
                Accion == (int)ActionsUI_Enums.VER)
            {
                return true;
            }
            else
                return false;
        }
        public string ObtenerAccion()
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO)
                return "Create";
            else if (Accion == (int)ActionsUI_Enums.MODIFICAR)
                return "Edit";
            else if (Accion == (int)ActionsUI_Enums.ELIMINAR)
                return "Delete";
            else if (Accion == (int)ActionsUI_Enums.VER)
                return "Detail";
            return "";

        }
        public string ObtenerTitulo(string pTexto)
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO)
                return "Crear " + pTexto;
            else if (Accion == (int)ActionsUI_Enums.MODIFICAR)
                return "Modificar " + pTexto;
            else if (Accion == (int)ActionsUI_Enums.ELIMINAR)
                return "Eliminar " + pTexto;
            else if (Accion == (int)ActionsUI_Enums.VER)
                return "Ver " + pTexto;
            return "";

        }
        public string ObtenerNombreBoton()
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO)
                return "Crear";
            else if (Accion == (int)ActionsUI_Enums.MODIFICAR)
                return "Modificar";
            else if (Accion == (int)ActionsUI_Enums.ELIMINAR)
                return "Eliminar";
            else if (Accion == (int)ActionsUI_Enums.VER)
                return "Ir a modificar";
            return "";

        }
        public string ObtenerAccionJs()
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO)
                return "crear";
            else if (Accion == (int)ActionsUI_Enums.MODIFICAR)
                return "modificar";
            else if (Accion == (int)ActionsUI_Enums.ELIMINAR)
                return "eliminar";
            else if (Accion == (int)ActionsUI_Enums.VER)
                return "ver";
            return "";

        }
        public bool SiTraerDatos()
        {
            if (Accion == (int)ActionsUI_Enums.NUEVO)
                return false;
            else
                return true;
        }
    }
    public enum ActionsUI_Enums
    {
        NUEVO = 1,
        MODIFICAR = 2,
        ELIMINAR = 3,
        VER = 4,
        IMPRIMIR = 5
    }
}

