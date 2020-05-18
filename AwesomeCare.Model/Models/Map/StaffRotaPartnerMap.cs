using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffRotaPartnerMap : IEntityTypeConfiguration<StaffRotaPartner>
    {
        public void Configure(EntityTypeBuilder<StaffRotaPartner> builder)
        {
            builder.ToTable("tbl_StaffRotaPartner");
            builder.HasKey(k => k.StaffRotaPartnerId);

            #region Properties
            builder.Property(p => p.StaffRotaPartnerId)
                .HasColumnName("StaffRotaPartnerId")
                .IsRequired();

            builder.Property(p => p.StaffRotaId)
               .HasColumnName("StaffRotaId")
               .IsRequired();

            builder.Property(p => p.StaffId)
               .HasColumnName("StaffId")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(r => r.StaffRota)
                .WithMany(m => m.StaffRotaPartners)
                .HasForeignKey(f => f.StaffRotaId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
