

namespace Verkoop.CapaDatos.DTO
{
    class ProductoCompradoDTO
    {
        private int iCantidad { get; set; }
        private int iIdCompra { get; set; }
        private int iIdProducto { get; set; }
        private int iIdProductoComprado { get; set; }

        public RealizarPagoDTO m_RealizarPagoDTO;
    }
}
