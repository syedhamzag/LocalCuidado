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
               .IsRequired();

            builder.Property(p => p.AreaRequiringImprovements)
               .HasColumnName("AreaRequiringImprovements")
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

            builder.HasMany<SurveyWorkteam>(p => p.Workteam)
                .WithOne(p => p.Survey)
                .HasForeignKey(p => p.StaffSurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<SurveyOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.Survey)
                .HasForeignKey(p => p.StaffSurveyId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
