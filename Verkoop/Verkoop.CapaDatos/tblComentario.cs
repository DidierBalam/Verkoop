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
    
    public partial class tblComentario
    {
        public int iIdComentario { get; set; }
        public int iIdUsuario { get; set; }
        public int iIdProducto { get; set; }
        public string cComentario { get; set; }
        public Nullable<System.DateTime> dtFecha { get; set; }
    
        public virtual tblCat_Producto tblCat_Producto { get; set; }
        public virtual tblCat_Usuario tblCat_Usuario { get; set; }
    }
}