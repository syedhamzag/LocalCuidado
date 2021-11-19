using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{

    public class StaffInterviewTaskMap : IEntityTypeConfiguration<StaffInterviewTask>
    {
        public void Configure(EntityTypeBuilder<StaffInterviewTask> builder)
        {
            builder.ToTable("tbl_StaffInterviewTask");
            builder.HasKey(k => k.StaffInterviewTaskId);

            #region Properties
            builder.Property(p => p.StaffInterviewId)
               .HasColumnName("StaffInterviewId")
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
