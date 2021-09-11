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
               .HasColumnName("Type")
               .IsRequired();

            builder.Property(p => p.Guideline)
               .HasColumnName("Guideline")
               .IsRequired();

            builder.Property(p => p.TestDate)
               .HasColumnName("TestDate")
               .IsRequired();

            builder.Property(p => p.VaccStatus)
               .HasColumnName("VaccStatus")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
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
