using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PainChartStaffNameMap : IEntityTypeConfiguration<PainChartStaffName>
    {
        public void Configure(EntityTypeBuilder<PainChartStaffName> builder)
        {
            builder.ToTable("tbl_PainChart_StaffName");
            builder.HasKey(k => k.PainChartStaffNameId);

            #region Properties
            builder.Property(p => p.PainChartStaffNameId)
               .HasColumnName("PainChartStaffNameId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.PainChartId)
             .HasColumnName("PainChartId")
             .IsRequired();

            #endregion
        }
    }
}
