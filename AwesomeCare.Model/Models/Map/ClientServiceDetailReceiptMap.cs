using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientServiceDetailReceiptMap : IEntityTypeConfiguration<ClientServiceDetailReceipt>
    {
        public void Configure(EntityTypeBuilder<ClientServiceDetailReceipt> builder)
        {
            builder.ToTable("tbl_ClientServiceDetailReceipt");
            builder.HasKey(k => k.ClientServiceDetailReceiptId);

            #region Properties
            builder.Property(p => p.ClientServiceDetailReceiptId)
                .HasColumnName("ClientServiceDetailReceiptId")
                .IsRequired();

            builder.Property(p => p.ClientServiceDetailId)
              .HasColumnName("ClientServiceDetailId")
              .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne(p => p.ClientServiceDetail)
                .WithMany(m => m.ClientServiceDetailReceipts)
                .HasForeignKey(f => f.ClientServiceDetailId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
