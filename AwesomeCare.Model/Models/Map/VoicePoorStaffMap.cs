using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class VoicePoorStaffMap : IEntityTypeConfiguration<VoicePoorStaff>
    {
        public void Configure(EntityTypeBuilder<VoicePoorStaff> builder)
        {
            builder.ToTable("tbl_Voice_PoorStaff");
            builder.HasKey(k => k.VoicePoorStaffId);

            #region Properties
            builder.Property(p => p.VoicePoorStaffId)
               .HasColumnName("VoicePoorStaffId")
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
