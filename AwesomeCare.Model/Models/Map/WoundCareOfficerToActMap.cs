using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class WoundCareOfficerToActMap : IEntityTypeConfiguration<WoundCareOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<WoundCareOfficerToAct> builder)
        {
            builder.ToTable("tbl_WoundCareOfficerToAct");
            builder.HasKey(k => k.WoundCareOfficerToActId);

            #region Properties
            builder.Property(p => p.WoundCareOfficerToActId)
               .HasColumnName("WoundCareOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.WoundCareId)
             .HasColumnName("WoundCareId")
             .IsRequired();

            #endregion
        }
    }
}
