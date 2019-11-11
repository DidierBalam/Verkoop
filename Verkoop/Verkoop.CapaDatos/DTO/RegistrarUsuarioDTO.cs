using System;

namespace Verkoop.CapaDatos.DTO
{
    public class RegistrarUsuarioDTO
    {
        private string cApellidoMaterno { get; set; }
        private string cApellidoPaterno { get; set; }
        private string cCodigoPostal { get; set; }
        private string cContrasenia { get; set; }
        private string cCorreo { get; set; }
        private string cDireccion { get; set; }
        private string cNombre { get; set; }
        private string cTelefono { get; set; }
        private DateTime dtFechaIngreso { get; set; }
        private int iIdMunicipio { get; set; }
        private int iIdUsuario { get; set; }
        private bool lDefault { get; set; }
    }
}
