using System;

namespace Verkoop.CapaDatos.DTO
{
    public class RegistrarUsuarioDTO
    {
        public string cApellidoMaterno { get; set; }
        public string cApellidoPaterno { get; set; }
        public string cCodigoPostal { get; set; }
        public string cContrasenia { get; set; }
        public string cCorreo { get; set; }
        public string cDireccion { get; set; }
        public string cNombre { get; set; }
        public string cTelefono { get; set; }
        public DateTime dtFechaIngreso { get; set; }
        public int iIdMunicipio { get; set; }
        public int iIdUsuario { get; set; }
        public bool lDefault { get; set; }
    }
}
