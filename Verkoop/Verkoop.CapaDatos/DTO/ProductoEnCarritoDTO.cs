

using System;
using System.Collections.Generic;

namespace Verkoop.CapaDatos.DTO
{
    public class ProductoEnCarritoDTO
    {
        public int iIdProducto { get; set; }
        public string cImagenCarrito { get; set; }
        public string cNombreproducto { get; set; }
        public decimal dPrecioProducto { get; set; }
        public int iIdCarrito { get; set; }
        public int iCantidad { get; set; }

        public static List<ProductoEnCarritoDTO> ToList()
        {
            throw new NotImplementedException();
        }
    }
}
