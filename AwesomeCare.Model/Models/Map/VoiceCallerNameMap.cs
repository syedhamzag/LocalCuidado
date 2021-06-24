using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class VoiceCallerNameMap : IEntityTypeConfiguration<VoiceCallerName>
    {
        public void Configure(EntityTypeBuilder<VoiceCallerName> builder)
        {
            builder.ToTable("tbl_Voice_CallerName");
            builder.HasKey(k => k.VoiceCallerNameId);

            #region Properties
            builder.Property(p => p.VoiceCallerNameId)
               .HasColumnName("VoiceCallerNameId")
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
