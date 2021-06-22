using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PulseRateOfficerToActMap : IEntityTypeConfiguration<PulseRateOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<PulseRateOfficerToAct> builder)
        {
            builder.ToTable("tbl_PulseRateOfficerToAct");
            builder.HasKey(k => k.PulseRateOfficerToActId);

            #region Properties
            builder.Property(p => p.PulseRateOfficerToActId)
               .HasColumnName("PulseRateOfficerToActId")
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
