﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BD_ActivosEntities : DbContext
    {
        public BD_ActivosEntities()
            : base("name=BD_ActivosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Aseguradora> Aseguradora { get; set; }
        public virtual DbSet<CategoriaActivo> CategoriaActivo { get; set; }
        public virtual DbSet<CategoriaUsuario> CategoriaUsuario { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }
        public virtual DbSet<HistoricoDepreciacion> HistoricoDepreciacion { get; set; }
        public virtual DbSet<Activo> Activo { get; set; }
    }
}