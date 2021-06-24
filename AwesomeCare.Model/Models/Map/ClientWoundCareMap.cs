using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientWoundCareMap : IEntityTypeConfiguration<ClientWoundCare>
    {
        public void Configure(EntityTypeBuilder<ClientWoundCare> builder)
        {
            builder.ToTable("tbl_Client_WoundCare");
            builder.HasKey(k => k.WoundCareId);

            #region Properties
            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.Time)
               .HasColumnName("Time")
               .IsRequired();

            builder.Property(p => p.Goal)
               .HasColumnName("Goal")
               .IsRequired();

            builder.Property(p => p.Type)
               .HasColumnName("Type")
               .IsRequired();

            builder.Property(p => p.TypeAttach)
               .HasColumnName("TypeAttach");

            builder.Property(p => p.UlcerStage)
               .HasColumnName("UlcerStage")
               .IsRequired();

            builder.Property(p => p.UlcerStageAttach)
               .HasColumnName("UlcerStageAttach");

            builder.Property(p => p.Measurment)
               .HasColumnName("Measurment")
               .IsRequired();

            builder.Property(p => p.MeasurementAttach)
               .HasColumnName("MeasurementAttach");

            builder.Property(p => p.PainLvl)
               .HasColumnName("PainLvl")
               .IsRequired();

            builder.Property(p => p.Location)
               .HasColumnName("Location")
               .IsRequired();

            builder.Property(p => p.LocationAttach)
               .HasColumnName("LocationAttach");

            builder.Property(p => p.WoundCause)
               .HasColumnName("WoundCause")
               .IsRequired();

            builder.Property(p => p.StatusImage)
               .HasColumnName("StatusImage")
               .IsRequired();

            builder.Property(p => p.StatusAttach)
               .HasColumnName("StatusAttach");

            builder.Property(p => p.Comment)
               .HasColumnName("Comment")
               .IsRequired();

            builder.Property(p => p.PhysicianResponse)
               .HasColumnName("PhysicianResponse")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientWoundCare)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
