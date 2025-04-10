using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Configurations;
using PharmaReachMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PharmaReachMVC
{
    internal class PharmaReachDbContext : DbContext
    {
        // Constructor
        public PharmaReachDbContext(DbContextOptions<PharmaReachDbContext> Options) : base(Options)
        {
        }

        // Override the OnConfiguring method for database connection setup
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // You can also configure connection string dynamically or via app settings here
            //optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=PharmaReachDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }

        // OnModelCreating method to apply Fluent API configurations from assembly
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations for the models from the current assembly (Fluent API)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #region DbSets - Database Tables

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<PharmacyMedicine> PharmacyMedicines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<MedicinePharmacyIsFree> MedicinePharmacyIsFrees { get; set; }
        public DbSet<MedicinePharmacyCanBeFree> MedicinePharmacyCanBeFrees { get; set; }

        #endregion
    }
}
