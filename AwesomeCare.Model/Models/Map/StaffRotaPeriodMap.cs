﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffRotaPeriodMap : IEntityTypeConfiguration<StaffRotaPeriod>
    {
        public void Configure(EntityTypeBuilder<StaffRotaPeriod> builder)
        {
            builder.ToTable("tbl_StaffRotaPeriod");
            builder.HasKey(k => k.StaffRotaPeriodId);

            #region Properties
            builder.Property(p => p.StaffRotaPeriodId)
                .HasColumnName("StaffRotaPeriodId")
                .IsRequired();

            builder.Property(p => p.StaffRotaId)
                .HasColumnName("StaffRotaId")
                .IsRequired();

            builder.Property(p => p.ClientRotaTypeId)
              .HasColumnName("ClientRotaTypeId")
              .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(b => b.StaffRota)
                .WithMany(b => b.StaffRotaPeriods)
                .HasForeignKey(k => k.StaffRotaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.ClientRotaType)
                .WithMany(c => c.StaffRotaPeriods)
                .HasForeignKey(f => f.ClientRotaTypeId);
            #endregion
        }
    }
}
