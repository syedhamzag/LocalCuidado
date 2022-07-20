using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class RotaPinMap : IEntityTypeConfiguration<RotaPin>
    {
        public void Configure(EntityTypeBuilder<RotaPin> builder)
        {
            builder.ToTable("tbl_RotaPin");
            builder.HasKey(p => p.PinId);

            #region Properties
            builder.Property(p => p.Pin)
                .HasColumnName("Pin")
                .IsRequired();

            builder.Property(p => p.Key)
                .HasColumnName("Key")
                .HasMaxLength(50)
                .IsRequired();
            #endregion
        }
    }
}
