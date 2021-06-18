using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientLogAuditMap : IEntityTypeConfiguration<ClientLogAudit>
    {
        public void Configure(EntityTypeBuilder<ClientLogAudit> builder)
        {
            builder.ToTable("tbl_ClientLogAudit");
            builder.HasKey(k => k.LogAuditId);

            #region Properties
            builder.Property(p => p.Reference)
               .HasColumnName("Reference")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.NextDueDate)
               .HasColumnName("NextDueDate")
               .IsRequired();

            builder.Property(p => p.IsCareExpected)
               .HasColumnName("IsCareExpected")
               .IsRequired();

            builder.Property(p => p.IsCareDifference)
               .HasColumnName("IsCareDifference")
               .IsRequired();

            builder.Property(p => p.ProperDocumentation)
               .HasColumnName("ProperDocumentation")
               .IsRequired();

            builder.Property(p => p.ImproperDocumentation)
               .HasColumnName("ImproperDocumentation")
               .IsRequired();

            builder.Property(p => p.Communication)
               .HasColumnName("Communication")
               .IsRequired();

            builder.Property(p => p.ThinkingServiceUsers)
               .HasColumnName("ThinkingServiceUsers")
               .IsRequired();

            builder.Property(p => p.ThinkingStaff)
               .HasColumnName("ThinkingStaff")
               .IsRequired();

            builder.Property(p => p.ThinkingStaffStop)
               .HasColumnName("ThinkingStaffStop")
               .IsRequired();

            builder.Property(p => p.Observations)
               .HasColumnName("Observations")
               .IsRequired();

            builder.Property(p => p.NameOfAuditor)
               .HasColumnName("NameOfAuditor")
               .IsRequired();

            builder.Property(p => p.ActionRecommended)
               .HasColumnName("ActionRecommended")
               .IsRequired();

            builder.Property(p => p.ActionTaken)
               .HasColumnName("ActionTaken")
               .IsRequired();

            builder.Property(p => p.EvidenceOfActionTaken)
             .HasColumnName("EvidenceOfActionTaken")
             .IsRequired();

            builder.Property(p => p.OfficerToTakeAction)
               .HasColumnName("OfficerToTakeAction")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .IsRequired();

            builder.Property(p => p.RepeatOfIncident)
             .HasColumnName("RepeatOfIncident")
             .IsRequired();

            builder.Property(p => p.RotCause)
             .HasColumnName("RotCause")
             .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.LessonLearntAndShared)
             .HasColumnName("LessonLearntAndShared")
             .IsRequired();

            builder.Property(p => p.LogURL)
             .HasColumnName("LogURL")
             .IsRequired();

            builder.Property(p => p.EvidenceFilePath)
             .HasColumnName("EvidenceFilePath")
             .IsRequired(false);







            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientLogAudit)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.ClientLogAudit)
                 .HasForeignKey(p => p.OfficerToTakeAction)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
