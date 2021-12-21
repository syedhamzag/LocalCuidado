using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class FoodIntakeStaffNameMap : IEntityTypeConfiguration<FoodIntakeStaffName>
    {
        public void Configure(EntityTypeBuilder<FoodIntakeStaffName> builder)
        {
            builder.ToTable("tbl_FoodIntake_StaffName");
            builder.HasKey(k => k.FoodIntakeStaffNameId);

            #region Properties
            builder.Property(p => p.FoodIntakeStaffNameId)
               .HasColumnName("FoodIntakeStaffNameId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.FoodIntakeId)
             .HasColumnName("FoodIntakeId")
             .IsRequired();

            #endregion
        }
    }
}
