using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class SupervisionOfficerToActMap : IEntityTypeConfiguration<SupervisionOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<SupervisionOfficerToAct> builder)
        {
            builder.ToTable("tbl_Supervision_OfficerToAct");
            builder.HasKey(k => k.SupervisionOfficerToActId);

            #region Properties
            builder.Property(p => p.SupervisionOfficerToActId)
               .HasColumnName("SupervisionOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.StaffSupervisionAppraisalId)
             .HasColumnName("StaffSupervisionAppraisalId")
             .IsRequired();

            #endregion
        }
    }
}
