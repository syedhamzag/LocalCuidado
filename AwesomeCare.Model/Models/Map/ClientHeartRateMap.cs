using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientHeartRateMap : IEntityTypeConfiguration<ClientHeartRate>
    {
        public void Configure(EntityTypeBuilder<ClientHeartRate> builder)
        {
            builder.ToTable("tbl_ClientHeartRate");
            builder.HasKey(k => k.HeartRateId);

            #region Properties
            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.Time)
               .HasColumnName("Time")
               .IsRequired();

            builder.Property(p => p.TargetHR)
               .HasColumnName("TargetHR")
               .IsRequired();

            builder.Property(p => p.TargetHRAttach)
               .HasColumnName("TargetHRAttach");

            builder.Property(p => p.Gender)
               .HasColumnName("Gender")
               .IsRequired();

            builder.Property(p => p.GenderAttach)
               .HasColumnName("GenderAttach");

            builder.Property(p => p.Age)
               .HasColumnName("Age")
               .IsRequired();

            builder.Property(p => p.BeatsPerSeconds)
               .HasColumnName("BeatsPerSeconds")
               .IsRequired();

            builder.Property(p => p.SeeChart)
               .HasColumnName("SeeChart")
               .IsRequired();

            builder.Property(p => p.SeeChartAttach)
               .HasColumnName("SeeChartAttach");

            builder.Property(p => p.Comment)
               .HasColumnName("Comment")
               .IsRequired();

            builder.Property(p => p.PhysicianResponse)
               .HasColumnName("PhysicianResponse")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientHeartRate)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<HeartRatePhysician>(p => p.Physician)
                .WithOne(p => p.HeartRate)
                .HasForeignKey(p => p.HeartRateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<HeartRateStaffName>(p => p.StaffName)
                .WithOne(p => p.HeartRate)
                .HasForeignKey(p => p.HeartRateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<HeartRateOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.HeartRate)
                .HasForeignKey(p => p.HeartRateId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
