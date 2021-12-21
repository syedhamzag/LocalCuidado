using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HospitalExitOfficerToTakeActionMap : IEntityTypeConfiguration<HospitalExitOfficerToTakeAction>
    {
        public void Configure(EntityTypeBuilder<HospitalExitOfficerToTakeAction> builder)
        {
            builder.ToTable("tbl_HospitalExitOfficerToTakeAction");
            builder.HasKey(k => k.HospitalExitOfficerToTakeActionId);

            #region Properties

            builder.Property(p => p.HospitalExitOfficerToTakeActionId)
               .HasColumnName("HospitalExitOfficerToTakeActionId")
               .IsRequired();

            builder.Property(p => p.HospitalExitId)
              .HasColumnName("HospitalExitId")
              .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();
            #endregion
        }
    }
}
