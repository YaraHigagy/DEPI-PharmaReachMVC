using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;

namespace PharmaReachMVC.Configurations
{
    public class MedicinePharmacyIsFreeConfiguration : IEntityTypeConfiguration<MedicinePharmacyIsFree>
    {
        public void Configure(EntityTypeBuilder<MedicinePharmacyIsFree> builder)
        {
            // Composite key for PharmacyId and MedicineId
            builder.HasKey(mp => new { mp.PharmacyId, mp.MedicineId });

            // Configuring relationships with Pharmacy and Medicine
            builder.HasOne(mp => mp.Pharmacy)
                   .WithMany(p => p.MedicinePharmacyIsFrees)
                   .HasForeignKey(mp => mp.PharmacyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mp => mp.Medicine)
                   .WithMany(med => med.MedicinePharmacyIsFrees)
                   .HasForeignKey(mp => mp.MedicineId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configuring AvailableQuantity property
            builder.Property(mp => mp.AvailableQuantity)
            .IsRequired()
            .HasDefaultValue(0) // Default to 0 if not specified
            .HasComment("The quantity of medicine available for free in the pharmacy.");
        }
    }
}
