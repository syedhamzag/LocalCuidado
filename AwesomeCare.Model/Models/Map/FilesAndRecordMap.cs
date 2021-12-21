using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class FilesAndRecordMap : IEntityTypeConfiguration<FilesAndRecord>
    {
        public void Configure(EntityTypeBuilder<FilesAndRecord> builder)
        {
            builder.ToTable("tbl_FilesAndRecord");
            builder.HasKey(k => k.FilesAndRecordId);

            #region Properties

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Subject)
              .HasColumnName("Subject")
              .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
             .HasColumnName("StaffPersonalInfoId")
             .IsRequired();

            builder.Property(p => p.Remarks)
             .HasColumnName("Remarks")
             .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.FilesAndRecord)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Relationship
            builder.HasOne(p => p.StaffPersonalInfo)
                 .WithMany(p => p.FilesAndRecord)
                 .HasForeignKey(p => p.StaffPersonalInfoId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
