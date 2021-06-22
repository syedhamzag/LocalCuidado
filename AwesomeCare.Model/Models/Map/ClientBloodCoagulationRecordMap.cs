using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientBloodCoagulationRecordMap : IEntityTypeConfiguration<ClientBloodCoagulationRecord>
    {
        public void Configure(EntityTypeBuilder<ClientBloodCoagulationRecord> builder)
        {
            builder.ToTable("tbl_ClientBloodCoagulationRecord");
            builder.HasKey(k => k.BloodRecordId);

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

            builder.Property(p => p.Indication)
               .HasColumnName("Indication")
               .IsRequired();

            builder.Property(p => p.TargetINR)
               .HasColumnName("TargetINR")
               .IsRequired();

            builder.Property(p => p.TargetINRAttach)
               .HasColumnName("TargetINRAttach");

            builder.Property(p => p.StartDate)
               .HasColumnName("StartDate")
               .IsRequired();

            builder.Property(p => p.CurrentDose)
               .HasColumnName("CurrentDose")
               .IsRequired();

            builder.Property(p => p.INR)
               .HasColumnName("INR")
               .IsRequired();

            builder.Property(p => p.NewDose)
               .HasColumnName("NewDose")
               .IsRequired();

            builder.Property(p => p.NewINR)
               .HasColumnName("NewINR")
               .IsRequired();

            builder.Property(p => p.BloodStatus)
             .HasColumnName("BloodStatus")
             .IsRequired();

            builder.Property(p => p.Comment)
               .HasColumnName("Comment")
               .IsRequired();


            builder.Property(p => p.PhysicianResponce)
               .HasColumnName("PhysicianResponce")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Remark)
               .HasColumnName("Remark")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientBloodCoagulationRecord)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<BloodCoagPhysician>(p => p.Physician)
                .WithOne(p => p.BloodCoagulation)
                .HasForeignKey(p => p.BloodRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<BloodCoagStaffName>(p => p.StaffName)
                .WithOne(p => p.BloodCoagulation)
                .HasForeignKey(p => p.BloodRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<BloodCoagOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.BloodCoagulation)
                .HasForeignKey(p => p.BloodRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
