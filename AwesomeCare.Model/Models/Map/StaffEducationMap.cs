using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffEducationMap : IEntityTypeConfiguration<StaffEducation>
    {
        public void Configure(EntityTypeBuilder<StaffEducation> builder)
        {
            builder.ToTable("tbl_StaffEducation");
            builder.HasKey(k => k.StaffEducationId);

            #region Properties
            builder.Property(p => p.StaffEducationId)
                .HasColumnName("StaffEducationId")
                .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();

            builder.Property(p => p.Institution)
              .HasColumnName("Institution")
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

            builder.Property(p => p.Address)
             .HasColumnName("Address")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.StartDate)
             .HasColumnName("StartDate")
             .HasMaxLength(25)
             .IsRequired();

            builder.Property(p => p.EndDate)
             .HasColumnName("EndDate")
             .HasMaxLength(25)
             .IsRequired(false);

            builder.Property(p => p.CertificateAttachment)
             .HasColumnName("CertificateAttachment")
             .IsRequired(false);
            #endregion

            #region Relationship
            builder.HasOne<StaffPersonalInfo>(p => p.Staff)
                .WithMany(p => p.Education)
                .HasForeignKey(p=>p.StaffPersonalInfoId)
                .OnDelete(DeleteBehavior.Cascade) ;
            #endregion
        }
    }
}
