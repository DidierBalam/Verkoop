

namespace Verkoop.CapaDatos.DTO
{
    public class DireccionDTO
    {
        private string cCodigoPostal { get; set; }
        private string cDireccion { get; set; }
        private int iIdMunicipio { get; set; }
        private int iIdUsuario { get; set; }

        public RegistrarUsuarioDTO m_RegistrarUsuarioDTO;
    }
}
