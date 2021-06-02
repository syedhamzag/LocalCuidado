using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientMgtVisitMap : IEntityTypeConfiguration<ClientMgtVisit>
    {
        public void Configure(EntityTypeBuilder<ClientMgtVisit> builder)
        {
            builder.ToTable("tbl_ClientMgtVisit");
            builder.HasKey(k => k.VisitId);

            #region Properties
            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.NextCheckDate)
               .HasColumnName("NextCheckDate")
               .IsRequired();

            builder.Property(p => p.ServiceRecommended)
               .HasColumnName("ServiceRecommended")
               .IsRequired();

            builder.Property(p => p.StaffBestSupport)
               .HasColumnName("StaffBestSupport")
               .IsRequired();

            builder.Property(p => p.RateServiceRecieving)
               .HasColumnName("RateServiceRecieving")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.RateManagers)
               .HasColumnName("RateManagers")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Observation)
               .HasColumnName("Observation")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.HowToComplain)
               .HasColumnName("HowToComplain")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.EvidenceOfActionTaken)
             .HasColumnName("EvidenceOfActionTaken")
             .IsRequired();

            builder.Property(p => p.OfficerToAct)
               .HasColumnName("OfficerToAct")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.ImprovementExpect)
             .HasColumnName("ImprovementExpect")
             .IsRequired();

            builder.Property(p => p.ActionRequired)
             .HasColumnName("ActionRequired")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.ActionsTakenByMPCC)
             .HasColumnName("ActionsTakenByMPCC")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.RotCause)
             .HasColumnName("RotCause")
             .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.LessonLearntAndShared)
             .HasColumnName("LessonLearntAndShared")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.URL)
             .HasColumnName("URL")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientMgtVisit)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.ClientMgtVisit)
                 .HasForeignKey(p => p.OfficerToAct)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
