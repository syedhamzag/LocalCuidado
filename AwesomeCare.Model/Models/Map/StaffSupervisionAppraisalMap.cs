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
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.WorkTeam)
               .HasColumnName("WorkTeam")
               .IsRequired();

            builder.Property(p => p.StaffRating)
               .HasColumnName("StaffRating")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.ProfessionalDevelopment)
               .HasColumnName("ProfessionalDevelopment")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.StaffComplaints)
               .HasColumnName("StaffComplaints")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.FiveStarRating)
               .HasColumnName("FiveStarRating")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.StaffDevelopment)
               .HasColumnName("StaffDevelopment")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.StaffAbility)
               .HasColumnName("StaffAbility")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.NoAbilityToSupport)
               .HasColumnName("NoAbilityToSupport")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.CondourAndWhistleBlowing)
               .HasColumnName("CondourAndWhistleBlowing")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.NoCondourAndWhistleBlowing)
               .HasColumnName("NoCondourAndWhistleBlowing")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.StaffSupportAreas)
               .HasColumnName("StaffSupportAreas")
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
                .WithMany(p => p.StaffSupervisionAppraisal)
                .HasForeignKey(p => p.WorkTeam)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.StaffSupervisionAppraisal)
                 .HasForeignKey(p => p.OfficerToAct)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
