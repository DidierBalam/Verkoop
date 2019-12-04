
using System.Collections.Generic;


namespace Verkoop.CapaDatos.DTO
{
    public class RealizarPagoDTO
    {
        public int iIdCompra { get; set; }
        public int iIdDireccion { get; set; }
        public int iIdTarjeta { get; set; }
        public int iIdUsuario { get; set; }
        public List<int> lstProductoComprado { get; set; }
    }
}
