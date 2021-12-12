using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HealthTask2Map : IEntityTypeConfiguration<HealthTask2>
    {
        public void Configure(EntityTypeBuilder<HealthTask2> builder)
        {
            builder.ToTable("tbl_HealthTask2");
            builder.HasKey(k => k.HealthTask2Id);

            #region Properties

            builder.Property(p => p.BestId)
               .HasColumnName("BestId")
               .IsRequired();

            builder.Property(p => p.HeadingId)
                .HasColumnName("HeadingId")
                .IsRequired();

            builder.Property(p => p.Status)
                .HasColumnName("Status")
                .IsRequired();

            builder.Property(p => p.Remarks)
                .HasColumnName("Remarks")
                .IsRequired();

            #endregion
        }
    }
}
