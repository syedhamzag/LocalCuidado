﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PersonalMap : IEntityTypeConfiguration<Personal>
    {
        public void Configure(EntityTypeBuilder<Personal> builder)
        {
            builder.ToTable("tbl_Personal");
            builder.HasKey(k => k.PersonalId);

            #region Properties

            builder.Property(p => p.PersonalId)
               .HasColumnName("PersonalId")
               .IsRequired();

            builder.Property(p => p.PersonalDetailId)
               .HasColumnName("PersonalDetailId")
               .IsRequired();

            builder.Property(p => p.Smoking)
               .HasColumnName("Smoking")
               .IsRequired();

            builder.Property(p => p.DNR)
               .HasColumnName("DNR")
               .IsRequired();

            builder.Property(p => p.Religion)
               .HasColumnName("Religion")
               .IsRequired();

            builder.Property(p => p.Nationality)
               .HasColumnName("Nationality")
               .IsRequired();
            #endregion
        }
    }
}
