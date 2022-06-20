using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientMedicationMap : IEntityTypeConfiguration<ClientMedication>
    {
        public void Configure(EntityTypeBuilder<ClientMedication> builder)
        {
            builder.ToTable("tbl_ClientMedication");
            builder.HasKey(k => k.ClientMedicationId);

            #region Properties
            builder.Property(p => p.ClientMedicationId)
                .HasColumnName("ClientMedicationId")
                .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.MedicationId)
                .HasColumnName("MedicationId")
                .IsRequired();

            builder.Property(p => p.MedicationManufacturerId)
               .HasColumnName("MedicationManufacturerId")
               .IsRequired();

            builder.Property(p => p.ExpiryDate)
               .HasColumnName("ExpiryDate")
               .HasMaxLength(15)
               .IsRequired();

            builder.Property(p => p.Dossage)
               .HasColumnName("Dossage")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Frequency)
               .HasColumnName("Frequency")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Gap_Hour)
               .HasColumnName("Gap_Hour")
               .IsRequired();

            builder.Property(p => p.Route)
               .HasColumnName("Route")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.StartDate)
               .HasColumnName("StartDate")
               .HasMaxLength(15)
               .IsRequired();

            builder.Property(p => p.StopDate)
              .HasColumnName("StopDate")
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Remark)
               .HasColumnName("Remark")
               .HasMaxLength(250)
               .IsRequired();

            builder.Property(p => p.Means)
               .HasColumnName("Means")
               .IsRequired();

            builder.Property(p => p.Type)
               .HasColumnName("Type")
               .IsRequired();

            builder.Property(p => p.TimeCritical)
               .HasColumnName("TimeCritical")
               .IsRequired();

            builder.Property(p => p.ClientMedImage)
               .HasColumnName("ClientMedImage")
               .IsRequired();
            #endregion

            #region Relationship

            builder.HasOne(c => c.Client)
                .WithMany(c => c.ClientMedication)
                .HasForeignKey(k => k.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
