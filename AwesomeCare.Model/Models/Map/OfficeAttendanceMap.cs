using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class OfficeAttendanceMap : IEntityTypeConfiguration<OfficeAttendance>
    {
        public void Configure(EntityTypeBuilder<OfficeAttendance> builder)
        {
            builder.ToTable("tbl_OfficeAttendance");
            builder.HasKey(k => k.AttendanceId);

            #region Properties

            builder.Property(p => p.Date)
                .HasColumnName("Date")
                .IsRequired();

            builder.Property(p => p.Staff)
               .HasColumnName("Staff")
               .IsRequired();

            builder.Property(p => p.Location)
               .HasColumnName("Location")
               .IsRequired(false);

            builder.Property(p => p.JobTitle)
               .HasColumnName("JobTitle")
               .IsRequired(false);

            builder.Property(p => p.ClockDiff)
               .HasColumnName("ClockDiff")
               .IsRequired(false);

            builder.Property(p => p.ClockIn)
               .HasColumnName("ClockIn")
               .IsRequired(false);

            builder.Property(p => p.ClockInAddress)
               .HasColumnName("ClockInAddress")
               .IsRequired(false);

            builder.Property(p => p.ClockInDistance)
               .HasColumnName("ClockInDistance")
               .IsRequired(false);

            builder.Property(p => p.ClockInMethod)
               .HasColumnName("ClockInMethod")
               .IsRequired(false);

            builder.Property(p => p.ClockOut)
               .HasColumnName("ClockOut")
               .IsRequired(false);

            builder.Property(p => p.ClockOutAddress)
               .HasColumnName("ClockOutAddress")
               .IsRequired(false);

            builder.Property(p => p.ClockOutDistance)
               .HasColumnName("ClockOutDistance")
               .IsRequired(false);

            builder.Property(p => p.ClockOutMethod)
               .HasColumnName("ClockOutMethod")
               .IsRequired(false);

            builder.Property(p => p.Remark)
               .HasColumnName("Remark")
               .IsRequired(false);

            builder.Property(p => p.StartTime)
               .HasColumnName("StartTime")
               .IsRequired(false);

            builder.Property(p => p.StopTime)
               .HasColumnName("StopTime")
               .IsRequired(false);
            #endregion
        }
    }
}
