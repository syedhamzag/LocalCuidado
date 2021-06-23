using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientMedAuditMap : IEntityTypeConfiguration<ClientMedAudit>
    {
        public void Configure(EntityTypeBuilder<ClientMedAudit> builder)
        {
            builder.ToTable("tbl_Client_MedAudit");
            builder.HasKey(k => k.MedAuditId);

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

            builder.Property(p => p.GapsInAdmistration)
               .HasColumnName("GapsInAdmistration")
               .IsRequired();

            builder.Property(p => p.RightsOfMedication)
               .HasColumnName("RightsOfMedication")
               .IsRequired();

            builder.Property(p => p.MarChartReview)
               .HasColumnName("MarChartReview")
               .IsRequired();

            builder.Property(p => p.MedicationConcern)
               .HasColumnName("MedicationConcern")
               .IsRequired();

            builder.Property(p => p.HardCopyReview)
               .HasColumnName("HardCopyReview")
               .IsRequired();

            builder.Property(p => p.ThinkingServiceUsers)
               .HasColumnName("ThinkingServiceUsers")
               .IsRequired();

            builder.Property(p => p.MedicationSupplyEfficiency)
               .HasColumnName("MedicationSupplyEfficiency")
               .IsRequired();

            builder.Property(p => p.MedicationInfoUploadEefficiency)
               .HasColumnName("MedicationInfoUploadEefficiency")
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

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired(false);
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientMedAudit)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.ClientMedAudit)
                 .HasForeignKey(p => p.OfficerToTakeAction)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
