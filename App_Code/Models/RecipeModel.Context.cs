﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RecipieEntities : DbContext
    {
        public RecipieEntities()
            : base("name=RecipieEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<IngredientTable> IngredientTables { get; set; }
        public virtual DbSet<RecipeTable> RecipeTables { get; set; }
        public virtual DbSet<QuantityTable> QuantityTables { get; set; }
    }
}
