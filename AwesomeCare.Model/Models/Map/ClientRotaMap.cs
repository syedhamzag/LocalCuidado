using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientRotaMap : IEntityTypeConfiguration<ClientRota>
    {
        public void Configure(EntityTypeBuilder<ClientRota> builder)
        {
            builder.ToTable("tbl_ClientRota");
            builder.HasKey(p => p.ClientRotaId);

            #region Properties
            builder.Property(p => p.ClientRotaId)
                .HasColumnName("ClientRotaId")
                .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.ClientRotaTypeId)
               .HasColumnName("ClientRotaTypeId")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientRota)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ClientRotaType)
                .WithMany(p => p.ClientRota)
                .HasForeignKey(p => p.ClientRotaTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
