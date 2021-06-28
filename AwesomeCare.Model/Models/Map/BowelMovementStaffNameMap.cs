using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BowelMovementStaffNameMap : IEntityTypeConfiguration<BowelMovementStaffName>
    {
        public void Configure(EntityTypeBuilder<BowelMovementStaffName> builder)
        {
            builder.ToTable("tbl_BowelMovement_StaffName");
            builder.HasKey(k => k.BowelMovementStaffNameId);

            #region Properties
            builder.Property(p => p.BowelMovementStaffNameId)
               .HasColumnName("BowelMovementStaffNameId")
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
