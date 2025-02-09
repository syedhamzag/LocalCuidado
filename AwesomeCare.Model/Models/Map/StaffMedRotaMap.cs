﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffMedRotaMap : IEntityTypeConfiguration<StaffMedRota>
    {
        public void Configure(EntityTypeBuilder<StaffMedRota> builder)
        {
            builder.ToTable("tbl_StaffMedRota");
            builder.HasKey(k => k.StaffRotaId);

            #region Properties
            
            builder.Property(p => p.StaffRotaId)
                .HasColumnName("StaffRotaId")
                .IsRequired();

            builder.Property(p => p.RotaDate)
               .HasColumnName("RotaDate")
               .HasColumnType("date")
               .IsRequired();

            builder.Property(p => p.Staff)
               .HasColumnName("Staff")
               .IsRequired();

            builder.Property(p => p.RotaId)
               .HasColumnName("RotaId")
               .IsRequired();

            builder.Property(p => p.RotaDayofWeekId)
             .HasColumnName("RotaDayofWeekId")
             .IsRequired(false);

            builder.Property(p => p.ReferenceNumber)
             .HasColumnName("ReferenceNumber")
              .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.DoseGiven)
             .HasColumnName("DoseGiven")
              .HasMaxLength(50);

            builder.Property(p => p.Time)
             .HasColumnName("Time")
              .HasMaxLength(50);

            builder.Property(p => p.Measurement)
             .HasColumnName("Measurement")
              .HasMaxLength(50);

            builder.Property(p => p.Location)
             .HasColumnName("Location")
              .HasMaxLength(250);

            builder.Property(p => p.Feedback)
             .HasColumnName("Feedback")
              .HasMaxLength(250);


            builder.Property(p => p.Remark)
               .HasColumnName("Remark")
               .HasMaxLength(225)
               .IsRequired(false);
            #endregion

           
        }
    }
}
