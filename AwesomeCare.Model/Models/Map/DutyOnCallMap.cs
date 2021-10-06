using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class DutyOnCallMap : IEntityTypeConfiguration<DutyOnCall>
    {
        public void Configure(EntityTypeBuilder<DutyOnCall> builder)
        {
            builder.ToTable("tbl_DutyOnCall");
            builder.HasKey(k => k.DutyOnCallId);

            #region Properties
            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();          

            builder.Property(p => p.ActionTaken)
             .HasColumnName("ActionTaken")
             .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired();

            builder.Property(p => p.ClientInitial)
             .HasColumnName("ClientInitial")
             .IsRequired();

            builder.Property(p => p.DateOfCall)
             .HasColumnName("DateOfCall")
             .IsRequired();

            builder.Property(p => p.DateOfIncident)
             .HasColumnName("DateOfIncident")
             .IsRequired();

            builder.Property(p => p.DetailsOfIncident)
             .HasColumnName("DetailsOfIncident")
             .IsRequired();

            builder.Property(p => p.DetailsRequired)
             .HasColumnName("DetailsRequired")
             .IsRequired();

            builder.Property(p => p.NotifyPerson)
              .HasColumnName("NotifyPerson")
              .IsRequired();

            builder.Property(p => p.NotifyStaffInvolved)
             .HasColumnName("NotifyStaffInvolved")
             .IsRequired();

            builder.Property(p => p.PositionOfReporting)
             .HasColumnName("PositionOfReporting")
             .IsRequired();

            builder.Property(p => p.Priority)
             .HasColumnName("Priority")
             .IsRequired();

            builder.Property(p => p.RefNo)
             .HasColumnName("RefNo")
             .IsRequired();

            builder.Property(p => p.Remarks)
             .HasColumnName("Remarks")
             .IsRequired();

            builder.Property(p => p.ReportedBy)
             .HasColumnName("ReportedBy")
             .IsRequired();

            builder.Property(p => p.StaffBlacklisted)
             .HasColumnName("StaffBlacklisted")
             .IsRequired();

            builder.Property(p => p.NotificationStatus)
             .HasColumnName("NotificationStatus")
             .IsRequired();

            builder.Property(p => p.Status)
             .HasColumnName("Status")
             .IsRequired();

            builder.Property(p => p.Subject)
             .HasColumnName("Subject")
             .IsRequired();

            builder.Property(p => p.TelephoneToCall)
             .HasColumnName("TelephoneToCall")
             .IsRequired();

            builder.Property(p => p.TimeOfCall)
             .HasColumnName("TimeOfCall")
             .IsRequired();

            builder.Property(p => p.TypeOfDutyCall)
             .HasColumnName("TypeOfDutyCall")
             .IsRequired();

            builder.Property(p => p.TypeOfIncident)
             .HasColumnName("TypeOfIncident")
             .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.DutyOnCall)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<DutyOnCallPersonResponsible>(p => p.PersonResponsible)
                .WithOne(p => p.DutyOnCall)
                .HasForeignKey(p => p.DutyOnCallId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<DutyOnCallPersonToAct>(p => p.PersonToAct)
                .WithOne(p => p.DutyOnCall)
                .HasForeignKey(p => p.DutyOnCallId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
