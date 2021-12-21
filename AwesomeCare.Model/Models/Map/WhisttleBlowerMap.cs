using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeCare.Model.Models.Map
{
    public class WhisttleBlowerMap : IEntityTypeConfiguration<WhisttleBlower>
    {
        public void Configure(EntityTypeBuilder<WhisttleBlower> builder)
        {
            builder.ToTable("tbl_Whisttle_Blower");
            builder.HasKey(k => k.WhisttleBlowerId);

            #region Properties
            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.UserName)
               .HasColumnName("UserName")
               .IsRequired();

            builder.Property(p => p.StaffName)
               .HasColumnName("StaffName")
               .IsRequired();

            builder.Property(p => p.IncidentDate)
               .HasColumnName("IncidentDate")
               .IsRequired();

            builder.Property(p => p.Happening)
               .HasColumnName("Happening")
               .IsRequired();

            builder.Property(p => p.Evidence)
               .HasColumnName("Evidence")
               .IsRequired();

            builder.Property(p => p.Witness)
               .HasColumnName("Witness")
               .IsRequired();

            builder.Property(p => p.LikeCalling)
               .HasColumnName("LikeCalling")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();
            #endregion

            #region Relationship

            builder.HasOne(p => p.Client)
                 .WithMany(p => p.WhisttleBlower)
                 .HasForeignKey(p => p.UserName)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}