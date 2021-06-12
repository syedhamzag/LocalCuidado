using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffKeyWorkerVoiceMap : IEntityTypeConfiguration<StaffKeyWorkerVoice>
    {
        public void Configure(EntityTypeBuilder<StaffKeyWorkerVoice> builder)
        {
            builder.ToTable("tbl_StaffKeyWorkerVoice");
            builder.HasKey(k => k.KeyWorkerId);

            #region Properties
            builder.Property(p => p.Reference)
               .HasColumnName("Reference")
               .IsRequired();

            builder.Property(p => p.StaffId)
               .HasColumnName("StaffId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.NextCheckDate)
               .HasColumnName("NextCheckDate")
               .IsRequired();

            builder.Property(p => p.Details)
               .HasColumnName("Details")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.TeamYouWorkFor)
               .HasColumnName("TeamYouWorkFor")
               .IsRequired();

            builder.Property(p => p.NotComfortableServices)
               .HasColumnName("NotComfortableServices")
               .IsRequired();

            builder.Property(p => p.ServicesRequiresTime)
               .HasColumnName("ServicesRequiresTime")
               .IsRequired();

            builder.Property(p => p.ServicesRequiresServices)
               .HasColumnName("ServicesRequiresServices")
               .IsRequired();

            builder.Property(p => p.WellSupportedServices)
               .HasColumnName("WellSupportedServices")
               .IsRequired();

            builder.Property(p => p.ChangesWeNeed)
               .HasColumnName("ChangesWeNeed")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.NutritionalChanges)
               .HasColumnName("NutritionalChanges")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.HealthAndWellNessChanges)
               .HasColumnName("HealthAndWellNessChanges")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.MedicationChanges)
               .HasColumnName("MedicationChanges")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.MovingAndHandling)
               .HasColumnName("MovingAndHandling")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.RiskAssessment)
               .HasColumnName("RiskAssessment")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.ActionRequired)
               .HasColumnName("ActionRequired")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.OfficerToAct)
               .HasColumnName("OfficerToAct")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
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

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.StaffKeyWorkerVoice)
                 .HasForeignKey(p => p.TeamYouWorkFor)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Client)
                 .WithMany(p => p.StaffKeyWorkerVoice)
                 .HasForeignKey(p => p.NotComfortableServices)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Client)
                 .WithMany(p => p.StaffKeyWorkerVoice)
                 .HasForeignKey(p => p.ServicesRequiresTime)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Client)
                 .WithMany(p => p.StaffKeyWorkerVoice)
                 .HasForeignKey(p => p.ServicesRequiresServices)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.StaffKeyWorkerVoice)
                 .HasForeignKey(p => p.OfficerToAct)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
