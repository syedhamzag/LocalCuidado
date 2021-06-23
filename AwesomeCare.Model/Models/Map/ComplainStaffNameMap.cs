using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ComplainStaffNameMap : IEntityTypeConfiguration<ComplainStaffName>
    {
        public void Configure(EntityTypeBuilder<ComplainStaffName> builder)
        {
            builder.ToTable("tbl_Complain_StaffName");
            builder.HasKey(k => k.ComplainStaffNameId);

            #region Properties
            builder.Property(p => p.ComplainStaffNameId)
               .HasColumnName("ComplainStaffNameId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.ComplainId)
             .HasColumnName("ComplainId")
             .IsRequired();

            #endregion
        }
    }
}
