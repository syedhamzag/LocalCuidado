using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class OneToOneOfficerToActMap : IEntityTypeConfiguration<OneToOneOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<OneToOneOfficerToAct> builder)
        {
            builder.ToTable("tbl_OneToOneOfficerToAct");
            builder.HasKey(k => k.OneToOneOfficerToActId);

            #region Properties
            builder.Property(p => p.OneToOneOfficerToActId)
               .HasColumnName("OneToOneOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.OneToOneId)
             .HasColumnName("OneToOneId")
             .IsRequired();

            #endregion
        }
    }
}
