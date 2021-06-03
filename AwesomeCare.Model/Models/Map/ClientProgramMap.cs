using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientProgramMap : IEntityTypeConfiguration<ClientProgram>
    {
        public void Configure(EntityTypeBuilder<ClientProgram> builder)
        {
            builder.ToTable("tbl_ClientProgram");
            builder.HasKey(k => k.ProgramId);

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

            builder.Property(p => p.ProgramOfChoice)
               .HasColumnName("ProgramOfChoice")
               .IsRequired();

            builder.Property(p => p.DaysOfChoice)
               .HasColumnName("DaysOfChoice")
               .IsRequired();

            builder.Property(p => p.PlaceLocationProgram)
             .HasColumnName("PlaceLocationProgram")
             .IsRequired();

            builder.Property(p => p.DetailsOfProgram)
                .HasColumnName("DetailsOfProgram")
                .IsRequired();

            builder.Property(p => p.OfficerToAct)
               .HasColumnName("OfficerToAct")
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

            builder.Property(p => p.Observation)
             .HasColumnName("Observation")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.ActionRequired)
             .HasColumnName("ActionRequired")
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
                 .WithMany(p => p.ClientProgram)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.ClientProgram)
                 .HasForeignKey(p => p.OfficerToAct)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
