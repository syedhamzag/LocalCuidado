using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffShadowingTaskMap : IEntityTypeConfiguration<StaffShadowingTask>
    {
        public void Configure(EntityTypeBuilder<StaffShadowingTask> builder)
        {
            builder.ToTable("tbl_StaffShadowingTask");
            builder.HasKey(k => k.StaffShadowingTaskId);

            #region Properties
            builder.Property(p => p.StaffShadowingId)
               .HasColumnName("StaffShadowingId")
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
