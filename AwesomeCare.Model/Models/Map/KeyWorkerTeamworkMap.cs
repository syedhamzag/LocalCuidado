using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class KeyWorkerWorkteamMap : IEntityTypeConfiguration<KeyWorkerWorkteam>
    {
        public void Configure(EntityTypeBuilder<KeyWorkerWorkteam> builder)
        {
            builder.ToTable("tbl_KeyWorkerWorkteam");
            builder.HasKey(k => k.KeyWorkerWorkteamId);

            #region Properties
            builder.Property(p => p.KeyWorkerWorkteamId)
               .HasColumnName("KeyWorkerWorkteamId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.KeyWorkerId)
             .HasColumnName("KeyWorkerId")
             .IsRequired();

            #endregion
        }
    }
}
