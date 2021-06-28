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
            builder.ToTable("tbl_Staff_KeyWorkerVoice");
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
               .IsRequired();

            builder.Property(p => p.NutritionalChanges)
               .HasColumnName("NutritionalChanges")
               .IsRequired();

            builder.Property(p => p.HealthAndWellNessChanges)
               .HasColumnName("HealthAndWellNessChanges")
               .IsRequired();

            builder.Property(p => p.MedicationChanges)
               .HasColumnName("MedicationChanges")
               .IsRequired();

            builder.Property(p => p.MovingAndHandling)
               .HasColumnName("MovingAndHandling")
               .IsRequired();

            builder.Property(p => p.RiskAssessment)
               .HasColumnName("RiskAssessment")
               .IsRequired();

            builder.Property(p => p.ActionRequired)
               .HasColumnName("ActionRequired")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .IsRequired();

            builder.Property(p => p.URL)
               .HasColumnName("URL")
               .IsRequired();

            builder.Property(p => p.Attachment)
               .HasColumnName("Attachment")
               .IsRequired();
            #endregion

            #region Relationship

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

            builder.HasMany<KeyWorkerWorkteam>(p => p.Workteam)
                .WithOne(p => p.KeyWorker)
                .HasForeignKey(p => p.KeyWorkerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<KeyWorkerOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.KeyWorker)
                .HasForeignKey(p => p.KeyWorkerId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
