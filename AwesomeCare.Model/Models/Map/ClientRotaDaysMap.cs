using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientRotaDaysMap : IEntityTypeConfiguration<ClientRotaDays>
    {
        public void Configure(EntityTypeBuilder<ClientRotaDays> builder)
        {
            builder.ToTable("tbl_ClientRotaDays");
            builder.HasKey(p => p.ClientRotaDaysId);

            #region Properties
            builder.Property(p => p.ClientRotaDaysId)
                .HasColumnName("ClientRotaDaysId")
                .IsRequired();

            builder.Property(p => p.ClientRotaId)
               .HasColumnName("ClientRotaId")
               .IsRequired();

            builder.Property(p => p.RotaDayofWeekId)
              .HasColumnName("RotaDayofWeekId")
              .IsRequired();

            builder.Property(p => p.StartTime)
             .HasColumnName("StartTime")
             .HasMaxLength(25)
             .IsRequired();

            builder.Property(p => p.StopTime)
             .HasColumnName("StopTime")
              .HasMaxLength(25)
             .IsRequired();
            #endregion

            #region Relationships
            builder.HasOne(p => p.ClientRota)
                .WithMany(p => p.ClientRotaDays)
                .HasForeignKey(p => p.ClientRotaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.RotaDayofWeek)
                .WithMany(p => p.ClientRotaDays)
                .HasForeignKey(p => p.RotaDayofWeekId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
