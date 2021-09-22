using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HomeRiskAssessmentMap : IEntityTypeConfiguration<HomeRiskAssessment>
    {
        public void Configure(EntityTypeBuilder<HomeRiskAssessment> builder)
        {
            builder.ToTable("tbl_HomeRiskAssessment");
            builder.HasKey(k => k.HomeRiskAssessmentId);

            #region Properties
            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Heading)
               .HasColumnName("Heading")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.HomeRiskAssessment)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<HomeRiskAssessmentTask>(p => p.HomeRiskAssessmentTask)
                .WithOne(p => p.HomeRiskAssessment)
                .HasForeignKey(p => p.HomeRiskAssessmentId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
