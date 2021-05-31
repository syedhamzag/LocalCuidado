using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientServiceDetailItemMap : IEntityTypeConfiguration<ClientServiceDetailItem>
    {
        public void Configure(EntityTypeBuilder<ClientServiceDetailItem> builder)
        {
            builder.ToTable("tbl_ClientServiceDetailItem");
            builder.HasKey(k => k.ClientServiceDetailItemId);

            #region Properties
            builder.Property(p => p.ClientServiceDetailItemId)
                .HasColumnName("ClientServiceDetailItemId")
                .IsRequired();

            builder.Property(p => p.ClientServiceDetailId)
                .HasColumnName("ClientServiceDetailId")
                .IsRequired();

            builder.Property(p => p.ItemName)
               .HasColumnName("ItemName")
               .HasMaxLength(225)
               .IsRequired();

            builder.Property(p => p.Quantity)
               .HasColumnName("Quantity")
               .IsRequired();

            builder.Property(p => p.Rate)
               .HasColumnName("Rate")
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder.Property(p => p.Amount)
              .HasColumnName("Amount")
              .HasColumnType("decimal(18,2)")
              .IsRequired();
            #endregion
        }
    }
}
