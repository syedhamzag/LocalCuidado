using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffRotaDynamicAdditionMap : IEntityTypeConfiguration<StaffRotaDynamicAddition>
    {
        public void Configure(EntityTypeBuilder<StaffRotaDynamicAddition> builder)
        {
            builder.ToTable("tbl_StaffRotaDynamicAddition");
            builder.HasKey(k => k.StaffRotaDynamicAdditionId);

            #region Properties
            builder.Property(p => p.StaffRotaDynamicAdditionId)
                .HasColumnName("StaffRotaDynamicAdditionId")
                .IsRequired();

            builder.Property(p => p.ItemName)
              .HasColumnName("ItemName")
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(p => p.Deleted)
           .HasColumnName("Deleted")
           .IsRequired();
            #endregion
        }
    }
}
