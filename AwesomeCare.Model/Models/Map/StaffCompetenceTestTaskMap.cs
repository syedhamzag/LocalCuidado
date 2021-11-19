using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffCompetenceTestTaskMap : IEntityTypeConfiguration<StaffCompetenceTestTask>
    {
        public void Configure(EntityTypeBuilder<StaffCompetenceTestTask> builder)
        {
            builder.ToTable("tbl_StaffCompetenceTestTask");
            builder.HasKey(k => k.StaffCompetenceTestTaskId);

            #region Properties
            builder.Property(p => p.StaffCompetenceTestId)
               .HasColumnName("StaffCompetenceTestId")
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

            builder.Property(p => p.Point)
               .HasColumnName("Point")
               .IsRequired();

            builder.Property(p => p.Score)
               .HasColumnName("Score")
               .IsRequired();
            #endregion
        }
    }
}
