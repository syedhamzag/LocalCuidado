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
            builder.ToTable("tbl_Client_Program");
            builder.HasKey(k => k.ProgramId);

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


            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .IsRequired();

            builder.Property(p => p.Observation)
             .HasColumnName("Observation")
             .IsRequired();

            builder.Property(p => p.ActionRequired)
             .HasColumnName("ActionRequired")
             .IsRequired();

            builder.Property(p => p.URL)
             .HasColumnName("URL")
             .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment");
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientProgram)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<ProgramOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.Program)
                .HasForeignKey(p => p.ProgramId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
