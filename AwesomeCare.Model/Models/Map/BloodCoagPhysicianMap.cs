using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BloodCoagPhysicianMap : IEntityTypeConfiguration<BloodCoagPhysician>
    {
        public void Configure(EntityTypeBuilder<BloodCoagPhysician> builder)
        {
            builder.ToTable("tbl_BloodCoagPhysician");
            builder.HasKey(k => k.BloodCoagPhysicianId);

            #region Properties
            builder.Property(p => p.BloodCoagPhysicianId)
               .HasColumnName("BloodCoagPhysicianId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.BloodRecordId)
             .HasColumnName("BloodRecordId")
             .IsRequired();

            #endregion
        }
    }
}
