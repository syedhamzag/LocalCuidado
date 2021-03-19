using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffRotaTaskMap : IEntityTypeConfiguration<StaffRotaTask>
    {
        public void Configure(EntityTypeBuilder<StaffRotaTask> builder)
        {
            builder.ToTable("tbl_StaffRotaTask");
            builder.HasKey(k => k.StaffRotaTaskId);

            #region Properties
            builder.Property(p => p.StaffRotaTaskId)
                .HasColumnName("StaffRotaTaskId")
                .IsRequired();

            builder.Property(p => p.StaffRotaPeriodId)
                .HasColumnName("StaffRotaPeriodId")
                .IsRequired();

            builder.Property(p => p.RotaTaskId)
                .HasColumnName("RotaTaskId")
                .IsRequired();

            builder.Property(p => p.IsGiven)
                .HasColumnName("IsGiven")
                .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.StaffRotaPeriod)
                .WithMany(m => m.StaffRotaTasks)
                .HasForeignKey(f => f.StaffRotaPeriodId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
