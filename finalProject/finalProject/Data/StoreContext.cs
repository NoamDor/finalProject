namespace finalProject
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using finalProject.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class StoreContext : DbContext
    {
        public StoreContext()
            : base("name=Store")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Purchases)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);            

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Purchases)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
