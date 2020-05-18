using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AwesomeCare.Model.Models.Map
{
    public class MedicationManufacturerMap : IEntityTypeConfiguration<MedicationManufacturer>
    {
        public void Configure(EntityTypeBuilder<MedicationManufacturer> builder)
        {
            builder.ToTable("tbl_MedicationManufacturer");
            builder.HasKey(k => k.MedicationManufacturerId);

            #region Properties
            builder.Property(p => p.MedicationManufacturerId)
                .HasColumnName("MedicationManufacturerId")
                .IsRequired();

            builder.Property(p => p.Manufacturer)
               .HasColumnName("Manufacturer")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Deleted)
               .HasColumnName("Deleted")
               .IsRequired();

            builder.HasIndex(i => i.Manufacturer)
                .HasName("IX_tbl_MedicationManufacturer_Manufacturer")
                .IsUnique();
            #endregion
        }
    }
}
