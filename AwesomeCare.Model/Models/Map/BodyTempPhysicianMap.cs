using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BodyTempPhysicianMap : IEntityTypeConfiguration<BodyTempPhysician>
    {
        public void Configure(EntityTypeBuilder<BodyTempPhysician> builder)
        {
            builder.ToTable("tbl_BodyTempPhysician");
            builder.HasKey(k => k.BodyTempPhysicianId);

            #region Properties
            builder.Property(p => p.BodyTempPhysicianId)
               .HasColumnName("BodyTempPhysicianId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.BodyTempId)
             .HasColumnName("BodyTempId")
             .IsRequired();

            #endregion
        }
    }
}
