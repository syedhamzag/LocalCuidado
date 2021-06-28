using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class VoiceGoodStaffMap : IEntityTypeConfiguration<VoiceGoodStaff>
    {
        public void Configure(EntityTypeBuilder<VoiceGoodStaff> builder)
        {
            builder.ToTable("tbl_Voice_GoodStaff");
            builder.HasKey(k => k.VoiceGoodStaffId);

            #region Properties
            builder.Property(p => p.VoiceGoodStaffId)
               .HasColumnName("VoiceGoodStaffId")
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
