using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ShiftBookingMap : IEntityTypeConfiguration<ShiftBooking>
    {
        public void Configure(EntityTypeBuilder<ShiftBooking> builder)
        {
            builder.ToTable("tbl_ShiftBooking");
            builder.HasKey(p => p.ShiftBookingId);

            #region Properties
            builder.Property(p => p.ShiftBookingId)
                .HasColumnName("ShiftBookingId")
                .IsRequired();

            builder.Property(p => p.ShiftDate)
                .HasColumnName("ShiftDate")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(p => p.Rota)
               .HasColumnName("Rota")
               .IsRequired();

            builder.Property(p => p.NumberOfStaff)
               .HasColumnName("NumberOfStaff")
               .IsRequired();

            builder.Property(p => p.StartTime)
               .HasColumnName("StartTime")
               .HasMaxLength(15)
               .IsRequired();

            builder.Property(p => p.StopTime)
               .HasColumnName("StopTime")
               .HasMaxLength(15)
               .IsRequired();

            builder.Property(p => p.Remark)
               .HasColumnName("Remark")
                .HasMaxLength(225)
               .IsRequired();

            builder.Property(p => p.Team)
               .HasColumnName("Team_StaffPersonalInfoId")
               .IsRequired();

            builder.Property(p => p.DriverRequired)
              .HasColumnName("DriverRequired")
              .IsRequired();

            builder.Property(p => p.PublishTo)
              .HasColumnName("PublishTo")
              .IsRequired(false);
            #endregion
        }
    }
}
