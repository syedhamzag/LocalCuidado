using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffBlackListMap : IEntityTypeConfiguration<StaffBlackList>
    {
        public void Configure(EntityTypeBuilder<StaffBlackList> builder)
        {
            builder.ToTable("tbl_StaffBlackList");
            builder.HasKey(k => k.StaffBlackListId);

            #region Properties
            builder.Property(p => p.StaffBlackListId)
                .HasColumnName("StaffBlackListId")
                .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.ClientId)
              .HasColumnName("ClientId")
              .IsRequired();

            builder.Property(p => p.Comment)
             .HasColumnName("Comment")
             .HasMaxLength(225)
             .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.StaffPersonalInfo)
                .WithMany(m => m.StaffBlackList)
                .HasForeignKey(f => f.StaffPersonalInfoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Client)
               .WithMany(m => m.StaffBlackList)
               .HasForeignKey(f => f.ClientId)
               .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
