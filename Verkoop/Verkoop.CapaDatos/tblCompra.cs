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
    
    public partial class tblCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCompra()
        {
            this.tblProductoComprado = new HashSet<tblProductoComprado>();
        }
    
        public int iIdCompra { get; set; }
        public int iIdUsuario { get; set; }
        public int iIdDireccion { get; set; }
        public Nullable<System.DateTime> dtFecha { get; set; }
    
        public virtual tblCat_Usuario tblCat_Usuario { get; set; }
        public virtual tblDireccion tblDireccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProductoComprado> tblProductoComprado { get; set; }
    }
}
