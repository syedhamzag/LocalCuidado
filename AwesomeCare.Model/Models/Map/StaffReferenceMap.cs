using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffReferenceMap : IEntityTypeConfiguration<StaffReference>
    {
        public void Configure(EntityTypeBuilder<StaffReference> builder)
        {
            builder.ToTable("tbl_Staff_Reference");
            builder.HasKey(k => k.StaffReferenceId);

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

            builder.Property(p => p.ApplicantRole)
               .HasColumnName("ApplicantRole")
               .IsRequired();

            builder.Property(p => p.DateofEmployement)
               .HasColumnName("DateofEmployement")
               .IsRequired();

            builder.Property(p => p.DateofExit)
               .HasColumnName("DateofExit")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.RehireStaff)
               .HasColumnName("RehireStaff")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Relationship)
               .HasColumnName("Relationship")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.TeamWork)
               .HasColumnName("TeamWork")
               .IsRequired();

            builder.Property(p => p.Integrity)
               .HasColumnName("Integrity")
               .IsRequired();

            builder.Property(p => p.Knowledgeable)
               .HasColumnName("Knowledgeable")
               .IsRequired();

            builder.Property(p => p.Caring)
               .HasColumnName("Caring")
               .IsRequired();

            builder.Property(p => p.WorkUnderPressure)
               .HasColumnName("WorkUnderPressure")
               .IsRequired();

            builder.Property(p => p.PreviousExperience)
               .HasColumnName("PreviousExperience")
               .IsRequired();

            builder.Property(p => p.RefreeName)
               .HasColumnName("RefreeName")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Address)
               .HasColumnName("Address")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Email)
               .HasColumnName("Email")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Contact)
               .HasColumnName("Contact")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Attachment)
               .HasColumnName("Attachment")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.ConfirmedBy)
               .HasColumnName("ConfirmedBy")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .HasMaxLength(255)
               .IsRequired();
            #endregion

            #region Relationship

            builder.HasOne(p => p.Client)
                 .WithMany(p => p.StaffReference)
                 .HasForeignKey(p => p.ApplicantRole)
                 .OnDelete(DeleteBehavior.Cascade);
                  
            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.StaffReference)
                 .HasForeignKey(p => p.ConfirmedBy)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
