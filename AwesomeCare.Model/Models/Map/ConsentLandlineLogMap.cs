using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{

    public class ConsentLandlineLogMap : IEntityTypeConfiguration<ConsentLandlineLog>
    {
        public void Configure(EntityTypeBuilder<ConsentLandlineLog> builder)
        {
            builder.ToTable("ConsentLandlineLog");
            builder.HasKey(k => k.ConsentLandlineLogId);

            #region Properties

            builder.Property(p => p.ConsentLandlineLogId)
               .HasColumnName("ConsentLandlineLogId")
               .IsRequired();

            builder.Property(p => p.LandlineId)
                .HasColumnName("LandlineId")
                .IsRequired();

            builder.Property(p => p.BaseRecordId)
               .HasColumnName("BaseRecordId")
               .IsRequired();

            #endregion
        }
    }
}
