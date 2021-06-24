using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientMealDaysMap : IEntityTypeConfiguration<ClientMealDays>
    {
        public void Configure(EntityTypeBuilder<ClientMealDays> builder)
        {
            builder.ToTable("tbl_Client_MealDay");
            builder.HasKey(p => p.ClientMealId);

            #region Properties
            builder.Property(p => p.ClientMealId)
                .HasColumnName("ClientMealId")
                .IsRequired();

            builder.Property(p => p.NutritionId)
               .HasColumnName("NutritionId")
               .IsRequired();

            builder.Property(p => p.MealDayofWeekId)
              .HasColumnName("MealDayofWeekId")
              .IsRequired();

            builder.Property(p => p.MEALDETAILS)
             .HasColumnName("MEALDETAILS")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.HOWTOPREPARE)
             .HasColumnName("HOWTOPREPARE")
              .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.SEEVIDEO)
             .HasColumnName("SEEVIDEO")
              .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.PICTURE)
             .HasColumnName("PICTURE")
             .IsRequired();

            builder.Property(p => p.TypeId)
             .HasColumnName("TypeId")
             .IsRequired();

            builder.Property(p => p.ClientMealTypeId)
                .HasColumnName("ClientMealTypeId")
                .IsRequired();

            #endregion

            #region Relationships
            builder.HasOne(p => p.ClientNutrition)
                .WithMany(p => p.ClientMealDays)
                .HasForeignKey(p => p.NutritionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.MealDayofWeek)
                .WithMany(p => p.ClientMealDays)
                .HasForeignKey(p => p.MealDayofWeekId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ClientMealType)
                .WithMany(p => p.ClientMeal)
                .HasForeignKey(p => p.ClientMealTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
