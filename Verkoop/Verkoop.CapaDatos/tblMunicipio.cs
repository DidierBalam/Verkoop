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
    
    public partial class tblMunicipio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblMunicipio()
        {
            this.tblDireccion = new HashSet<tblDireccion>();
        }
    
        public int iIdMunicipio { get; set; }
        public int iIdEstado { get; set; }
        public string cNombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDireccion> tblDireccion { get; set; }
        public virtual tblEstado tblEstado { get; set; }
    }
}
