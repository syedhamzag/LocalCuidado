﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffAdlObsMap : IEntityTypeConfiguration<StaffAdlObs>
    {
        public void Configure(EntityTypeBuilder<StaffAdlObs> builder)
        {
            builder.ToTable("tbl_StaffAdlObs");
            builder.HasKey(k => k.ObservationID);

            #region Properties
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

            builder.Property(p => p.UnderstandingofEquipment)
               .HasColumnName("UnderstandingofEquipment")
               .IsRequired();

            builder.Property(p => p.UnderstandingofService)
               .HasColumnName("UnderstandingofService")
               .IsRequired();

            builder.Property(p => p.UnderstandingofControl)
               .HasColumnName("UnderstandingofControl")
               .IsRequired();

            builder.Property(p => p.FivePrinciples)
               .HasColumnName("FivePrinciples")
               .IsRequired();

            builder.Property(p => p.Comments)
               .HasColumnName("Comments")
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
                 .WithMany(p => p.StaffAdlObs)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.StaffAdlObs)
                 .HasForeignKey(p => p.OfficerToAct)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
