using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientCleaningMap : IEntityTypeConfiguration<ClientCleaning>
    {
        public void Configure(EntityTypeBuilder<ClientCleaning> builder)
        {
            builder.ToTable("tbl_ClientCleaning");
            builder.HasKey(p => p.CleaningId);

            #region Properties
            builder.Property(p => p.CleaningId)
                .HasColumnName("CleaningId")
                .IsRequired();

            builder.Property(p => p.NutritionId)
               .HasColumnName("NutritionId")
               .IsRequired();

            builder.Property(p => p.AreasAndItems)
              .HasColumnName("AreasAndItems")
              .IsRequired();

            builder.Property(p => p.Details)
             .HasColumnName("Details")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.SafetyHazard)
             .HasColumnName("SafetyHazard")
              .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.LocationOfItem)
             .HasColumnName("LocationOfItem")
              .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.DescOfItem)
             .HasColumnName("DescOfItem")
              .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.Disposal)
             .HasColumnName("Disposal")
              .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.DAYOFCLEANING)
             .HasColumnName("DAYOFCLEANING")
              .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.WhereToKeep)
             .HasColumnName("WhereToKeep")
             .IsRequired();

            builder.Property(p => p.SEEVIDEO)
             .HasColumnName("SEEVIDEO")
              .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.Image)
             .HasColumnName("Image")
             .IsRequired();

            builder.Property(p => p.WhereToGet)
             .HasColumnName("WhereToGet")
             .IsRequired();

            builder.Property(p => p.STAFFId)
                .HasColumnName("STAFFId")
                .IsRequired();

            builder.Property(p => p.MinuteAlloted)
                .HasColumnName("MinuteAlloted")
                .IsRequired();

            builder.Property(p => p.DATEFROM)
                .HasColumnName("DATEFROM")
                .IsRequired();

            builder.Property(p => p.DATETO)
                .HasColumnName("DATETO")
                .IsRequired();

            #endregion

            #region Relationships
            builder.HasOne(p => p.ClientNutrition)
                .WithMany(p => p.ClientCleaning)
                .HasForeignKey(p => p.NutritionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.StaffPersonalInfo)
                .WithMany(p => p.ClientCleaning)
                .HasForeignKey(p => p.STAFFId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
