using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ShiftBookingBlockedDaysMap : IEntityTypeConfiguration<ShiftBookingBlockedDays>
    {
        public void Configure(EntityTypeBuilder<ShiftBookingBlockedDays> builder)
        {
            builder.ToTable("tbl_ShiftBookingBlockedDays");
            builder.HasKey(k => k.ShiftBookingBlockedDaysId);

            #region Properties
            builder.Property(p => p.ShiftBookingBlockedDaysId)
                .HasColumnName("ShiftBookingBlockedDaysId")
                .IsRequired();

            builder.Property(p => p.ShiftBookingId)
               .HasColumnName("ShiftBookingId")
               .IsRequired();

            builder.Property(p => p.Day)
              .HasColumnName("Day")
              .HasMaxLength(2)
              .IsRequired();

            builder.Property(p => p.WeekDay)
             .HasColumnName("WeekDay")
             .HasMaxLength(15)
             .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne(r => r.ShiftBooking)
                .WithMany(m => m.ShiftBookingBlockedDays)
                .HasForeignKey(f=>f.ShiftBookingId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

        }
    }
}
