using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class SeizurePhysicianMap : IEntityTypeConfiguration<SeizurePhysician>
    {
        public void Configure(EntityTypeBuilder<SeizurePhysician> builder)
        {
            builder.ToTable("tbl_Seizure_Physician");
            builder.HasKey(k => k.SeizurePhysicianId);

            #region Properties
            builder.Property(p => p.SeizurePhysicianId)
               .HasColumnName("SeizurePhysicianId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.SeizureId)
             .HasColumnName("SeizureId")
             .IsRequired();

            #endregion
        }
    }
}
