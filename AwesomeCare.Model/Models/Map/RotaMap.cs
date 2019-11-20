using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class RotaMap : IEntityTypeConfiguration<Rota>
    {
        public void Configure(EntityTypeBuilder<Rota> builder)
        {
            builder.ToTable("tbl_ClientRota");
            builder.HasKey(k => k.RotaId);

            #region Properties
            builder.Property(p => p.RotaId)
                .IsRequired();

            builder.Property(p => p.RotaName)
                .HasColumnName("RotaName")
                .HasMaxLength(225)
                .IsRequired();

            builder.Property(p => p.NumberOfStaff)
               .HasColumnName("NumberOfStaff")
               .IsRequired();

            builder.Property(p => p.Gender)
               .HasColumnName("Gender")
               .HasMaxLength(15)
               .IsRequired();

            builder.Property(p => p.Area)
               .HasColumnName("Area")
               .HasMaxLength(225);

            builder.Property(p => p.Deleted)
               .HasColumnName("Deleted");


            #endregion
        }
    }
}
