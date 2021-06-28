using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BMIChartStaffNameMap : IEntityTypeConfiguration<BMIChartStaffName>
    {
        public void Configure(EntityTypeBuilder<BMIChartStaffName> builder)
        {
            builder.ToTable("tbl_BMIChart_StaffName");
            builder.HasKey(k => k.BMIChartStaffNameId);

            #region Properties
            builder.Property(p => p.BMIChartStaffNameId)
               .HasColumnName("BMIChartStaffNameId")
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
