using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BowelMovementOfficerToActMap : IEntityTypeConfiguration<BowelMovementOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<BowelMovementOfficerToAct> builder)
        {
            builder.ToTable("tbl_BowelMovementOfficerToAct");
            builder.HasKey(k => k.BowelMovementOfficerToActId);

            #region Properties
            builder.Property(p => p.BowelMovementOfficerToActId)
               .HasColumnName("BowelMovementOfficerToActId")
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
