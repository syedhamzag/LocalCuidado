using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientMedicationDayMap : IEntityTypeConfiguration<ClientMedicationDay>
    {
        public void Configure(EntityTypeBuilder<ClientMedicationDay> builder)
        {
            builder.ToTable("tbl_ClientMedicationDay");
            builder.HasKey(k => k.ClientMedicationDayId);

            #region Properties
            builder.Property(p => p.ClientMedicationDayId)
                .HasColumnName("ClientMedicationDayId")
                .IsRequired();

            builder.Property(p => p.ClientMedicationId)
               .HasColumnName("ClientMedicationId")
               .IsRequired();

            builder.Property(p => p.RotaDayofWeekId)
               .HasColumnName("RotaDayofWeekId")
               .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne<ClientMedication>()
                .WithMany(m => m.ClientMedicationDay)
                .HasForeignKey(k => k.ClientMedicationId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
