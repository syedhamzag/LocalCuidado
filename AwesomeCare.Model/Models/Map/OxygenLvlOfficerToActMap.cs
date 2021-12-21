using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class OxygenLvlOfficerToActMap : IEntityTypeConfiguration<OxygenLvlOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<OxygenLvlOfficerToAct> builder)
        {
            builder.ToTable("tbl_OxygenLvl_OfficerToAct");
            builder.HasKey(k => k.OxygenLvlOfficerToActId);

            #region Properties
            builder.Property(p => p.OxygenLvlOfficerToActId)
               .HasColumnName("OxygenLvlOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.OxygenLvlId)
             .HasColumnName("OxygenLvlId")
             .IsRequired();

            #endregion
        }
    }
}
