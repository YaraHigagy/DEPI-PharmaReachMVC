using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;

namespace PharmaReachMVC.Configurations
{
    public class MedicinePharmacyCanBeFreeConfiguration : IEntityTypeConfiguration<MedicinePharmacyCanBeFree>
    {
        public void Configure(EntityTypeBuilder<MedicinePharmacyCanBeFree> builder)
        {
            // Define the composite key for MedicinePharmacyCanBeFree
            builder.HasKey(mp => new { mp.PharmacyId, mp.MedicineId });

            // Configure relationships
            builder.HasOne(mp => mp.Pharmacy)
                   .WithMany(p => p.MedicinePharmacyCanBeFrees)
                   .HasForeignKey(mp => mp.PharmacyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mp => mp.Medicine)
                   .WithMany(m => m.MedicinePharmacyCanBeFrees)
                   .HasForeignKey(mp => mp.MedicineId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configure other properties as necessary
            builder.Property(mp => mp.AvailableQuantity)
                .IsRequired()
                .HasDefaultValue(0) // Default to 0 if not specified
                .HasComment("The quantity of medicine available to be offered for free under certain conditions.");
        }
    }
}
