using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ConsentDataMap : IEntityTypeConfiguration<ConsentData>
    {
        public void Configure(EntityTypeBuilder<ConsentData> builder)
        {
            builder.ToTable("tbl_ConsentData");
            builder.HasKey(k => k.DataId);

            #region Properties

            builder.Property(p => p.DataId)
               .HasColumnName("DataId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Signature)
              .HasColumnName("Signature")
              .IsRequired();

            builder.Property(p => p.Date)
             .HasColumnName("Date")
             .IsRequired();

            #endregion
        }
    }
}
