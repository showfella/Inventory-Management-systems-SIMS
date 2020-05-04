using InventorySystem.Core.Entities;
using InventorySystem.Infrastructure.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Infrastructure.Context
{
     public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext() : base("InventorySystemString")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        
        public DbSet<Purchase> Purchases { get; set; } 

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
