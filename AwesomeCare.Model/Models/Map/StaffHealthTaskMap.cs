using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffHealthTaskMap : IEntityTypeConfiguration<StaffHealthTask>
    {
        public void Configure(EntityTypeBuilder<StaffHealthTask> builder)
        {
            builder.ToTable("tbl_StaffHealthTask");
            builder.HasKey(k => k.StaffHealthTaskId);

            #region Properties
            builder.Property(p => p.StaffHealthId)
               .HasColumnName("StaffHealthId")
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

