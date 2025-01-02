using Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Set up the database connection string
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ECommerce");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: Configure relationships explicitly
            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 1000, Stock = 50, CategoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest model smartphone", Price = 800, Stock = 100, CategoryId = 1 },
                new Product { Id = 3, Name = "T-shirt", Description = "Comfortable cotton T-shirt", Price = 20, Stock = 200, CategoryId = 2 }
            );
        }

        // DbSets for your entities
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}