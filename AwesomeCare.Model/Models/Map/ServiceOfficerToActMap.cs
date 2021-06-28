using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ServiceOfficerToActMap : IEntityTypeConfiguration<ServiceOfficerToAct>
    {
        public void Configure(EntityTypeBuilder<ServiceOfficerToAct> builder)
        {
            builder.ToTable("tbl_Service_OfficerToAct");
            builder.HasKey(k => k.ServiceOfficerToActId);

            #region Properties
            builder.Property(p => p.ServiceOfficerToActId)
               .HasColumnName("ServiceOfficerToActId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.ServiceId)
             .HasColumnName("ServiceId")
             .IsRequired();

            #endregion
        }
    }
}
