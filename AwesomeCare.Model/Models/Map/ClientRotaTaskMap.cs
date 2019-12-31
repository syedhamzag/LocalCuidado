using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientRotaTaskMap : IEntityTypeConfiguration<ClientRotaTask>
    {
        public void Configure(EntityTypeBuilder<ClientRotaTask> builder)
        {
            builder.ToTable("tbl_ClientRotaTask");
            builder.HasKey(p => p.ClientRotaTaskId);

            #region Properties
            builder.Property(p => p.ClientRotaTaskId)
                .HasColumnName("ClientRotaTaskId")
                .IsRequired();

            builder.Property(p => p.RotaTaskId)
               .HasColumnName("RotaTaskId")
               .IsRequired();

            builder.Property(p => p.ClientRotaDaysId)
               .HasColumnName("ClientRotaDaysId")
               .IsRequired();
            #endregion

            #region Relationships
            builder.HasOne(p => p.ClientRotaDays)
                .WithMany(p => p.ClientRotaTask)
                .HasForeignKey(p => p.ClientRotaDaysId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.RotaTask)
                .WithMany(p => p.ClientRotaTask)
                .HasForeignKey(p => p.RotaTaskId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
