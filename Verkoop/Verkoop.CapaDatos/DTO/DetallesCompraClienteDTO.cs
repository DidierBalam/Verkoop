using System;
using System.Collections.Generic;

namespace Verkoop.Business
{
    public class DetallesCompraClienteDTO
    {
        public string cDireccion { get; set; }
        public string cNombreCliente { get; set; }
        public decimal dPrecio { get; set; }
        public DateTime dtFecha { get; set; }
        public int iTotalProductos { get; set; }
        
    }
}