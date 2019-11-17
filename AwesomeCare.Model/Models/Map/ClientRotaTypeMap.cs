using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientRotaTypeMap : IEntityTypeConfiguration<ClientRotaType>
    {
        public void Configure(EntityTypeBuilder<ClientRotaType> builder)
        {
            builder.ToTable("tbl_ClientRotaType");
            builder.HasKey(p => p.ClientRotaTypeId);

            #region Properties
            builder.Property(p => p.ClientRotaTypeId)
                .HasColumnName("ClientRotaTypeId")
                .IsRequired();

            builder.Property(p => p.RotaType)
                .HasColumnName("RotaType")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(p => p.Deleted)
               .HasColumnName("Deleted")
               .IsRequired();

            builder.HasIndex(i => i.RotaType)
                .IsUnique(true)
                .HasName("IX_tbl_ClientRotaType_RotaType");
            #endregion
        }
    }
}
