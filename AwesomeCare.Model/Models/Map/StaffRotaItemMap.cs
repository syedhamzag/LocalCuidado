using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffRotaItemMap : IEntityTypeConfiguration<StaffRotaItem>
    {
        public void Configure(EntityTypeBuilder<StaffRotaItem> builder)
        {
            builder.ToTable("tbl_StaffRotaItem");
            builder.HasKey(k => k.StaffRotaItemId);

            #region Properties
            builder.Property(p => p.StaffRotaItemId)
                .HasColumnName("StaffRotaItemId")
                .IsRequired();

            builder.Property(p => p.StaffRotaId)
               .HasColumnName("StaffRotaId")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(r => r.StaffRota)
                .WithMany(m => m.StaffRotaItem)
                .HasForeignKey(k => k.StaffRotaId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
