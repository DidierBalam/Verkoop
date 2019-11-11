using System;
using System.Collections.Generic;

namespace Verkoop.CapaDatos.DTO
{
    public class DetallesCompraClienteDTO
    {
        private string cDireccion { get; set; }
        private string cNombreCliente { get; set; }
        private decimal dPrecio { get; set; }
        private DateTime dtFecha { get; set; }
        private int iTotalProductos { get; set; }
        private List<DetalleProductoCompradoClienteDTO> objProductoComprado { get; set; }

        public DetalleProductoCompradoClienteDTO m_DetalleProductoCompradoClienteDTO;
    }
}
