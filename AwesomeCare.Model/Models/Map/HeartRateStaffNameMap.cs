using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HeartRateStaffNameMap : IEntityTypeConfiguration<HeartRateStaffName>
    {
        public void Configure(EntityTypeBuilder<HeartRateStaffName> builder)
        {
            builder.ToTable("tbl_HeartRate_StaffName");
            builder.HasKey(k => k.HeartRateStaffNameId);

            #region Properties
            builder.Property(p => p.HeartRateStaffNameId)
               .HasColumnName("HeartRateStaffNameId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.HeartRateId)
             .HasColumnName("HeartRateId")
             .IsRequired();

            #endregion
        }
    }
}
