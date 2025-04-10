using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;

namespace PharmaReachMVC.Configurations
{
    public class PharmacyMedicineConfiguration : IEntityTypeConfiguration<PharmacyMedicine>
    {
        public void Configure(EntityTypeBuilder<PharmacyMedicine> builder)
        {
            // Composite primary key
            builder.HasKey(pm => new { pm.PharmacyId, pm.MedicineId });

            // Configuring relationships
            builder.HasOne(pm => pm.Pharmacy)
                   .WithMany(p => p.PharmacyMedicines)
                   .HasForeignKey(pm => pm.PharmacyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pm => pm.Medicine)
                   .WithMany(m => m.PharmacyMedicines)
                   .HasForeignKey(pm => pm.MedicineId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configuring property constraints
            builder.Property(pm => pm.PriceOverride)
                   .IsRequired()
                   .HasPrecision(18, 2); // Setting precision for decimal (18 total digits, 2 after decimal)
        }
    }
}
