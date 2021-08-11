using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class KeyIndicatorsMap : IEntityTypeConfiguration<KeyIndicators>
    {
        public void Configure(EntityTypeBuilder<KeyIndicators> builder)
        {
            builder.ToTable("tbl_KeyIndicators");
            builder.HasKey(k => k.KeyId);

            #region Properties

            builder.Property(p => p.KeyId)
               .HasColumnName("KeyId")
               .IsRequired();

            builder.Property(p => p.PersonalDetailId)
               .HasColumnName("PersonalDetailId")
               .IsRequired();

            builder.Property(p => p.AboutMe)
              .HasColumnName("AboutMe")
              .IsRequired();

            builder.Property(p => p.FamilyRole)
             .HasColumnName("FamilyRole")
             .IsRequired();

            builder.Property(p => p.LivingStatus)
               .HasColumnName("LivingStatus")
               .IsRequired();

            builder.Property(p => p.Debture)
               .HasColumnName("Debture")
               .IsRequired();

            builder.Property(p => p.ThingsILike)
              .HasColumnName("ThingsILike")
              .IsRequired();

            builder.Property(p => p.LogMethod)
             .HasColumnName("LogMethod")
             .IsRequired();

            #endregion
        }
    }
}
