using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HealthTaskMap : IEntityTypeConfiguration<HealthTask>
    {
        public void Configure(EntityTypeBuilder<HealthTask> builder)
        {
            builder.ToTable("tbl_HealthTask");
            builder.HasKey(k => k.HealthTaskId);

            #region Properties

            builder.Property(p => p.BestId)
               .HasColumnName("BestId")
               .IsRequired();

            builder.Property(p => p.HeadingId)
                .HasColumnName("HeadingId")
                .IsRequired();

            builder.Property(p => p.Title)
                .HasColumnName("Title")
                .IsRequired();

            builder.Property(p => p.Answer)
                .HasColumnName("Answer")
                .IsRequired();

            builder.Property(p => p.Remarks)
                .HasColumnName("Remarks")
                .IsRequired();

            #endregion
        }
    }
}
