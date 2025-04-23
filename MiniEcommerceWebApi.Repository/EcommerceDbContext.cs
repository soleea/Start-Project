using Microsoft.EntityFrameworkCore;

using MiniEcommerceWebApi.Core.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MiniEcommerceWebApi.Repository
{
   
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //              optionsBuilder.UseSqlServer("Server=LAPTOP-BCC6PVKV\\SQLEXPRESS;Database=Chromium;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, CustomerName = "James Bon", Email = "james@gmail.com", Password = "password", Phone = "1234567890", BillingAddress = "123 Main St" },
                new Customer { Id = 2, CustomerName = "Mary Land", Email = "mary@gmail.com", Password = "password", Phone = "0987654321", BillingAddress = "456 Park Ave" },
                new Customer { Id = 3, CustomerName = "Eko John", Email = "eko@example.com", Password = "admin", Phone = "1112223333", BillingAddress = "Dele HQ" }
            );
            // Seed Products.
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ProductName = "Apple iPhone 13",
                    Description = "The latest Apple iPhone featuring the A15 Bionic chip, 5G connectivity, and an advanced dual-camera system.",
                    UnitPrice = 799.00m
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Techno S21",
                    Description = "A high-end smartphone with a 6.2-inch display, Exynos 2100 processor, and a versatile triple-camera setup.",
                    UnitPrice = 699.00m
                },
                new Product
                {
                    Id = 3,
                    ProductName = "Lenovo Headphones",
                    Description = "Industry-leading noise-canceling headphones with superior sound quality and long battery life.",
                    UnitPrice = 349.99m
                },
                new Product
                {
                    Id = 4,
                    ProductName = "HP Laptop",
                    Description = "A sleek and powerful ultrabook featuring a 13.4-inch display, Intel Core i7 processor, and fast SSD storage.",
                    UnitPrice = 999.99m
                },
                new Product
                {
                    Id = 5,
                    ProductName = "Headset",
                    Description = "A compact smart speaker with Alexa voice assistant for controlling smart home devices and streaming music.",
                    UnitPrice = 49.99m
                }
            );
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
      
    }


}

