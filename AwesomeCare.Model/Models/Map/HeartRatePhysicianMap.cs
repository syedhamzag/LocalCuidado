using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HeartRatePhysicianMap : IEntityTypeConfiguration<HeartRatePhysician>
    {
        public void Configure(EntityTypeBuilder<HeartRatePhysician> builder)
        {
            builder.ToTable("tbl_HeartRatePhysician");
            builder.HasKey(k => k.HeartRatePhysicianId);

            #region Properties
            builder.Property(p => p.HeartRatePhysicianId)
               .HasColumnName("HeartRatePhysicianId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.HeartRateId)
             .HasColumnName("HeartRateId")
             .IsRequired();

            #endregion
        }
    }
}
