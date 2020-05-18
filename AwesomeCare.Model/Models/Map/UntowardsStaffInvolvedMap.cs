using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
   public class UntowardsStaffInvolvedMap : IEntityTypeConfiguration<UntowardsStaffInvolved>
    {
        public void Configure(EntityTypeBuilder<UntowardsStaffInvolved> builder)
        {
            builder.ToTable("tbl_UntowardsStaffInvolved");
            builder.HasKey(k => k.UntowardsStaffInvolvedId);

            #region Properties
            builder.Property(p => p.UntowardsStaffInvolvedId)
               .HasColumnName("UntowardsStaffInvolvedId")
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
