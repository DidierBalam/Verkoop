

namespace Verkoop.CapaDatos.DTO
{
    public class TarjetaDTO
    {
        private string cAnioVigencia { get; set; }
        private string cMesVigencia { get; set; }
        private string cNumeroTarjeta { get; set; }
        private int iIdTarjeta { get; set; }
        private int iIdUsuario { get; set; }

        public RegistrarUsuarioDTO m_RegistrarUsuarioDTO;
    }
}
