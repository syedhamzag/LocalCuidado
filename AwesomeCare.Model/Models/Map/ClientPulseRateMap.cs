﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientPulseRateMap : IEntityTypeConfiguration<ClientPulseRate>
    {
        public void Configure(EntityTypeBuilder<ClientPulseRate> builder)
        {
            builder.ToTable("tbl_Client_PulseRate");
            builder.HasKey(k => k.PulseRateId);

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

            builder.Property(p => p.TargetPulse)
               .HasColumnName("TargetPulse")
               .IsRequired();

            builder.Property(p => p.TargetPulseAttach)
               .HasColumnName("TargetPulseAttach");

            builder.Property(p => p.CurrentPulse)
               .HasColumnName("CurrentPulse")
               .IsRequired();

            builder.Property(p => p.Chart)
               .HasColumnName("Chart")
               .IsRequired();

            builder.Property(p => p.SeeChart)
               .HasColumnName("SeeChart")
               .IsRequired();

            builder.Property(p => p.SeeChartAttach)
               .HasColumnName("SeeChartAttach");

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
                 .WithMany(p => p.ClientPulseRate)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<PulseRatePhysician>(p => p.Physician)
                .WithOne(p => p.PulseRate)
                .HasForeignKey(p => p.PulseRateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<PulseRateStaffName>(p => p.StaffName)
                .WithOne(p => p.PulseRate)
                .HasForeignKey(p => p.PulseRateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<PulseRateOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.PulseRate)
                .HasForeignKey(p => p.PulseRateId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
