using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BowelMovementPhysicianMap : IEntityTypeConfiguration<BowelMovementPhysician>
    {
        public void Configure(EntityTypeBuilder<BowelMovementPhysician> builder)
        {
            builder.ToTable("tbl_BowelMovement_Physician");
            builder.HasKey(k => k.BowelMovementPhysicianId);

            #region Properties
            builder.Property(p => p.BowelMovementPhysicianId)
               .HasColumnName("BowelMovementPhysicianId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.BowelMovementId)
             .HasColumnName("BowelMovementId")
             .IsRequired();

            #endregion
        }
    }
}
