using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class TaskBoardAssignedToMap : IEntityTypeConfiguration<TaskBoardAssignedTo>
    {
            public void Configure(EntityTypeBuilder<TaskBoardAssignedTo> builder)
            {
                builder.ToTable("tbl_TaskBoardAssignedTo");
                builder.HasKey(k => k.TaskBoardAssignedToId);

                #region Properties
                builder.Property(p => p.TaskBoardAssignedToId)
                   .HasColumnName("TaskBoardAssignedToId")
                   .IsRequired();

                builder.Property(p => p.TaskBoardId)
                   .HasColumnName("TaskBoardId")
                   .IsRequired();

                builder.Property(p => p.StaffPersonalInfoId)
                   .HasColumnName("StaffPersonalInfoId")
                   .IsRequired();
                #endregion
            }
    }
}
