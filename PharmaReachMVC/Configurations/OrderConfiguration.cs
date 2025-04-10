using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;

namespace PharmaReachMVC.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Single primary key for Order
            builder.HasKey(o => o.Id);

            // Configuring relationships
            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Pharmacy)
                   .WithMany(p => p.Orders)
                   .HasForeignKey(o => o.PharmacyId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configuring property constraints
            builder.Property(o => o.TotalPrice)
                   .IsRequired()
                   .HasPrecision(18, 2); // Setting precision for decimal (18 total digits, 2 after decimal)

            builder.Property(o => o.CreatedAt).HasDefaultValueSql("GETDATE()"); // add current datetime when creating the record itself
        }
    }
}
