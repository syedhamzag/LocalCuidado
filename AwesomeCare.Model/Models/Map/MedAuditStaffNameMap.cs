using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class MedAuditStaffNameMap : IEntityTypeConfiguration<MedAuditStaffName>
    {
        public void Configure(EntityTypeBuilder<MedAuditStaffName> builder)
        {
            builder.ToTable("tbl_MedAuditStaffName");
            builder.HasKey(k => k.MedAuditStaffNameId);

            #region Properties
            builder.Property(p => p.MedAuditStaffNameId)
               .HasColumnName("MedAuditStaffNameId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.MedAuditId)
             .HasColumnName("MedAuditId")
             .IsRequired();

            #endregion
        }
    }
}
