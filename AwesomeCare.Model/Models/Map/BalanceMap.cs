using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class BalanceMap : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.ToTable("tbl_Balance");
            builder.HasKey(k => k.BalanceId);

            #region Properties
            builder.Property(p => p.BalanceId)
               .HasColumnName("BalanceId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Description)
               .HasColumnName("Description")
               .IsRequired();

            builder.Property(p => p.Mobility)
               .HasColumnName("Mobility")
               .IsRequired();

            builder.Property(p => p.Name)
               .HasColumnName("Name")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();
            #endregion
        }
    }
}
