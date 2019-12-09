using System;

namespace Verkoop.CapaDatos.DTO
{
    public class CatalogoProductoAdministradorDTO
    {

        public string cNombre { get; set; }
        public string cNombreCategoria { get; set; }
        public decimal dPrecio { get; set; }
        public DateTime? dtFechaAlta { get; set; }
        public DateTime? dtFechaBaja { get; set; }
        public DateTime? dtFechaModificacion { get; set; }
        public int iCantidad { get; set; }
        public int iIdProducto { get; set; }
    }
}
