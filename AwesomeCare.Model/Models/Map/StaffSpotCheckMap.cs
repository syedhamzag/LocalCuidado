using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffSpotCheckMap : IEntityTypeConfiguration<StaffSpotCheck>
    {
        public void Configure(EntityTypeBuilder<StaffSpotCheck> builder)
        {
            builder.ToTable("tbl_Staff_SpotCheck");
            builder.HasKey(k => k.SpotCheckId);

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

            builder.Property(p => p.Details)
               .HasColumnName("Details")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.StaffArriveOnTime)
               .HasColumnName("StaffArriveOnTime")
               .IsRequired();

            builder.Property(p => p.StaffDressCode)
               .HasColumnName("StaffDressCode")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.AreaComments)
               .HasColumnName("AreaComments")
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
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.StaffSpotCheck)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.StaffSpotCheck)
                 .HasForeignKey(p => p.OfficerToAct)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
