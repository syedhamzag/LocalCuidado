using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffTrainingMap : IEntityTypeConfiguration<StaffTraining>
    {
        public void Configure(EntityTypeBuilder<StaffTraining> builder)
        {
            builder.ToTable("tbl_StaffTraining");
            builder.HasKey(k => k.StaffTrainingId);

            #region Properties
            builder.Property(p => p.StaffTrainingId)
                .HasColumnName("StaffTrainingId")
                .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();

            builder.Property(p => p.Training)
              .HasColumnName("Training")
              .HasMaxLength(255)
              .IsRequired();

            builder.Property(p => p.Certificate)
             .HasColumnName("Certificate")
             .HasMaxLength(125)
             .IsRequired();

            builder.Property(p => p.Location)
             .HasColumnName("Location")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.Trainer)
             .HasColumnName("Trainer")
             .HasMaxLength(125)
             .IsRequired();

            builder.Property(p => p.StartDate)
             .HasColumnName("StartDate")
             .HasMaxLength(25)
             .IsRequired();

            builder.Property(p => p.ExpiredDate)
             .HasColumnName("ExpiredDate")
             .HasMaxLength(25)
             .IsRequired(false);

            builder.Property(p => p.CertificateAttachment)
             .HasColumnName("CertificateAttachment")
             .IsRequired(false);
            #endregion

            #region Relationship
            builder.HasOne<StaffPersonalInfo>(p => p.Staff)
                .WithMany(p => p.Trainings)
                .HasForeignKey(p => p.StaffPersonalInfoId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
