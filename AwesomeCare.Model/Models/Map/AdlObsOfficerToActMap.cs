using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class AdlObsOfficerToActMap : IEntityTypeConfiguration<AdlObsOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<AdlObsOfficerToAct> builder)
        {
            builder.ToTable("tbl_AdlObs_OfficerToAct");
            builder.HasKey(k => k.AdlObsOfficerToActId);

            #region Properties
            builder.Property(p => p.AdlObsOfficerToActId)
               .HasColumnName("AdlObsOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.ObservationId)
             .HasColumnName("ObservationId")
             .IsRequired();

            #endregion
        }
    }
}
