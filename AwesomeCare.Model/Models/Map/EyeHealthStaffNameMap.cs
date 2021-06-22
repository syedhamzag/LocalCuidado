using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class EyeHealthStaffNameMap : IEntityTypeConfiguration<EyeHealthStaffName>
    {
        public void Configure(EntityTypeBuilder<EyeHealthStaffName> builder)
        {
            builder.ToTable("tbl_EyeHealthStaffName");
            builder.HasKey(k => k.EyeHealthStaffNameId);

            #region Properties
            builder.Property(p => p.EyeHealthStaffNameId)
               .HasColumnName("EyeHealthStaffNameId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.EyeHealthId)
             .HasColumnName("EyeHealthId")
             .IsRequired();

            #endregion
        }
    }
}
