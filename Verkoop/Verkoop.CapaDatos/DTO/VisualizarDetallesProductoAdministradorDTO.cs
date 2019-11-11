using System;

namespace Verkoop.CapaDatos.DTO
{
    class VisualizarDetallesProductoAdministradorDTO
    {
        private string cDescripcion { get; set; }
        private string cImagen { get; set; }
        private string cNombreCategoria { get; set; }
        private string cNombreProducto { get; set; }
        private decimal dPrecio { get; set; }
        private DateTime dtFechaAlta { get; set; }
        private DateTime dtFechaBaja { get; set; }
        private DateTime dtFechaModificacion { get; set; }
        private int iCantidad { get; set; }
        private bool iEstatus { get; set; }
        private int iIdProducto { get; set; }

    }
}
