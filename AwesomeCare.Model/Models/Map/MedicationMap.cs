using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class MedicationMap : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.ToTable("tbl_Medication");
            builder.HasKey(k => k.MedicationId);

            #region Properties
            builder.Property(p => p.MedicationId)
                .HasColumnName("MedicationId")
                .IsRequired();

            builder.Property(p => p.MedicationName)
               .HasColumnName("MedicationName")
               .HasMaxLength(225)
               .IsRequired();

            builder.Property(p => p.Strength)
              .HasColumnName("Strength")
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(p => p.Deleted)
              .HasColumnName("Deleted")
              .IsRequired();
            #endregion
        }
    }
}
