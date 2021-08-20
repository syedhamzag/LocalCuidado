using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffMedCompObsMap : IEntityTypeConfiguration<StaffMedComp>
    {
        public void Configure(EntityTypeBuilder<StaffMedComp> builder)
        {
            builder.ToTable("tbl_Staff_MedCompObs");
            builder.HasKey(k => k.MedCompId);

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
            .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.UnderstandingofMedication)
               .HasColumnName("UnderstandingofMedication")
               .IsRequired();

            builder.Property(p => p.UnderstandingofRights)
               .HasColumnName("UnderstandingofRights")
               .IsRequired();

            builder.Property(p => p.ReadingMedicalPrescriptions)
               .HasColumnName("ReadingMedicalPrescriptions")
               .IsRequired();

            builder.Property(p => p.CarePlan)
               .HasColumnName("CarePlan")
               .IsRequired();

            builder.Property(p => p.RateStaff)
               .HasColumnName("RateStaff")
               .IsRequired();

            builder.Property(p => p.ActionRequired)
             .HasColumnName("ActionRequired")
             .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .IsRequired();

            builder.Property(p => p.URL)
             .HasColumnName("URL")
             .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.StaffMedCompObs)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.StaffMedCompObs)
                 .HasForeignKey(p => p.StaffId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<MedCompOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.MedComp)
                .HasForeignKey(p => p.MedCompId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
