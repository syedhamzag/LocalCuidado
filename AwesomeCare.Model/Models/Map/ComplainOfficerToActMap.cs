using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ComplainOfficerToActMap : IEntityTypeConfiguration<ComplainOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<ComplainOfficerToAct> builder)
        {
            builder.ToTable("tbl_ComplainOfficerToAct");
            builder.HasKey(k => k.ComplainOfficerToActId);

            #region Properties
            builder.Property(p => p.ComplainOfficerToActId)
               .HasColumnName("ComplainOfficerToActId")
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
