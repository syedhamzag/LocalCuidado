using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HomeRiskAssessmentTaskMap : IEntityTypeConfiguration<HomeRiskAssessmentTask>
    {
        public void Configure(EntityTypeBuilder<HomeRiskAssessmentTask> builder)
        {
            builder.ToTable("tbl_HomeRiskAssessmentTask");
            builder.HasKey(k => k.HomeRiskAssessmentTaskId);

            #region Properties
            builder.Property(p => p.HomeRiskAssessmentId)
               .HasColumnName("HomeRiskAssessmentId")
               .IsRequired();

            builder.Property(p => p.Title)
               .HasColumnName("Title")
               .IsRequired();

            builder.Property(p => p.Answer)
               .HasColumnName("Answer")
               .IsRequired();

            builder.Property(p => p.Comment)
               .HasColumnName("Comment")
               .IsRequired();
            #endregion
        }
    }
}
