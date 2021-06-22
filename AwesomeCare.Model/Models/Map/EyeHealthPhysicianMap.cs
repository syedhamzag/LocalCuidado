using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class EyeHealthPhysicianMap : IEntityTypeConfiguration<EyeHealthPhysician>
    {
        public void Configure(EntityTypeBuilder<EyeHealthPhysician> builder)
        {
            builder.ToTable("tbl_EyeHealthPhysician");
            builder.HasKey(k => k.EyeHealthPhysicianId);

            #region Properties
            builder.Property(p => p.EyeHealthPhysicianId)
               .HasColumnName("EyeHealthPhysicianId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.EyeHealthId)
             .HasColumnName("EyeHealthId")
             .IsRequired();

            #endregion
        }
    }
}
