using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class EyeHealthOfficerToActMap : IEntityTypeConfiguration<EyeHealthOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<EyeHealthOfficerToAct> builder)
        {
            builder.ToTable("tbl_EyeHealthOfficerToAct");
            builder.HasKey(k => k.EyeHealthOfficerToActId);

            #region Properties
            builder.Property(p => p.EyeHealthOfficerToActId)
               .HasColumnName("EyeHealthOfficerToActId")
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
