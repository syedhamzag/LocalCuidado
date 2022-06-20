using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    class ChatMap : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("tbl_ClientChat");
            builder.HasKey(k => k.ChatId);

            #region Properties
            builder.Property(p => p.Dated)
               .HasColumnName("Dated")
               .IsRequired();

            builder.Property(p => p.Message)
               .HasColumnName("Message")
               .IsRequired();

            builder.Property(p => p.Type)
               .HasColumnName("Type")
               .IsRequired();

            builder.Property(p => p.SenderId)
               .HasColumnName("SenderId")
               .IsRequired();

            builder.Property(p => p.ReceiverId)
            .HasColumnName("ReceiverId")
            .IsRequired();
            #endregion

            #region Relationship
            #endregion
        }
    }
}
