using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class VisitStaffNameMap : IEntityTypeConfiguration<VisitStaffName>
    {
        public void Configure(EntityTypeBuilder<VisitStaffName> builder)
        {
            builder.ToTable("tbl_VisitStaffName");
            builder.HasKey(k => k.VisitStaffNameId);

            #region Properties
            builder.Property(p => p.VisitStaffNameId)
               .HasColumnName("VisitStaffNameId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.VisitId)
             .HasColumnName("VisitId")
             .IsRequired();

            #endregion
        }
    }
}
