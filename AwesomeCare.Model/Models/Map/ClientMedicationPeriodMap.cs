using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientMedicationPeriodMap : IEntityTypeConfiguration<ClientMedicationPeriod>
    {
        public void Configure(EntityTypeBuilder<ClientMedicationPeriod> builder)
        {
            builder.ToTable("tbl_ClientMedicationPeriod");
            builder.HasKey(k => k.ClientMedicationPeriodId);

            #region Properties
            builder.Property(p => p.ClientMedicationPeriodId)
                .HasColumnName("ClientMedicationPeriodId")
                .IsRequired();

            builder.Property(p => p.ClientRotaTypeId)
                .HasColumnName("ClientRotaTypeId")
                .IsRequired();

            builder.Property(p => p.ClientMedicationDayId)
              .HasColumnName("ClientMedicationDayId")
              .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne(o => o.ClientMedicationDay)
                .WithMany(m => m.ClientMedicationPeriod)
                .HasForeignKey(f => f.ClientMedicationDayId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
