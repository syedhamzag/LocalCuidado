using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffTeamLeadTasksMap : IEntityTypeConfiguration<StaffTeamLeadTasks>
    {
        public void Configure(EntityTypeBuilder<StaffTeamLeadTasks> builder)
        {
            builder.ToTable("tbl_StaffTeamLeadTasks");
            builder.HasKey(p => p.TeamLeadTaskId);

            #region Properties
            builder.Property(p => p.TeamLeadId)
                .HasColumnName("TeamLeadId")
                .IsRequired();

            builder.Property(p => p.Title)
                .HasColumnName("Title")
                .IsRequired();

            builder.Property(p => p.Status)
                .HasColumnName("Status")
                .IsRequired();

            builder.Property(p => p.Comments)
                .HasColumnName("Comments")
                .IsRequired();
            #endregion
        }
    }
}