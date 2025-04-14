using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;
using System.Reflection.Emit;

namespace PharmaReachMVC.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id); // Single primary key for Customer

            builder.HasOne(c => c.Address)
                   .WithMany(a => a.Customers)
                   .HasForeignKey(c => c.AddressId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.ApplicationUser)
                   .WithOne(u => u.Customer)
                   .HasForeignKey<Customer>(c => c.ApplicationUserId)
                   .IsRequired(false); // <--- This makes it optional


            builder.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()"); // add current datetime when creating the record itself
        }
    }
}
