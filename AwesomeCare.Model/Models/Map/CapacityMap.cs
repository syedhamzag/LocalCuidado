using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class CapacityMap : IEntityTypeConfiguration<Capacity>
    {
        public void Configure(EntityTypeBuilder<Capacity> builder)
        {
            builder.ToTable("tbl_Capacity");
            builder.HasKey(k => k.CapacityId);

            #region Properties

            builder.Property(p => p.CapacityId)
               .HasColumnName("CapacityId")
               .IsRequired();

            builder.Property(p => p.PersonalDetailId)
               .HasColumnName("PersonalDetailId")
               .IsRequired();

            builder.Property(p => p.Pointer)
              .HasColumnName("Pointer")
              .IsRequired();

            builder.Property(p => p.Implications)
             .HasColumnName("Implications")
             .IsRequired();

            #endregion

            #region Relationship
            builder.HasMany<CapacityIndicator>(p => p.Indicator)
                .WithOne(p => p.Capacity)
                .HasForeignKey(p => p.CapacityId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
