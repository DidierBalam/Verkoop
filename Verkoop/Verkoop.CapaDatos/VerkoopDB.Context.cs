﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VerkoopDBEntities : DbContext
    {
        public VerkoopDBEntities()
            : base("name=VerkoopDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCarrito> tblCarrito { get; set; }
        public virtual DbSet<tblCat_Categoria> tblCat_Categoria { get; set; }
        public virtual DbSet<tblCat_Producto> tblCat_Producto { get; set; }
        public virtual DbSet<tblCat_Usuario> tblCat_Usuario { get; set; }
        public virtual DbSet<tblCompra> tblCompra { get; set; }
        public virtual DbSet<tblDireccion> tblDireccion { get; set; }
        public virtual DbSet<tblEstado> tblEstado { get; set; }
        public virtual DbSet<tblMunicipio> tblMunicipio { get; set; }
        public virtual DbSet<tblPais> tblPais { get; set; }
        public virtual DbSet<tblProductoComprado> tblProductoComprado { get; set; }
        public virtual DbSet<tblSesion> tblSesion { get; set; }
        public virtual DbSet<tblTarjeta> tblTarjeta { get; set; }
    }
}