using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaReachMVC.Models;

namespace PharmaReachMVC.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            // Composite key for OrderId and MedicineId
            builder.HasKey(od => new { od.OrderId, od.MedicineId });

            // Configuring relationships
            builder.HasOne(od => od.Order)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(od => od.Medicine)
                   .WithMany(m => m.OrderDetails)
                   .HasForeignKey(od => od.MedicineId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configuring property constraints
            builder.Property(od => od.Quantity)
                   .IsRequired()
                   .HasDefaultValue(1);  // Set a default value for quantity

            builder.Property(od => od.PricePerItem)
                   .IsRequired()
                   .HasPrecision(18, 2); // Setting precision for decimal (18 total digits, 2 after decimal)

            builder.Property(od => od.TotalPricePerItem)
                   .IsRequired()
                   .HasPrecision(18, 2); // Setting precision for decimal (18 total digits, 2 after decimal)
        }
    }
}
