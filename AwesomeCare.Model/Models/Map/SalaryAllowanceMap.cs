using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class SalaryAllowanceMap : IEntityTypeConfiguration<SalaryAllowance>
    {
        public void Configure(EntityTypeBuilder<SalaryAllowance> builder)
        {
            builder.ToTable("tbl_SalaryAllowance");
            builder.HasKey(k => k.SalaryAllowanceId);

            #region Properties
            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.AllowanceType)
              .HasColumnName("AllowanceType")
              .IsRequired();

            builder.Property(p => p.Reoccurent)
             .HasColumnName("Reoccurent")
             .IsRequired();

            builder.Property(p => p.Amount)
             .HasColumnName("Amount")
             .IsRequired();

            builder.Property(p => p.Percentage)
             .HasColumnName("Percentage")
             .IsRequired();

            builder.Property(p => p.StartDate)
             .HasColumnName("StartDate")
             .IsRequired();

            builder.Property(p => p.EndDate)
             .HasColumnName("EndDate")
             .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.StaffPersonalInfo)
                    .WithMany(m => m.SalaryAllowance)
                    .HasForeignKey(f => f.StaffPersonalInfoId)
                    .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}