//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Verkoop.CapaDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCat_Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCat_Usuario()
        {
            this.tblCarrito = new HashSet<tblCarrito>();
            this.tblDireccion = new HashSet<tblDireccion>();
            this.tblTarjeta = new HashSet<tblTarjeta>();
            this.tblComentario = new HashSet<tblComentario>();
            this.tblValoracion = new HashSet<tblValoracion>();
            this.tblSesion = new HashSet<tblSesion>();
            this.tblCompra = new HashSet<tblCompra>();
        }
    
        public int iIdUsuario { get; set; }
        public string cNombre { get; set; }
        public string cApellidoPaterno { get; set; }
        public string cApellidoMaterno { get; set; }
        public string cImagen { get; set; }
        public string cTelefono { get; set; }
        public int iTipoUsuario { get; set; }
        public bool lEstatus { get; set; }
        public System.DateTime dtFechaIngreso { get; set; }
        public Nullable<System.DateTime> dtFechaModificacion { get; set; }
        public Nullable<System.DateTime> dtFechaBaja { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCarrito> tblCarrito { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDireccion> tblDireccion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTarjeta> tblTarjeta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblComentario> tblComentario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblValoracion> tblValoracion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSesion> tblSesion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCompra> tblCompra { get; set; }
    }
}
