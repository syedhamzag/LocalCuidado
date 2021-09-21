using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class TaskBoardMap : IEntityTypeConfiguration<TaskBoard>
    {
        public void Configure(EntityTypeBuilder<TaskBoard> builder)
        {
            builder.ToTable("tbl_TaskBoard");
            builder.HasKey(k => k.TaskId);

            #region Properties
            builder.Property(p => p.AssignedBy)
               .HasColumnName("AssignedBy")
               .IsRequired();

            builder.Property(p => p.Attachment)
               .HasColumnName("Attachment")
               .IsRequired();

            builder.Property(p => p.CompletionDate)
               .HasColumnName("CompletionDate")
               .IsRequired();

            builder.Property(p => p.Note)
               .HasColumnName("Note")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            builder.Property(p => p.TaskImage)
               .HasColumnName("TaskImage")
               .IsRequired();

            builder.Property(p => p.TaskName)
               .HasColumnName("TaskName")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasMany<TaskBoardAssignedTo>(p => p.AssignedTo)
                .WithOne(p => p.TaskBoard)
                .HasForeignKey(p => p.TaskBoardId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
