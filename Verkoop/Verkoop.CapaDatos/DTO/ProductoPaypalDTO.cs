namespace Verkoop.CapaDatos.DTO
{
    /// <summary>
    /// Propiedades principales de la lista de productos en carrito.
    /// </summary>
    public class ProductoPaypalDTO
    {
        /// <summary>
        /// Cantidad de productos individuales en carrito.
        /// </summary>
        public int iCantidad { get; set; }

        public int iIdProducto { get; set; }

        /// <summary>
        /// Precio individual de los productos en carrito.
        /// </summary>
        public decimal dPrecio { get; set; }

        /// <summary>
        /// Nombre de cada producto individual en carrito.
        /// </summary>
        public string cNombre { get; set; }
    }

}
