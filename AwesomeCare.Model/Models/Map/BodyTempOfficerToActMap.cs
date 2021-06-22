using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BodyTempOfficerToActMap : IEntityTypeConfiguration<BodyTempOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<BodyTempOfficerToAct> builder)
        {
            builder.ToTable("tbl_BodyTempOfficerToAct");
            builder.HasKey(k => k.BodyTempOfficerToActId);

            #region Properties
            builder.Property(p => p.BodyTempOfficerToActId)
               .HasColumnName("BodyTempOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.BodyTempId)
             .HasColumnName("BodyTempId")
             .IsRequired();

            #endregion
        }
    }
}
