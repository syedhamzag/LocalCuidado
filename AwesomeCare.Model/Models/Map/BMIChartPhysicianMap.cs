using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BMIChartPhysicianMap : IEntityTypeConfiguration<BMIChartPhysician>
    {
        public void Configure(EntityTypeBuilder<BMIChartPhysician> builder)
        {
            builder.ToTable("tbl_BMIChart_Physician");
            builder.HasKey(k => k.BMIChartPhysicianId);

            #region Properties
            builder.Property(p => p.BMIChartPhysicianId)
               .HasColumnName("BMIChartPhysicianId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.BMIChartId)
             .HasColumnName("BMIChartId")
             .IsRequired();

            #endregion
        }
    }
}
