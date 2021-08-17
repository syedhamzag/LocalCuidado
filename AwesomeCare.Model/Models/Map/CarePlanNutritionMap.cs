using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class CarePlanNutritionMap : IEntityTypeConfiguration<CarePlanNutrition>
    {
        public void Configure(EntityTypeBuilder<CarePlanNutrition> builder)
        {
            builder.ToTable("tbl_CarePlanNutrition");
            builder.HasKey(k => k.NutritionId);

            #region Properties
            builder.Property(p => p.NutritionId)
               .HasColumnName("NutritionId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.AvoidFood)
               .HasColumnName("AvoidFood")
               .IsRequired();

            builder.Property(p => p.DrinkType)
               .HasColumnName("DrinkType")
               .IsRequired();

            builder.Property(p => p.EatingDifficulty)
               .HasColumnName("EatingDifficulty")
               .IsRequired();

            builder.Property(p => p.FoodIntake)
               .HasColumnName("FoodIntake")
               .IsRequired();

            builder.Property(p => p.FoodStorage)
               .HasColumnName("FoodStorage")
               .IsRequired();

            builder.Property(p => p.MealPreparation)
               .HasColumnName("MealPreparation")
               .IsRequired();

            builder.Property(p => p.RiskMitigations)
               .HasColumnName("RiskMitigations")
               .IsRequired();

            builder.Property(p => p.ServingMeal)
               .HasColumnName("ServingMeal")
               .IsRequired();

            builder.Property(p => p.SpecialDiet)
               .HasColumnName("SpecialDiet")
               .IsRequired();

            builder.Property(p => p.ThingsILike)
               .HasColumnName("ThingsILike")
               .IsRequired();

            builder.Property(p => p.WhenRestock)
               .HasColumnName("WhenRestock")
               .IsRequired();

            builder.Property(p => p.WhoRestock)
               .HasColumnName("WhoRestock")
               .IsRequired();
            #endregion
        }
    }
}
