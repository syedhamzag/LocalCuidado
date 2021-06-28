using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class MedCompOfficerToActMap : IEntityTypeConfiguration<MedCompOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<MedCompOfficerToAct> builder)
        {
            builder.ToTable("tbl_MedComp_OfficerToAct");
            builder.HasKey(k => k.MedCompOfficerToActId);

            #region Properties
            builder.Property(p => p.MedCompOfficerToActId)
               .HasColumnName("MedCompOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.MedCompId)
             .HasColumnName("MedCompId")
             .IsRequired();

            #endregion
        }
    }
}
