using System;

namespace Verkoop.CapaDatos.DTO
{
    public class VisualizarDetallesProductoAdministradorDTO
    {
        public string cDescripcion { get; set; }
        public string cImagen { get; set; }
        public string cNombreCategoria { get; set; }
        public string cNombreProducto { get; set; }
        public decimal dPrecio { get; set; }
        public DateTime dtFechaAlta { get; set; }
        public DateTime dtFechaBaja { get; set; }
        public DateTime dtFechaModificacion { get; set; }
        public int iCantidad { get; set; }
        public bool iEstatus { get; set; }
        public int iIdProducto { get; set; }

    }
}
