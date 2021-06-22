using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BloodPressureOfficerToActMap : IEntityTypeConfiguration<BloodPressureOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<BloodPressureOfficerToAct> builder)
        {
            builder.ToTable("tbl_BloodPressureOfficerToAct");
            builder.HasKey(k => k.BloodPressureOfficerToActId);

            #region Properties
            builder.Property(p => p.BloodPressureOfficerToActId)
               .HasColumnName("BloodPressureOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.BloodPressureId)
             .HasColumnName("BloodPressureId")
             .IsRequired();

            #endregion
        }
    }
}
