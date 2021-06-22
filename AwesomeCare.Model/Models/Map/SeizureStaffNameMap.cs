using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class SeizureStaffNameMap : IEntityTypeConfiguration<SeizureStaffName>
    {
        public void Configure(EntityTypeBuilder<SeizureStaffName> builder)
        {
            builder.ToTable("tbl_SeizureStaffName");
            builder.HasKey(k => k.SeizureStaffNameId);

            #region Properties
            builder.Property(p => p.SeizureStaffNameId)
               .HasColumnName("SeizureStaffNameId")
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
