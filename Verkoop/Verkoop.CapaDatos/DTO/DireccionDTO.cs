

namespace Verkoop.CapaDatos.DTO
{
    public class DireccionDTO
    {
        public int iIdDireccion { get; set; }
        public string cCodigoPostal { get; set; }
        public string cDireccion { get; set; }
        public string cMunicipio { get; set; }
        public string cEstado { get; set; }
        public bool bEstado { get; set; }
        public string cPais { get; set; }


        public RegistrarUsuarioDTO m_RegistrarUsuarioDTO;
    }
}
