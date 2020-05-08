using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffRotaMap : IEntityTypeConfiguration<StaffRota>
    {
        public void Configure(EntityTypeBuilder<StaffRota> builder)
        {
            builder.ToTable("tbl_StaffRota");
            builder.HasKey(k => k.StaffRotaId);

            #region Properties
            
            builder.Property(p => p.StaffRotaId)
                .HasColumnName("StaffRotaId")
                .IsRequired();

            builder.Property(p => p.RotaDate)
               .HasColumnName("RotaDate")
               .HasColumnType("date")
               .IsRequired();

            builder.Property(p => p.Staff)
               .HasColumnName("Staff")
               .IsRequired();

            builder.Property(p => p.RotaId)
               .HasColumnName("RotaId")
               .IsRequired();

            builder.Property(p => p.ReferenceNumber)
             .HasColumnName("ReferenceNumber")
              .HasMaxLength(50)
             .IsRequired();


            builder.Property(p => p.Remark)
               .HasColumnName("Remark")
               .HasMaxLength(225)
               .IsRequired(false);
            #endregion

           
        }
    }
}
