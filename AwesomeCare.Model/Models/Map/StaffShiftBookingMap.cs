using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffShiftBookingMap : IEntityTypeConfiguration<StaffShiftBooking>
    {
        public void Configure(EntityTypeBuilder<StaffShiftBooking> builder)
        {
            builder.ToTable("tbl_StaffShiftBooking");
            builder.HasKey(k => k.StaffShiftBookingId);

            #region Properties
            builder.Property(p => p.StaffShiftBookingId)
                .HasColumnName("StaffShiftBookingId")
                .IsRequired();

            builder.Property(p => p.RotaId)
               .HasColumnName("RotaId")
               .IsRequired();

            builder.Property(p => p.MonthIndex)
              .HasColumnName("MonthIndex")
              .IsRequired();

            builder.Property(p => p.MonthName)
              .HasColumnName("MonthName")
              .HasMaxLength(25)
              .IsRequired();

            builder.Property(p => p.Year)
              .HasColumnName("Year")
              .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();
            #endregion

            #region Relationship    
            builder.HasOne(p => p.StaffPersonalInfo)
                .WithMany(p=>p.ShiftBookings)
                .HasForeignKey(k=>k.StaffPersonalInfoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Rota)
                .WithMany(p => p.ShiftBookings)
                .HasForeignKey(k => k.RotaId);
              
            #endregion
        }
    }
}
