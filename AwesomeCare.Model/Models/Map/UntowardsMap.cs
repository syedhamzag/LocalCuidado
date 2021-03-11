using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class UntowardsMap : IEntityTypeConfiguration<Untowards>
    {
        public void Configure(EntityTypeBuilder<Untowards> builder)
        {
            builder.ToTable("tbl_Untowards");
            builder.HasKey(k => k.UntowardsId);

            #region Properties
            builder.Property(p => p.UntowardsId)
                .HasColumnName("UntowardsId")
                .IsRequired();

            builder.Property(p => p.TicketNumber)
              .HasColumnName("TicketNumber")
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(p => p.Date)
             .HasColumnName("Date")
              .HasMaxLength(15)
             .IsRequired();

            builder.Property(p => p.Subject)
             .HasColumnName("Subject")
             .HasMaxLength(225)
             .IsRequired();

            builder.Property(p => p.TimeOfCall)
             .HasColumnName("TimeOfCall")
             .HasMaxLength(15)
             .IsRequired();

            builder.Property(p => p.PersonReporting)
             .HasColumnName("PersonReporting")
             .HasMaxLength(100)
             .IsRequired(false);

            builder.Property(p => p.PersonReportingTelephone)
             .HasColumnName("PersonReportingTelephone")
             .HasMaxLength(50)
             .IsRequired(false);

            builder.Property(p => p.PersonReportingEmail)
             .HasColumnName("PersonReportingEmail")
             .HasMaxLength(225)
             .IsRequired(false);

            builder.Property(p => p.Details)
             .HasColumnName("Details")
             .HasMaxLength(225)
             .IsRequired();

            builder.Property(p => p.ActionStatus)
             .HasColumnName("ActionStatus")
             .HasMaxLength(7)
             .IsRequired();

            builder.Property(p => p.Priority)
             .HasColumnName("Priority")
             .HasMaxLength(7)
             .IsRequired();

            builder.Property(p => p.ActionTaken)
             .HasColumnName("ActionTaken")
             .HasMaxLength(225)
             .IsRequired();

            builder.Property(p => p.ActionRequired)
             .HasColumnName("ActionRequired")
             .HasMaxLength(225)
             .IsRequired();

            builder.Property(p => p.FinalActionToCloseCase)
             .HasColumnName("FinalActionToCloseCase")
             .HasMaxLength(225)
             .IsRequired(false);

            builder.Property(p => p.ExpectedDateAndTimeOfFeedback)
             .HasColumnName("ExpectedDateAndTimeOfFeedback")
             .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.IsBlackListRequired)
             .HasColumnName("IsBlackListRequired")
             .IsRequired();

            builder.Property(p => p.HomeCareClientId)
            .HasColumnName("HomeCareClientId")
            .IsRequired();

            builder.Property(p => p.IsHospitalEntry)
             .HasColumnName("IsHospitalEntry")
             .IsRequired();

            builder.Property(p => p.HospitalEntryReason)
             .HasColumnName("HospitalEntryReason")
             .HasMaxLength(225)
             .IsRequired(false);

            builder.Property(p => p.IsHospitalExit)
             .HasColumnName("IsHospitalExit")
             .IsRequired();

            builder.Property(p => p.HospitalExitDetails)
            .HasColumnName("HospitalExitDetails")
            .HasMaxLength(225)
            .IsRequired(false);

            builder.Property(p => p.ShouldNotifyInvolvingStaff)
             .HasColumnName("ShouldNotifyInvolvingStaff")
             .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired(false);

            builder.Property(p => p.Others)
             .HasColumnName("Others")
             .HasMaxLength(225)
             .IsRequired(false);

            builder.Property(p => p.TypeofRequiredNotification)
             .HasColumnName("TypeofRequiredNotification")
             .IsRequired();

            builder.Property(p => p.EntryHospitalName)
           .HasColumnName("EntryHospitalName")
           .HasMaxLength(250)
           .IsRequired(false);

            builder.Property(p => p.ExitHospitalName)
          .HasColumnName("ExitHospitalName")
          .HasMaxLength(250)
          .IsRequired(false);
            #endregion

            #region Relationship
            builder.HasMany<UntowardsStaffInvolved>(p => p.StaffInvolved)
                .WithOne(p => p.Untowards)
                .HasForeignKey(p => p.UntowardsId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<UntowardsOfficerToAct>(p => p.OfficerToAct)
               .WithOne(p => p.Untowards)
               .HasForeignKey(p => p.UntowardsId)
               .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
