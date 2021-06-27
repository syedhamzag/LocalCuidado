using Microsoft.EntityFrameworkCore;
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
            builder.ToTable("tbl_Staff_AdlObs");
            builder.HasKey(k => k.ObservationID);

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
                 .WithMany(p => p.StaffAdlObs)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<AdlObsOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.AdlObs)
                .HasForeignKey(p => p.ObservationId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
