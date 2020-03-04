using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class UntowardsOfficerToActMap : IEntityTypeConfiguration<UntowardsOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<UntowardsOfficerToAct> builder)
        {
            builder.ToTable("tbl_UntowardsOfficerToAct");
            builder.HasKey(k => k.UntowardsOfficerToActId);

            #region Properties
            builder.Property(p => p.UntowardsOfficerToActId)
               .HasColumnName("UntowardsOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.UntowardsId)
             .HasColumnName("UntowardsId")
             .IsRequired();

            #endregion
        }
    }
}
