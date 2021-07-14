using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class CapacityIndicatorMap : IEntityTypeConfiguration<CapacityIndicator>
    {
        public void Configure(EntityTypeBuilder<CapacityIndicator> builder)
        {
            builder.ToTable("tbl_CapacityIndicator");
            builder.HasKey(k => k.CapacityIndicatorId);

            #region Properties

            builder.Property(p => p.CapacityIndicatorId)
               .HasColumnName("CapacityIndicatorId")
               .IsRequired();

            builder.Property(p => p.CapacityId)
               .HasColumnName("CapacityId")
               .IsRequired();

            #endregion
        }
    }
}
