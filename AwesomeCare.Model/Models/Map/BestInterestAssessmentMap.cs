using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BestInterestAssessmentMap : IEntityTypeConfiguration<BestInterestAssessment>
    {
        public void Configure(EntityTypeBuilder<BestInterestAssessment> builder)
        {
            builder.ToTable("tbl_BestInterestAssessment");
            builder.HasKey(k => k.BestId);

            #region Properties

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
                .HasColumnName("Date")
                .IsRequired();

            builder.Property(p => p.Name)
               .HasColumnName("Name")
               .IsRequired();

            builder.Property(p => p.Position)
                .HasColumnName("Position")
                .IsRequired();

            builder.Property(p => p.Signature)
               .HasColumnName("Signature")
               .IsRequired();

            #endregion
            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.BestInterestAssessment)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<CareIssuesTask>(p => p.CareIssuesTask)
                .WithOne(p => p.BestInterestAssessment)
                .HasForeignKey(p => p.BestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<HealthTask>(p => p.HealthTask)
                .WithOne(p => p.BestInterestAssessment)
                .HasForeignKey(p => p.BestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<HealthTask2>(p => p.HealthTask2)
                .WithOne(p => p.BestInterestAssessment)
                .HasForeignKey(p => p.BestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<BelieveTask>(p => p.BelieveTask)
                .WithOne(p => p.BestInterestAssessment)
                .HasForeignKey(p => p.BestId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
