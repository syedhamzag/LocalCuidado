using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffHolidayMap : IEntityTypeConfiguration<StaffHoliday>
    {
        public void Configure(EntityTypeBuilder<StaffHoliday> builder)
        {
            builder.ToTable("tbl_StaffHoliday");
            builder.HasKey(k => k.StaffHolidayId);

            #region Properties
            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.YearOfService)
              .HasColumnName("YearOfService")
              .IsRequired();

            builder.Property(p => p.AllocatedDays)
             .HasColumnName("AllocatedDays")
             .IsRequired();

            builder.Property(p => p.StartDate)
             .HasColumnName("StartDate")
             .IsRequired();

            builder.Property(p => p.EndDate)
             .HasColumnName("EndDate")
             .IsRequired();

            builder.Property(p => p.Days)
             .HasColumnName("Days")
             .IsRequired();

            builder.Property(p => p.Purpose)
             .HasColumnName("Purpose")
             .IsRequired();

            builder.Property(p => p.Class)
             .HasColumnName("Class")
             .IsRequired();

            builder.Property(p => p.PersonOnResponsibility)
             .HasColumnName("PersonOnResponsibility")
             .IsRequired();

            builder.Property(p => p.CopyOfHandover)
             .HasColumnName("CopyOfHandover")
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
                .WithMany(m => m.StaffHoliday)
                .HasForeignKey(f => f.StaffPersonalInfoId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
