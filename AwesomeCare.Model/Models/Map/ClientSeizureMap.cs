using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientSeizureMap : IEntityTypeConfiguration<ClientSeizure>
    {
        public void Configure(EntityTypeBuilder<ClientSeizure> builder)
        {
            builder.ToTable("tbl_Client_Seizure");
            builder.HasKey(k => k.SeizureId);

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

            builder.Property(p => p.SeizureType)
               .HasColumnName("SeizureType")
               .IsRequired();

            builder.Property(p => p.SeizureTypeAttach)
               .HasColumnName("SeizureTypeAttach");

            builder.Property(p => p.SeizureLength)
               .HasColumnName("SeizureLength")
               .IsRequired();

            builder.Property(p => p.SeizureLengthAttach)
               .HasColumnName("SeizureLengthAttach");

            builder.Property(p => p.Often)
               .HasColumnName("Often")
               .IsRequired();

            builder.Property(p => p.WhatHappened)
               .HasColumnName("WhatHappened")
               .IsRequired();

            builder.Property(p => p.StatusImage)
               .HasColumnName("StatusImage")
               .IsRequired();

            builder.Property(p => p.StatusAttach)
               .HasColumnName("StatusAttach");

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
                 .WithMany(p => p.ClientSeizure)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<SeizurePhysician>(p => p.Physician)
                .WithOne(p => p.Seizure)
                .HasForeignKey(p => p.SeizureId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<SeizureStaffName>(p => p.StaffName)
                .WithOne(p => p.Seizure)
                .HasForeignKey(p => p.SeizureId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<SeizureOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.Seizure)
                .HasForeignKey(p => p.SeizureId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
