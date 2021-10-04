using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class DutyOnCallPersonToActMap : IEntityTypeConfiguration<DutyOnCallPersonToAct>
    {
        public void Configure(EntityTypeBuilder<DutyOnCallPersonToAct> builder)
        {
            builder.ToTable("tbl_DutyOnCallPersonToAct");
            builder.HasKey(k => k.PersonToActId);

            #region Properties
            builder.Property(p => p.PersonToActId)
               .HasColumnName("PersonToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.DutyOnCallId)
             .HasColumnName("DutyOnCallId")
             .IsRequired();

            #endregion
        }
    }
}
