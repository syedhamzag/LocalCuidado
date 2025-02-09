﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ConsentLandLineMap : IEntityTypeConfiguration<ConsentLandLine>
    {
        public void Configure(EntityTypeBuilder<ConsentLandLine> builder)
        {
            builder.ToTable("tbl_ConsentLandLine");
            builder.HasKey(k => k.LandlineId);

            #region Properties

            builder.Property(p => p.LandlineId)
               .HasColumnName("LandlineId")
               .IsRequired();

            builder.Property(p => p.PersonalDetailId)
               .HasColumnName("PersonalDetailId")
               .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(p => p.Signature)
              .HasColumnName("Signature")
              .IsRequired();

            builder.Property(p => p.Date)
             .HasColumnName("Date")
             .IsRequired();

            #endregion

            #region Relationship
            builder.HasMany<ConsentLandlineLog>(p => p.LogMethod)
                .WithOne(p => p.ConsentLandLine)
                .HasForeignKey(p => p.LandlineId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
