using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientInvolvingPartyItemMap : IEntityTypeConfiguration<ClientInvolvingPartyItem>
    {
        public void Configure(EntityTypeBuilder<ClientInvolvingPartyItem> builder)
        {
            builder.ToTable("tbl_ClientInvolvingPartyItem");
            builder.HasKey(p => p.ClientInvolvingPartyItemId);

            #region Properties
            builder.HasIndex(c => c.ItemName).IsUnique(true).HasName("IX_tbl_ClientInvolvingPartyItem_ItemName");

            builder.Property(p => p.ClientInvolvingPartyItemId)
               .HasColumnName("ClientInvolvingPartyItemId")
               .IsRequired();
            builder.Property(p => p.ItemName)
               .HasColumnName("ItemName")
               .HasMaxLength(100)
               .IsRequired();
            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .HasMaxLength(225)
                .IsRequired();
            builder.Property(p => p.Deleted)
                .HasColumnName("Deleted");
            #endregion
        }
    }
}
