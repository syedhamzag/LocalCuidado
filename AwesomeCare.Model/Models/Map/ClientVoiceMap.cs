﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientVoiceMap : IEntityTypeConfiguration<ClientVoice>
    {
        public void Configure(EntityTypeBuilder<ClientVoice> builder)
        {
            builder.ToTable("tbl_ClientVoice");
            builder.HasKey(k => k.VoiceId);

            #region Properties
            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.NextCheckDate)
               .HasColumnName("NextCheckDate")
               .IsRequired();

            builder.Property(p => p.RateServiceRecieving)
               .HasColumnName("RateServiceRecieving")
               .IsRequired();

            builder.Property(p => p.RateStaffAttending)
               .HasColumnName("RateStaffAttending")
               .IsRequired();

            builder.Property(p => p.StaffBestSupport)
               .HasColumnName("StaffBestSupport")
               .IsRequired();

            builder.Property(p => p.StaffPoorSupport)
               .HasColumnName("StaffPoorSupport")
               .IsRequired();

            builder.Property(p => p.OfficeStaffSupport)
               .HasColumnName("OfficeStaffSupport")
               .IsRequired();

            builder.Property(p => p.AreasOfImprovements)
               .HasColumnName("AreasOfImprovements")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.SomethingSpecial)
               .HasColumnName("SomethingSpecial")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.InterestedInPrograms)
               .HasColumnName("InterestedInPrograms")
               .IsRequired();

            builder.Property(p => p.HealthGoalShortTerm)
               .HasColumnName("HealthGoalShortTerm")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.HealthGoalLongTerm)
               .HasColumnName("HealthGoalLongTerm")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.NameOfCaller)
               .HasColumnName("NameOfCaller")
               .IsRequired();

            builder.Property(p => p.ActionRequired)
               .HasColumnName("ActionRequired")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.EvidenceOfActionTaken)
             .HasColumnName("EvidenceOfActionTaken")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.ActionsTakenByMPCC)
               .HasColumnName("ActionsTakenByMPCC")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.OfficerToAct)
             .HasColumnName("OfficerToAct")
             .IsRequired();

            builder.Property(p => p.RotCause)
             .HasColumnName("RotCause")
             .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.LessonLearntAndShared)
             .HasColumnName("LessonLearntAndShared")
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
                 .WithMany(p => p.ClientVoice)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.ClientVoice)
                 .HasForeignKey(p => p.OfficerToAct)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
