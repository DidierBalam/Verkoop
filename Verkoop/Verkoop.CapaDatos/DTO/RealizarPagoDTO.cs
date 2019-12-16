
using System.Collections.Generic;


namespace Verkoop.CapaDatos.DTO
{
    public class RealizarPagoDTO
    {
        public int iIdDireccion { get; set; }
        public tblTarjeta objTarjeta { get; set; }
        public List<tblProductoComprado> lstProductoComprado { get; set; }

    }
}
