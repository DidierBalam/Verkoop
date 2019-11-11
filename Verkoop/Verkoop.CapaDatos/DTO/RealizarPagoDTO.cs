
using System.Collections.Generic;


namespace Verkoop.CapaDatos.DTO
{
    public class RealizarPagoDTO
    {
        private int iIdCompra { get; set; }
        private int iIdDireccion { get; set; }
        private int iIdTarjeta { get; set; }
        private int iIdUsuario { get; set; }
        private List<ProductoCompradoDTO> objProductoComprado { get; set; }
    }
}
