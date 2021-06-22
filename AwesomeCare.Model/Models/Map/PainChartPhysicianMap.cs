using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PainChartPhysicianMap : IEntityTypeConfiguration<PainChartPhysician>
    {
        public void Configure(EntityTypeBuilder<PainChartPhysician> builder)
        {
            builder.ToTable("tbl_PainChartPhysician");
            builder.HasKey(k => k.PainChartPhysicianId);

            #region Properties
            builder.Property(p => p.PainChartPhysicianId)
               .HasColumnName("PainChartPhysicianId")
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
