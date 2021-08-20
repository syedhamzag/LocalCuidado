using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class KeyIndicatorLogMap : IEntityTypeConfiguration<KeyIndicatorLog>
    {
        public void Configure(EntityTypeBuilder<KeyIndicatorLog> builder)
        {
            builder.ToTable("tbl_KeyIndicatorLog");
            builder.HasKey(k => k.KeyIndicatorLogId);

            #region Properties

            builder.Property(p => p.KeyIndicatorLogId)
               .HasColumnName("KeyIndicatorLogId")
               .IsRequired();

            builder.Property(p => p.KeyId)
                .HasColumnName("KeyId")
                .IsRequired();

            builder.Property(p => p.BaseRecordId)
               .HasColumnName("BaseRecordId")
               .IsRequired();

            #endregion
        }
    }
}
