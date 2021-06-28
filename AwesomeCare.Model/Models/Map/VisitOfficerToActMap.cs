using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class VisitOfficerToActMap : IEntityTypeConfiguration<VisitOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<VisitOfficerToAct> builder)
        {
            builder.ToTable("tbl_Visit_OfficerToAct");
            builder.HasKey(k => k.VisitOfficerToActId);

            #region Properties
            builder.Property(p => p.VisitOfficerToActId)
               .HasColumnName("VisitOfficerToActId")
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
