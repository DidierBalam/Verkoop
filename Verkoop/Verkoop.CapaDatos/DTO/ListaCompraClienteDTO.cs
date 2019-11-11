using System;

namespace Verkoop.CapaDatos.DTO
{
    public class ListaCompraClienteDTO
    {
        private string cNombreCliente { get; set; }
        private string cNombreCompra { get; set; }
        private decimal dPrecio { get; set; }
        private DateTime dtFecha { get; set; }
        private int iIdCompra { get; set; }
    }
}
