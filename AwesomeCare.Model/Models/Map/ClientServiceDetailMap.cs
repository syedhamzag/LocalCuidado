using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientServiceDetailMap : IEntityTypeConfiguration<ClientServiceDetail>
    {
        public void Configure(EntityTypeBuilder<ClientServiceDetail> builder)
        {
            builder.ToTable("tbl_ClientServiceDetail");
            builder.HasKey(k => k.ClientServiceDetailId);

            #region Properties
            builder.Property(p => p.ClientServiceDetailId)
                .HasColumnName("ClientServiceDetailId")
                .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();

            builder.Property(p => p.ClientId)
              .HasColumnName("ClientId")
              .IsRequired();

            builder.Property(p => p.AmountGiven)
              .HasColumnName("AmountGiven")
              .HasColumnType("decimal(18,2)")
              .IsRequired();

            builder.Property(p => p.AmountReturned)
              .HasColumnName("AmountReturned")
              .HasColumnType("decimal(18,2)")
              .IsRequired();

            builder.Property(p => p.ServiceDate)
              .HasColumnName("ServiceDate")
              .IsRequired();
            #endregion
        }
    }
}
