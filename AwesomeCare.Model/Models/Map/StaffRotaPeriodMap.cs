using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffRotaPeriodMap : IEntityTypeConfiguration<StaffRotaPeriod>
    {
        public void Configure(EntityTypeBuilder<StaffRotaPeriod> builder)
        {
            builder.ToTable("tbl_StaffRotaPeriod");
            builder.HasKey(k => k.StaffRotaPeriodId);

            #region Properties
            builder.Property(p => p.StaffRotaPeriodId)
                .HasColumnName("StaffRotaPeriodId")
                .IsRequired();

            builder.Property(p => p.StaffRotaId)
                .HasColumnName("StaffRotaId")
                .IsRequired();

            builder.Property(p => p.ClientRotaTypeId)
              .HasColumnName("ClientRotaTypeId")
              .IsRequired();

            builder.Property(p => p.ClockInTime)
             .HasColumnName("ClockInTime")
             .HasColumnType("datetimeoffset")
             .IsRequired(false);

            builder.Property(p => p.ClockOutTime)
            .HasColumnName("ClockOutTime")
             .HasColumnType("datetimeoffset")
            .IsRequired(false);

            builder.Property(p => p.ClockInAddress)
           .HasColumnName("ClockInAddress")
           .HasMaxLength(300)
           .IsRequired(false);

            builder.Property(p => p.ClockOutAddress)
         .HasColumnName("ClockOutAddress")
         .HasMaxLength(300)
         .IsRequired(false);


            builder.Property(p => p.Feedback)
         .HasColumnName("Feedback")
         .HasMaxLength(225)
         .IsRequired(false);


            builder.Property(p => p.Comment)
         .HasColumnName("Comment")
         .HasMaxLength(225)
         .IsRequired(false);

            builder.Property(p => p.HandOver)
        .HasColumnName("HandOver")
        .HasMaxLength(225)
        .IsRequired(false);

            builder.Property(p => p.ClockInMode)
               .HasColumnName("ClockInMode")
               .HasMaxLength(225)
               .IsRequired(false);

            builder.Property(p => p.ClockOutMode)
               .HasColumnName("ClockOutMode")
               .HasMaxLength(225)
               .IsRequired(false);

            builder.Property(p => p.StartTime)
              .HasColumnName("StartTime")
              .HasMaxLength(50)
              .IsRequired(false);

            builder.Property(p => p.StopTime)
              .HasColumnName("StopTime")
              .HasMaxLength(50)
              .IsRequired(false);

            builder.Property(p => p.ClientId)
             .HasColumnName("ClientId")
             .IsRequired(false);


            builder.Property(p => p.BowelMovement)
             .HasColumnName("BowelMovement")
             .HasMaxLength(5)
             .IsRequired(false);

            builder.Property(p => p.OralCare)
              .HasColumnName("OralCare")
              .HasMaxLength(5)
              .IsRequired(false);

            builder.Property(p => p.FluidIntake)
             .HasColumnName("FluidIntake")
             .HasMaxLength(5)
             .IsRequired(false);

            builder.Property(p => p.ClockInClientTelephone)
                .HasColumnName("ClockInClientTelephone")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(p => p.ClockOutClientTelephone)
               .HasColumnName("ClockOutClientTelephone")
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(p => p.ClockInGeolocation)
              .HasColumnName("ClockInGeolocation")
              .HasMaxLength(1000)
              .IsRequired(false);

            builder.Property(p => p.ClockOutGeolocation)
             .HasColumnName("ClockOutGeolocation")
             .HasMaxLength(1000)
             .IsRequired(false);

            builder.Property(p => p.ClockOutClientTelephone)
              .HasColumnName("ClockOutClientTelephone")
              .HasMaxLength(50)
              .IsRequired(false);

            builder.Property(p => p.ClockInDistance)
            .HasColumnName("ClockInDistance")
            .HasMaxLength(50)
            .IsRequired(false);

            builder.Property(p => p.ClockOutDistance)
            .HasColumnName("ClockOutDistance")
            .HasMaxLength(50)
            .IsRequired(false);

            builder.Property(p => p.ClockInCount)
           .HasColumnName("ClockInCount")
           .IsRequired(false);

            builder.Property(p => p.ClockOutCount)
            .HasColumnName("ClockOutCount")
            .IsRequired(false);
            #endregion

            #region Relationship
            builder.HasOne(b => b.StaffRota)
                .WithMany(b => b.StaffRotaPeriods)
                .HasForeignKey(k => k.StaffRotaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.ClientRotaType)
                .WithMany(c => c.StaffRotaPeriods)
                .HasForeignKey(f => f.ClientRotaTypeId);
            #endregion
        }
    }
}
