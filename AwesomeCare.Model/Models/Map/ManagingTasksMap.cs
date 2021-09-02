using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ManagingTasksMap : IEntityTypeConfiguration<ManagingTasks>
    {
        public void Configure(EntityTypeBuilder<ManagingTasks> builder)
        {
            builder.ToTable("tbl_ManagingTasks");
            builder.HasKey(k => k.TaskId);

            #region Properties
            builder.Property(p => p.TaskId)
               .HasColumnName("ManagingTasksId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Task)
               .HasColumnName("Task")
               .IsRequired();

            builder.Property(p => p.Help)
               .HasColumnName("Help")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();
            #endregion

        }
    }
}
