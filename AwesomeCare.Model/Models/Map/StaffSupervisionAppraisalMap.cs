using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffSupervisionAppraisalMap : IEntityTypeConfiguration<StaffSupervisionAppraisal>
    {
        public void Configure(EntityTypeBuilder<StaffSupervisionAppraisal> builder)
        {
            builder.ToTable("tbl_Staff_SupervisionAppraisal");
            builder.HasKey(k => k.StaffSupervisionAppraisalId);

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

            builder.Property(p => p.StaffRating)
               .HasColumnName("StaffRating")
               .IsRequired();

            builder.Property(p => p.ProfessionalDevelopment)
               .HasColumnName("ProfessionalDevelopment")
               .IsRequired();

            builder.Property(p => p.StaffComplaints)
               .HasColumnName("StaffComplaints")
               .IsRequired();

            builder.Property(p => p.FiveStarRating)
               .HasColumnName("FiveStarRating")
               .IsRequired();

            builder.Property(p => p.StaffDevelopment)
               .HasColumnName("StaffDevelopment")
               .IsRequired();

            builder.Property(p => p.StaffAbility)
               .HasColumnName("StaffAbility")
               .IsRequired();

            builder.Property(p => p.NoAbilityToSupport)
               .HasColumnName("NoAbilityToSupport")
               .IsRequired();

            builder.Property(p => p.CondourAndWhistleBlowing)
               .HasColumnName("CondourAndWhistleBlowing")
               .IsRequired();

            builder.Property(p => p.NoCondourAndWhistleBlowing)
               .HasColumnName("NoCondourAndWhistleBlowing")
               .IsRequired();

            builder.Property(p => p.StaffSupportAreas)
               .HasColumnName("StaffSupportAreas")
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

            builder.HasMany<SupervisionWorkteam>(p => p.Workteam)
                .WithOne(p => p.Supervision)
                .HasForeignKey(p => p.StaffSupervisionAppraisalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<SupervisionOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.Supervision)
                .HasForeignKey(p => p.StaffSupervisionAppraisalId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
