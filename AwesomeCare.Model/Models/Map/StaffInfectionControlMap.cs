using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffInfectionControlMap : IEntityTypeConfiguration<StaffInfectionControl>
    {
        public void Configure(EntityTypeBuilder<StaffInfectionControl> builder)
        {
            builder.ToTable("tbl_StaffInfectionControl");
            builder.HasKey(k => k.InfectionId);

            #region Properties
            builder.Property(p => p.InfectionId)
               .HasColumnName("InfectionId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
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
            builder.HasOne(p => p.StaffPersonalInfo)
                 .WithMany(p => p.StaffInfectionControl)
                 .HasForeignKey(p => p.StaffPersonalInfoId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
