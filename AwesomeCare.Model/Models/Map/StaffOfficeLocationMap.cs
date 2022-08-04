using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffOfficeLocationMap : IEntityTypeConfiguration<StaffOfficeLocation>
    {
        public void Configure(EntityTypeBuilder<StaffOfficeLocation> builder)
        {
            builder.ToTable("tbl_StaffOfficeLocation");
            builder.HasKey(k => k.Id);

            #region Properties
            builder.Property(p => p.Location)
               .HasColumnName("Location")
               .IsRequired();

            builder.Property(p => p.Staff)
              .HasColumnName("Staff")
              .IsRequired();
            #endregion
        }
    }
}

