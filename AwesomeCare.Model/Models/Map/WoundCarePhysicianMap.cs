using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class WoundCarePhysicianMap : IEntityTypeConfiguration<WoundCarePhysician>
    {
        public void Configure(EntityTypeBuilder<WoundCarePhysician> builder)
        {
            builder.ToTable("tbl_WoundCarePhysician");
            builder.HasKey(k => k.WoundCarePhysicianId);

            #region Properties
            builder.Property(p => p.WoundCarePhysicianId)
               .HasColumnName("WoundCarePhysicianId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.WoundCareId)
             .HasColumnName("WoundCareId")
             .IsRequired();

            #endregion
        }
    }
}
