using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HospitalEntryMap : IEntityTypeConfiguration<HospitalEntry>
    {
        public void Configure(EntityTypeBuilder<HospitalEntry> builder)
        {
            builder.ToTable("tbl_HospitalEntry");
            builder.HasKey(k => k.HospitalEntryId);

            #region Properties

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Reference)
               .HasColumnName("Reference")
               .IsRequired();

            builder.Property(p => p.Date)
              .HasColumnName("Date")
              .IsRequired();

            builder.Property(p => p.Time)
             .HasColumnName("Time")
             .IsRequired();

            builder.Property(p => p.PurposeofAdmission)
               .HasColumnName("PurposeofAdmission")
               .IsRequired();

            builder.Property(p => p.CauseofAdmission)
              .HasColumnName("CauseofAdmission")
              .IsRequired();

            builder.Property(p => p.LastDateofAdmission)
             .HasColumnName("LastDateofAdmission")
             .IsRequired();

            builder.Property(p => p.ConditionOfAdmission)
               .HasColumnName("ConditionOfAdmission")
               .IsRequired();

            builder.Property(p => p.IsFamilyInformed)
              .HasColumnName("IsFamilyInformed")
              .IsRequired();

            builder.Property(p => p.PossibleDateReturn)
             .HasColumnName("PossibleDateReturn")
             .IsRequired();

            builder.Property(p => p.IsHomeCleaned)
               .HasColumnName("IsHomeCleaned")
               .IsRequired();

            builder.Property(p => p.NameParamedicStaff)
              .HasColumnName("NameParamedicStaff")
              .IsRequired();

            builder.Property(p => p.ParamicStaffTeamNo)
             .HasColumnName("ParamicStaffTeamNo")
             .IsRequired();

            builder.Property(p => p.URLLINK)
               .HasColumnName("URLLINK")
               .IsRequired();

            builder.Property(p => p.MeansOfTransport)
              .HasColumnName("MeansOfTransport")
              .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
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
                .WithMany(p => p.HospitalEntry)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<HospitalEntryPersonToTakeAction>(p => p.PersonToTakeAction)
                .WithOne(p => p.HospitalEntry)
                .HasForeignKey(p => p.HospitalEntryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<HospitalEntryStaffInvolved>(p => p.StaffInvolved)
                .WithOne(p => p.HospitalEntry)
                .HasForeignKey(p => p.HospitalEntryId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
