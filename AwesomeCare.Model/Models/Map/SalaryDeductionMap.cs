using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    class SalaryDeductionMap : IEntityTypeConfiguration<SalaryDeduction>
    {
        public void Configure(EntityTypeBuilder<SalaryDeduction> builder)
        {
            builder.ToTable("tbl_SalaryDeduction");
            builder.HasKey(k => k.SalaryDeductionId);

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
                    .WithMany(m => m.SalaryDeduction)
                    .HasForeignKey(f => f.StaffPersonalInfoId)
                    .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
