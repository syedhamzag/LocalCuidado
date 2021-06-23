using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class MedAuditOfficerToActMap : IEntityTypeConfiguration<MedAuditOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<MedAuditOfficerToAct> builder)
        {
            builder.ToTable("tbl_MedAudit_OfficerToAct");
            builder.HasKey(k => k.MedAuditOfficerToActId);

            #region Properties
            builder.Property(p => p.MedAuditOfficerToActId)
               .HasColumnName("MedAuditOfficerToActId")
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
