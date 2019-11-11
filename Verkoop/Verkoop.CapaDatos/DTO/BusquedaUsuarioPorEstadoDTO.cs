using System;

namespace Verkoop.CapaDatos.DTO
{
    public class BusquedaUsuarioPorEstadoDTO
    {
        private bool bEstatus { get; set; }
        private string cApellidoMaterno { get; set; }
        private string cApellidoPaterno { get; set; }
        private string cNombre { get; set; }
        private string cTelefono { get; set; }
        private DateTime dtFechaBaja { get; set; }
        private DateTime dtFechaIngreso { get; set; }
        private int iIdUsuario { get; set; }
    }
}
