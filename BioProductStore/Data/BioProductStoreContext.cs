using BioProductStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Data
{
    public class BioProductStoreContext
    {
        public class BioProductsStoreContext : DbContext
        {
            //for manipulation - > context -> tables defined as sets
            public DbSet<DataBaseModel> DataBaseModels { get; set; }
            //Users table
            public DbSet<User> Users { get; set; }
            //Orders table
            public DbSet<Order> Orders { get; set; }
            //Categories table
            public DbSet<Category> Categories { get; set; }
            //Products table
            public DbSet<Product> Products { get; set; }
            //ExpeditionAddress table
            public DbSet<ExpeditionAddress> ExpeditionAddresses { get; set; }

            //many-to-many relationship between Orders and Products
            public DbSet<OrderProduct> OrderProduct { get; set; }
            //constructor
            public BioProductsStoreContext(DbContextOptions<BioProductsStoreContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                //Relationships in my database

                //1. ONE-TO-MANY:

                //One-to-many relationship between User(1) and Order(M)
                builder.Entity<User>() //one User has multiple Orders
                    .HasMany(u => u.Orders)
                    .WithOne(o => o.User);

                // builder.Entity<Order>() //identical as before
                //    .HasOne(o => o.User)
                //   .WithMany(u => u.Orders);

                //One-to-many relationship between Category(1) and Product(M)
                builder.Entity<Category>() //one Category has multiple Products
                    .HasMany(c => c.Products)
                    .WithOne(p => p.Category);

                //builder.Entity<Product>() //identical as before
                //   .HasOne(p => p.Category) //one Cate
                //  .WithMany(c => c.Products);


                //2. ONE-TO-ONE:

                //One-to-one relationship between Order and ExpeditionAdsress
                builder.Entity<Order>() //One Order has one ExpeditionAdress.
                    .HasOne(o => o.ExpeditionAddress)
                    .WithOne(d => d.Order)
                    .HasForeignKey<ExpeditionAddress>(d => d.OrderId);


                //3. MANY-TO-MANY:

                //Many-to-many relationship between Order and Product = > OrderProduct associative tabel
                builder.Entity<OrderProduct>()
                    .HasKey(op => new //OrderProduct class will have the Id as a tuple of the 2 primary keys
                {
                        op.OrderId,
                        op.ProductId
                    });


                builder.Entity<OrderProduct>() //One Order can have multiple OrderProducts
                    .HasOne<Order>(op => op.Order)
                    .WithMany(o => o.OrderProducts)
                    .HasForeignKey(op => op.OrderId);

                builder.Entity<OrderProduct>() //One Product can have multiple OrderProducrs
                    .HasOne<Product>(op => op.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(op => op.ProductId);


                base.OnModelCreating(builder);
            }

        }
    }
}
