using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffEmergencyContactMap : IEntityTypeConfiguration<StaffEmergencyContact>
    {
        public void Configure(EntityTypeBuilder<StaffEmergencyContact> builder)
        {
            builder.ToTable("tbl_StaffEmergencyContact");
            builder.HasKey(p => p.StaffEmergencyContactId);

            #region Properties
            builder.Property(p => p.StaffEmergencyContactId)
              .HasColumnName("StaffEmergencyContactId")
              .IsRequired();

            builder.Property(p => p.ContactName)
             .HasColumnName("ContactName")
             .HasMaxLength(100)
             .IsRequired();

            builder.Property(p => p.Telephone)
            .HasColumnName("Telephone")
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(p => p.Email)
            .HasColumnName("Email")
            .HasMaxLength(100)
            .IsRequired(false);

            builder.Property(p => p.Relationship)
            .HasColumnName("Relationship")
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(p => p.Address)
            .HasColumnName("Address")
            .HasMaxLength(100)
            .IsRequired();
            #endregion

            builder.HasOne<StaffPersonalInfo>(p => p.StaffPersonalInfo)
                .WithMany(s => s.EmergencyContacts)
                .HasForeignKey(k => k.StaffPersonalInfoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
