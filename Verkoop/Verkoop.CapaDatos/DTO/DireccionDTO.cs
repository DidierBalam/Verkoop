

namespace Verkoop.CapaDatos.DTO
{
    public class DireccionDTO
    {
        public int iIdDireccion { get; set; }
        public string cCodigoPostal { get; set; }
        public string cDireccion { get; set; }
        public int iIdMunicipio { get; set; }
        public int iIdUsuario { get; set; }

        public RegistrarUsuarioDTO m_RegistrarUsuarioDTO;
    }
}
