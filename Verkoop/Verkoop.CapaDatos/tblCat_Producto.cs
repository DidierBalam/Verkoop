//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Verkoop.CapaDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCat_Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCat_Producto()
        {
            this.tblCarrito = new HashSet<tblCarrito>();
            this.tblProductoComprado = new HashSet<tblProductoComprado>();
        }
    
        public int iIdProducto { get; set; }
        public int iIdCategoria { get; set; }
        public int iCantidad { get; set; }
        public string cNombre { get; set; }
        public string cDescripcion { get; set; }
        public string cImagen { get; set; }
        public decimal dPrecio { get; set; }
        public bool lEstatus { get; set; }
        public Nullable<System.DateTime> dtFechaAlta { get; set; }
        public Nullable<System.DateTime> dtFechaBaja { get; set; }
        public Nullable<System.DateTime> dtFechaModificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCarrito> tblCarrito { get; set; }
        public virtual tblCat_Categoria tblCat_Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProductoComprado> tblProductoComprado { get; set; }
    }
}