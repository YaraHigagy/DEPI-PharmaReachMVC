using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;

namespace PharmaReachMVC.Configurations
{
    public class PharmacyConfiguration : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            builder.HasKey(p => p.Id); // Single primary key for Pharmacy

            builder.HasOne(p => p.Address)
                   .WithMany(a => a.Pharmacies)
                   .HasForeignKey(p => p.AddressId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()"); // add current datetime when creating the record itself
        }
    }
}
