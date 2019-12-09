using System;

namespace Verkoop.CapaDatos.DTO
{
    public class BusquedaUsuarioPorEstadoDTO
    {
        public string cNombre { get; set; }
        public string cTelefono { get; set; }
        public DateTime dtFechaBaja { get; set; }
        public DateTime dtFechaIngreso { get; set; }
        public int iIdUsuario { get; set; }
    }
}
