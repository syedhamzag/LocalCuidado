using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HistoryOfFallMap : IEntityTypeConfiguration<HistoryOfFall>
    {
        public void Configure(EntityTypeBuilder<HistoryOfFall> builder)
        {
            builder.ToTable("tbl_HistoryOfFall");
            builder.HasKey(k => k.HistoryId);

            builder.Property(p => p.HistoryId)
               .HasColumnName("HistoryId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.Details)
               .HasColumnName("Details")
               .IsRequired();

            builder.Property(p => p.Cause)
               .HasColumnName("Cause")
               .IsRequired();

            builder.Property(p => p.Prevention)
               .HasColumnName("Prevention")
               .IsRequired();
        }
    }
}
