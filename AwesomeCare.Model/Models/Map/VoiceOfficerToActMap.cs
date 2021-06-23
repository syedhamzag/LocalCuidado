using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class VoiceOfficerToActMap : IEntityTypeConfiguration<VoiceOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<VoiceOfficerToAct> builder)
        {
            builder.ToTable("tbl_VoiceOfficerToAct");
            builder.HasKey(k => k.VoiceOfficerToActId);

            #region Properties
            builder.Property(p => p.VoiceOfficerToActId)
               .HasColumnName("VoiceOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.VoiceId)
             .HasColumnName("VoiceId")
             .IsRequired();

            #endregion
        }
    }
}
