using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeCare.Model.Models.Map
{
    public class IncomingMedsMap : IEntityTypeConfiguration<IncomingMeds>
    {
        public void Configure(EntityTypeBuilder<IncomingMeds> builder)
        {
            builder.ToTable("tbl_Incoming_Meds");
            builder.HasKey(k => k.IncomingMedsId);

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

            builder.Property(p => p.StartDate)
               .HasColumnName("StartDate")
               .IsRequired();

            builder.Property(p => p.ChartImage)
               .HasColumnName("ChartImage")
               .IsRequired();

            builder.Property(p => p.MedsImage)
               .HasColumnName("MedsImage")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();
            #endregion

            #region Relationship

            builder.HasOne(p => p.Client)
                 .WithMany(p => p.IncomingMeds)
                 .HasForeignKey(p => p.UserName)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}