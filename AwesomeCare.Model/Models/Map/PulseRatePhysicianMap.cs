using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PulseRatePhysicianMap : IEntityTypeConfiguration<PulseRatePhysician>
    {
        public void Configure(EntityTypeBuilder<PulseRatePhysician> builder)
        {
            builder.ToTable("tbl_PulseRate_Physician");
            builder.HasKey(k => k.PulseRatePhysicianId);

            #region Properties
            builder.Property(p => p.PulseRatePhysicianId)
               .HasColumnName("PulseRatePhysicianId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.PulseRateId)
             .HasColumnName("PulseRateId")
             .IsRequired();

            #endregion
        }
    }
}
