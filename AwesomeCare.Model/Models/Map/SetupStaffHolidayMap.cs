using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class SetupStaffHolidayMap : IEntityTypeConfiguration<SetupStaffHoliday>
    {
        public void Configure(EntityTypeBuilder<SetupStaffHoliday> builder)
        {
            builder.ToTable("tbl_SetupStaffHoliday");
            builder.HasKey(k => k.SetupHolidayId);

            #region Properties
            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.YearOfEmployment)
              .HasColumnName("YearOfEmployment")
              .IsRequired();

            builder.Property(p => p.TypeOfHoliday)
             .HasColumnName("TypeOfHoliday")
             .IsRequired();

            builder.Property(p => p.YearEndPeriodStartDate)
             .HasColumnName("YearEndPeriodStartDate")
             .IsRequired();

            builder.Property(p => p.HoursSoFar)
             .HasColumnName("HoursSoFar")
             .IsRequired();

            builder.Property(p => p.IncrementalDailyHolidayByHrs)
             .HasColumnName("IncrementalDailyHolidayByHrs")
             .IsRequired();

            builder.Property(p => p.NumberOfDays)
             .HasColumnName("NumberOfDays")
             .IsRequired();

            builder.Property(p => p.Remark)
             .HasColumnName("Remark")
             .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.StaffPersonalInfo)
                    .WithMany(m => m.SetupStaffHoliday)
                    .HasForeignKey(f => f.StaffPersonalInfoId)
                    .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
