using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class CommunicationMap : IEntityTypeConfiguration<Communication>
    {
        public void Configure(EntityTypeBuilder<Communication> builder)
        {
            builder.ToTable("tbl_Communication");
            builder.HasKey(k => k.CommunicationId);

            #region Properties
            builder.Property(p => p.CommunicationId)
                .HasColumnName("CommunicationId")
                .IsRequired();

            builder.Property(p => p.From)
               .HasColumnName("FromUserId")
               .IsRequired();

            builder.Property(p => p.To)
              .HasColumnName("ToUserId")
              .IsRequired();

            builder.Property(p => p.Message)
              .HasColumnName("Message")
              .IsRequired();

            builder.Property(p => p.CommuncationDate)
              .HasColumnName("CommuncationDate")
              .IsRequired();

            builder.Property(p => p.IsRead)
              .HasColumnName("IsRead")
              .IsRequired();

            builder.Property(p => p.Subject)
             .HasColumnName("Subject")
             .HasMaxLength(125)
             .IsRequired(false);
            #endregion
        }
    }
}
