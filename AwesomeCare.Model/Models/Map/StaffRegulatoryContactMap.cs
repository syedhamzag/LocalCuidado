using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffRegulatoryContactMap : IEntityTypeConfiguration<StaffRegulatoryContact>
    {
        public void Configure(EntityTypeBuilder<StaffRegulatoryContact> builder)
        {
            builder.ToTable("tbl_StaffRegulatoryContact");
            builder.HasKey(p => p.StaffRegulatoryContactId);

            #region Properties
            builder.Property(p => p.StaffRegulatoryContactId)
                .HasColumnName("StaffRegulatoryContactId")
                .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.BaseRecordItemId)
              .HasColumnName("BaseRecordItemId")
              .IsRequired();

            builder.Property(p => p.DatePerformed)
                .HasColumnName("DatePerformed")
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property(p => p.DueDate)
               .HasColumnName("DueDate")
               .HasColumnType("datetime2")
               .IsRequired(false);

            builder.Property(p => p.Evidence)
                .HasColumnName("Evidence")
                .IsRequired(false);
            #endregion
        }
    }
}
