using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class RotaTaskMap : IEntityTypeConfiguration<RotaTask>
    {
        public void Configure(EntityTypeBuilder<RotaTask> builder)
        {
            builder.ToTable("tbl_RotaTask");
            builder.HasKey(p => p.RotaTaskId);

            #region Properties
            builder.Property(p => p.RotaTaskId)
                .HasColumnName("RotaTaskId")
                .IsRequired();

            builder.Property(p => p.TaskName)
                .HasColumnName("TaskName")
                .HasMaxLength(125)
                .IsRequired();

            builder.Property(p => p.GivenAcronym)
              .HasColumnName("GivenAcronym")
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(p => p.NotGivenAcronym)
             .HasColumnName("NotGivenAcronym")
             .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.Remark)
             .HasColumnName("Remark")
             .HasMaxLength(125)
             .IsRequired(false);

            builder.Property(p => p.Deleted)
             .HasColumnName("Deleted");

            builder.HasIndex(i => i.TaskName)
                .IsUnique(true)
                .HasName("IX_tbl_RotaTask_TaskName");

            builder.HasIndex(i => i.GivenAcronym)
               .IsUnique(true)
               .HasName("IX_tbl_RotaTask_GivenAcronym");

            builder.HasIndex(i => i.NotGivenAcronym)
               .IsUnique(true)
               .HasName("IX_tbl_RotaTask_NotGivenAcronym");
            #endregion
        }
    }
}
