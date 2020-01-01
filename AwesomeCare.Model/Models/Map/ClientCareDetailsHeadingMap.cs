using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientCareDetailsHeadingMap : IEntityTypeConfiguration<ClientCareDetailsHeading>
    {
        public void Configure(EntityTypeBuilder<ClientCareDetailsHeading> builder)
        {
            builder.ToTable("tbl_ClientCareDetailsHeading");
            builder.HasKey(k => k.ClientCareDetailsHeadingId);

            #region Properties
            builder.Property(p => p.ClientCareDetailsHeadingId)
              .HasColumnName("ClientCareDetailsHeadingId")
              .IsRequired();

            builder.Property(p => p.Heading)
                .HasColumnName("Heading")
                .HasMaxLength(225)
                .IsRequired();
            #endregion

            builder.HasIndex(c => c.Heading)
                .IsUnique(true)
                .HasName("IX_tbl_ClientCareDetailsHeading_Heading");
        }
    }
}
