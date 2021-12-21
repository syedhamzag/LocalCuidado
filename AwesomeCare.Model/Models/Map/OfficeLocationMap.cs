using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class OfficeLocationMap : IEntityTypeConfiguration<OfficeLocation>
    {
        public void Configure(EntityTypeBuilder<OfficeLocation> builder)
        {
            builder.ToTable("tbl_OfficeLocation");
            builder.HasKey(k => k.OfficeLocationId);

            #region Properties

            builder.Property(p => p.OfficeLocationId)
                .HasColumnName("OfficeLocationId")
                .IsRequired();

            builder.Property(p => p.UniqueId)
                .HasColumnName("UniqueId")
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(p => p.Address)
               .HasColumnName("Address")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Latitude)
              .HasColumnName("Latitude")
              .HasMaxLength(255)
              .IsRequired(false);

            builder.Property(p => p.Longitude)
             .HasColumnName("Longitude")
             .HasMaxLength(255)
             .IsRequired(false);

            builder.Property(p => p.ContactPersonFullName)
             .HasColumnName("ContactPersonFullName")
             .HasMaxLength(255)
             .IsRequired(false);

            builder.Property(p => p.ContactPersonEmail)
             .HasColumnName("ContactPersonEmail")
             .HasMaxLength(255)
             .IsRequired(false);

            builder.Property(p => p.ContactPersonPhoneNumber)
             .HasColumnName("ContactPersonPhoneNumber")
             .HasMaxLength(255)
             .IsRequired(false);
            #endregion
        }
    }
}
