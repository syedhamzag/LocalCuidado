using Microsoft.EntityFrameworkCore;
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
            builder.ToTable("tbl_Client_Voice");
            builder.HasKey(k => k.VoiceId);

            #region Properties
            builder.Property(p => p.Reference)
               .HasColumnName("Reference")
               .IsRequired();

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

            builder.Property(p => p.OfficeStaffSupport)
               .HasColumnName("OfficeStaffSupport")
               .IsRequired();

            builder.Property(p => p.AreasOfImprovements)
               .HasColumnName("AreasOfImprovements")
               .IsRequired();

            builder.Property(p => p.SomethingSpecial)
               .HasColumnName("SomethingSpecial")
               .IsRequired();

            builder.Property(p => p.InterestedInPrograms)
               .HasColumnName("InterestedInPrograms")
               .IsRequired();

            builder.Property(p => p.HealthGoalShortTerm)
               .HasColumnName("HealthGoalShortTerm")
               .IsRequired();

            builder.Property(p => p.HealthGoalLongTerm)
               .HasColumnName("HealthGoalLongTerm")
               .IsRequired();


            builder.Property(p => p.ActionRequired)
               .HasColumnName("ActionRequired")
               .IsRequired();

            builder.Property(p => p.EvidenceOfActionTaken)
             .HasColumnName("EvidenceOfActionTaken")
             .IsRequired();

            builder.Property(p => p.ActionsTakenByMPCC)
               .HasColumnName("ActionsTakenByMPCC")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .IsRequired();

            builder.Property(p => p.RotCause)
             .HasColumnName("RotCause")
             .IsRequired();

            builder.Property(p => p.LessonLearntAndShared)
             .HasColumnName("LessonLearntAndShared")
             .IsRequired();

            builder.Property(p => p.URL)
             .HasColumnName("URL")
             .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment");
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientVoice)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<VoiceOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.Voice)
                .HasForeignKey(p => p.VoiceId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
