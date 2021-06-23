using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class SupervisionWorkteamMap : IEntityTypeConfiguration<SupervisionWorkteam>
    {
        public void Configure(EntityTypeBuilder<SupervisionWorkteam> builder)
        {
            builder.ToTable("tbl_SupervisionWorkteam");
            builder.HasKey(k => k.SupervisionWorkteamId);

            #region Properties
            builder.Property(p => p.SupervisionWorkteamId)
               .HasColumnName("SupervisionWorkteamId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.StaffSupervisionAppraisalId)
             .HasColumnName("StaffSupervisionAppraisalId")
             .IsRequired();

            #endregion
        }
    }
}
