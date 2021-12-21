using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BloodCoagStaffNameMap : IEntityTypeConfiguration<BloodCoagStaffName>
    {
        public void Configure(EntityTypeBuilder<BloodCoagStaffName> builder)
        {
            builder.ToTable("tbl_BloodCoag_StaffName");
            builder.HasKey(k => k.BloodCoagStaffNameId);

            #region Properties
            builder.Property(p => p.BloodCoagStaffNameId)
               .HasColumnName("BloodCoagStaffNameId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.BloodRecordId)
             .HasColumnName("BloodRecordId")
             .IsRequired();

            #endregion
        }
    }
}
