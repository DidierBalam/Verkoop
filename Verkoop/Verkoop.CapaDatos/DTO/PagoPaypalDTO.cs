using System.Collections.Generic;

namespace Verkoop.CapaDatos.DTO
{
    /// <summary>
    /// Contiene las propiedades que se envían a paypal.
    /// </summary>
    public class PagoPaypalDTO
    {
        /// <summary>
        /// Precio total de todos los productos.
        /// </summary>
        public decimal dPrecioTotal { get; set; }

        /// <summary>
        /// Lista de los productos.
        /// </summary>
        public List<ProductoPaypalDTO> lstProducto { get; set; }

        /// <summary>
        /// Constructo que inicializa lstProducto.
        /// </summary>
        public PagoPaypalDTO()
        {
            lstProducto = new List<ProductoPaypalDTO>();
        }
    }
}
