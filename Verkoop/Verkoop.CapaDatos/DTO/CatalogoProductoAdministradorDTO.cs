using System;

namespace Verkoop.CapaDatos.DTO
{
    class CatalogoProductoAdministradorDTO
    {

        private string cNombre { get; set; }
        private string cNombreCategoria { get; set; }
        private decimal dPrecio { get; set; }
        private DateTime dtFechaAlta { get; set; }
        private DateTime dtFechaBaja { get; set; }
        private DateTime dtFechaModificacion { get; set; }
        private int iCantidad { get; set; }
        private int iIdProducto { get; set; }
    }
}
