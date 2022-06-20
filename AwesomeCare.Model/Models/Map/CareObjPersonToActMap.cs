using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class CareObjPersonToActMap : IEntityTypeConfiguration<CareObjPersonToAct>
    {
        public void Configure(EntityTypeBuilder<CareObjPersonToAct> builder)
        {
            builder.ToTable("tbl_CareObjPersonToAct");
            builder.HasKey(k => k.Id);

            #region Properties
            builder.Property(p => p.Id)
               .HasColumnName("Id")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.CareObjId)
             .HasColumnName("CareObjId")
             .IsRequired();

            #endregion
        }
    }
}
