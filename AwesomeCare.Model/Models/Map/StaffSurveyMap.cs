using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffSurveyMap : IEntityTypeConfiguration<StaffSurvey>
    {
        public void Configure(EntityTypeBuilder<StaffSurvey> builder)
        {
            builder.ToTable("tbl_Staff_Survey");
            builder.HasKey(k => k.StaffSurveyId);

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

            builder.Property(p => p.WorkTeam)
               .HasColumnName("WorkTeam")
               .IsRequired();

            builder.Property(p => p.AdequateTrainingReceived)
               .HasColumnName("AdequateTrainingReceived")
               .IsRequired();

            builder.Property(p => p.HealthCareServicesSatisfaction)
               .HasColumnName("HealthCareServicesSatisfaction")
               .IsRequired();

            builder.Property(p => p.SupportFromCompany)
               .HasColumnName("SupportFromCompany")
               .IsRequired();

            builder.Property(p => p.CompanyManagement)
               .HasColumnName("CompanyManagement")
               .IsRequired();

            builder.Property(p => p.AccessToPolicies)
               .HasColumnName("AccessToPolicies")
               .IsRequired();

            builder.Property(p => p.WorkEnvironmentSuggestions)
               .HasColumnName("WorkEnvironmentSuggestions")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.AreaRequiringImprovements)
               .HasColumnName("AreaRequiringImprovements")
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
                 .WithMany(p => p.StaffSurvey)
                 .HasForeignKey(p => p.WorkTeam)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.StaffSurvey)
                 .HasForeignKey(p => p.OfficerToAct)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
