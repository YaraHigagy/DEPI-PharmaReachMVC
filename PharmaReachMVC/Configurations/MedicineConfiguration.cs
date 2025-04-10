using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PharmaReachMVC.Models;

namespace PharmaReachMVC.Configurations
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.HasKey(m => m.Id); // Single primary key for Medicine

            builder.Property(m => m.Price)
                   .IsRequired()
                   .HasPrecision(18, 2); // Setting precision for decimal (18 total digits, 2 after decimal)

            builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETDATE()"); // add current datetime when creating the record itself
        }
    }

}
