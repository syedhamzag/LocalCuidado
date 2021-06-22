using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class OxygenLvlStaffNameMap : IEntityTypeConfiguration<OxygenLvlStaffName>
    {
        public void Configure(EntityTypeBuilder<OxygenLvlStaffName> builder)
        {
            builder.ToTable("tbl_OxygenLvlStaffName");
            builder.HasKey(k => k.OxygenLvlStaffNameId);

            #region Properties
            builder.Property(p => p.OxygenLvlStaffNameId)
               .HasColumnName("OxygenLvlStaffNameId")
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
