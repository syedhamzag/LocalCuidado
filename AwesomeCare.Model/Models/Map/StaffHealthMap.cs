using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffHealthMap : IEntityTypeConfiguration<StaffHealth>
    {
        public void Configure(EntityTypeBuilder<StaffHealth> builder)
        {
            builder.ToTable("tbl_StaffHealth");
            builder.HasKey(k => k.StaffHealthId);

            #region Properties
            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();

            builder.Property(p => p.Heading)
               .HasColumnName("Heading")
               .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne(p => p.StaffPersonalInfo)
                 .WithMany(p => p.StaffHealth)
                 .HasForeignKey(p => p.StaffPersonalInfoId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<StaffHealthTask>(p => p.StaffHealthTask)
                .WithOne(p => p.StaffHealth)
                .HasForeignKey(p => p.StaffHealthId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}

