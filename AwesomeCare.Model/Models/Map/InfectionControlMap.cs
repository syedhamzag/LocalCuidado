using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class InfectionControlMap : IEntityTypeConfiguration<InfectionControl>
    {
        public void Configure(EntityTypeBuilder<InfectionControl> builder)
        {
            builder.ToTable("tbl_InfectionControl");
            builder.HasKey(k => k.InfectionId);

            #region Properties
            builder.Property(p => p.InfectionId)
               .HasColumnName("InfectionId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Type)
               .HasColumnName("Cleaning")
               .IsRequired();

            builder.Property(p => p.Guideline)
               .HasColumnName("CleaningFreq")
               .IsRequired();

            builder.Property(p => p.TestDate)
               .HasColumnName("CleaningTools")
               .IsRequired();

            builder.Property(p => p.VaccStatus)
               .HasColumnName("DesiredCleaning")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("DirtyLaundry")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("DryLaundry")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.InfectionControl)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
