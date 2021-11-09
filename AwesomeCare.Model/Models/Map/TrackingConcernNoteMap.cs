using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class TrackingConcernNoteMap : IEntityTypeConfiguration<TrackingConcernNote>
    {
        public void Configure(EntityTypeBuilder<TrackingConcernNote> builder)
        {
            builder.ToTable("tbl_TrackingConcernNote");
            builder.HasKey(k => k.Ref);

            #region Properties
            builder.Property(p => p.Date)
             .HasColumnName("Date")
             .IsRequired();

            builder.Property(p => p.ConcernNote)
             .HasColumnName("ConcernNote")
             .IsRequired();

            builder.Property(p => p.ActionRequired)
             .HasColumnName("ActionRequired")
             .IsRequired();

            builder.Property(p => p.DateOfIncident)
             .HasColumnName("DateOfIncident")
             .IsRequired();

            builder.Property(p => p.DateOfIncident)
             .HasColumnName("DateOfIncident")
             .IsRequired();

            builder.Property(p => p.ExpectedDeadline)
             .HasColumnName("ExpectedDeadline")
             .IsRequired();

            builder.Property(p => p.StaffNotify)
             .HasColumnName("StaffNotify")
             .IsRequired();

            builder.Property(p => p.ManagerCopied)
              .HasColumnName("ManagerCopied")
              .IsRequired();

            builder.Property(p => p.Status)
             .HasColumnName("Status")
             .IsRequired();

            builder.Property(p => p.Remarks)
             .HasColumnName("Remarks")
             .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired();

            #endregion

            #region Relationship

            builder.HasMany<TrackingConcernManager>(p => p.ManagerInvolved)
                .WithOne(p => p.TrackingConcernNote)
                .HasForeignKey(p => p.TrackingConcernNoteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<TrackingConcernStaff>(p => p.StaffInvolved)
                .WithOne(p => p.TrackingConcernNote)
                .HasForeignKey(p => p.TrackingConcernNoteId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
