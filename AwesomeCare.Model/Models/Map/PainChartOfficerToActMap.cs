using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PainChartOfficerToActMap : IEntityTypeConfiguration<PainChartOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<PainChartOfficerToAct> builder)
        {
            builder.ToTable("tbl_PainChart_OfficerToAct");
            builder.HasKey(k => k.PainChartOfficerToActId);

            #region Properties
            builder.Property(p => p.PainChartOfficerToActId)
               .HasColumnName("PainChartOfficerToActId")
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
