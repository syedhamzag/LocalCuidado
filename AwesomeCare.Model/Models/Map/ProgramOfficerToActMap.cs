using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ProgramOfficerToActMap : IEntityTypeConfiguration<ProgramOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<ProgramOfficerToAct> builder)
        {
            builder.ToTable("tbl_ProgramOfficerToAct");
            builder.HasKey(k => k.ProgramOfficerToActId);

            #region Properties
            builder.Property(p => p.ProgramOfficerToActId)
               .HasColumnName("ProgramOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.ProgramId)
             .HasColumnName("ProgramId")
             .IsRequired();

            #endregion
        }
    }
}
