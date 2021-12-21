using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PulseRateStaffNameMap : IEntityTypeConfiguration<PulseRateStaffName>
    {
        public void Configure(EntityTypeBuilder<PulseRateStaffName> builder)
        {
            builder.ToTable("tbl_PulseRate_StaffName");
            builder.HasKey(k => k.PulseRateStaffNameId);

            #region Properties
            builder.Property(p => p.PulseRateStaffNameId)
               .HasColumnName("PulseRateStaffNameId")
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
