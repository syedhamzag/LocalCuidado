using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class FoodIntakePhysicianMap : IEntityTypeConfiguration<FoodIntakePhysician>
    {
        public void Configure(EntityTypeBuilder<FoodIntakePhysician> builder)
        {
            builder.ToTable("tbl_FoodIntakePhysician");
            builder.HasKey(k => k.FoodIntakePhysicianId);

            #region Properties
            builder.Property(p => p.FoodIntakePhysicianId)
               .HasColumnName("FoodIntakePhysicianId")
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
