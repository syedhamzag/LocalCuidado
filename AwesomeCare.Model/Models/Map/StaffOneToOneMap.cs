using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffOneToOneMap : IEntityTypeConfiguration<StaffOneToOne>
    {
        public void Configure(EntityTypeBuilder<StaffOneToOne> builder)
        {
            builder.ToTable("tbl_StaffOneToOne");
            builder.HasKey(k => k.OneToOneId);

            #region Properties
            builder.Property(p => p.Reference)
               .HasColumnName("Reference")
               .IsRequired();

            builder.Property(p => p.StaffId)
               .HasColumnName("StaffId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.NextCheckDate)
               .HasColumnName("NextCheckDate")
               .IsRequired();

            builder.Property(p => p.Purpose)
               .HasColumnName("Purpose")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.PreviousSupervision)
               .HasColumnName("PreviousSupervision")
               .IsRequired();

            builder.Property(p => p.StaffImprovedInAreas)
               .HasColumnName("StaffImprovedInAreas")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.CurrentEventArea)
               .HasColumnName("CurrentEventArea")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.StaffConclusion)
               .HasColumnName("StaffConclusion")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.DecisionsReached)
               .HasColumnName("DecisionsReached")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.ImprovementRecorded)
               .HasColumnName("ImprovementRecorded")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.ActionRequired)
               .HasColumnName("ActionRequired")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.OfficerToAct)
               .HasColumnName("OfficerToAct")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.URL)
               .HasColumnName("URL")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Attachment)
               .HasColumnName("Attachment")
               .IsRequired();
            #endregion

            #region Relationship

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.StaffOneToOne)
                 .HasForeignKey(p => p.OfficerToAct)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
