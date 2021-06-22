using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientPainChartMap : IEntityTypeConfiguration<ClientPainChart>
    {
        public void Configure(EntityTypeBuilder<ClientPainChart> builder)
        {
            builder.ToTable("tbl_ClientPainChart");
            builder.HasKey(k => k.PainChartId);

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

            builder.Property(p => p.Type)
               .HasColumnName("Type")
               .IsRequired();

            builder.Property(p => p.TypeAttach)
               .HasColumnName("TypeAttach");

            builder.Property(p => p.Location)
               .HasColumnName("Location")
               .IsRequired();

            builder.Property(p => p.LocationAttach)
               .HasColumnName("LocationAttach");

            builder.Property(p => p.PainLvl)
               .HasColumnName("PainLvl")
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
                 .WithMany(p => p.ClientPainChart)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<PainChartPhysician>(p => p.Physician)
                .WithOne(p => p.PainChart)
                .HasForeignKey(p => p.PainChartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<PainChartStaffName>(p => p.StaffName)
                .WithOne(p => p.PainChart)
                .HasForeignKey(p => p.PainChartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<PainChartOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.PainChart)
                .HasForeignKey(p => p.PainChartId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
