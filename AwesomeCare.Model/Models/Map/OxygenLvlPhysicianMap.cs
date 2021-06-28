using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class OxygenLvlPhysicianMap : IEntityTypeConfiguration<OxygenLvlPhysician>
    {
        public void Configure(EntityTypeBuilder<OxygenLvlPhysician> builder)
        {
            builder.ToTable("tbl_OxygenLvl_Physician");
            builder.HasKey(k => k.OxygenLvlPhysicianId);

            #region Properties
            builder.Property(p => p.OxygenLvlPhysicianId)
               .HasColumnName("OxygenLvlPhysicianId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.OxygenLvlId)
             .HasColumnName("OxygenLvlId")
             .IsRequired();

            #endregion
        }
    }
}
