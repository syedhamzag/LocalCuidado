using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientNutritionMap : IEntityTypeConfiguration<ClientNutrition>
    {
        public void Configure(EntityTypeBuilder<ClientNutrition> builder)
        {
            builder.ToTable("tbl_ClientNutrition");
            builder.HasKey(p => p.NutritionId);

            #region Properties
            builder.Property(p => p.NutritionId)
                .HasColumnName("NutritionId")
                .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.StaffId)
               .HasColumnName("StaffId")
               .IsRequired();

            builder.Property(p => p.MealSpecialNote)
               .HasColumnName("MealSpecialNote")
               .IsRequired();

            builder.Property(p => p.ShoppingSpecialNote)
               .HasColumnName("ShoppingSpecialNote")
               .IsRequired();

            builder.Property(p => p.CleaningSpecialNote)
               .HasColumnName("CleaningSpecialNote")
               .IsRequired();

            builder.Property(p => p.DATEFROM)
                .HasColumnName("DATEFROM")
                .IsRequired();

            builder.Property(p => p.DATETO)
                .HasColumnName("DATETO")
                .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientNutrition)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.ClientNutrition)
                 .HasForeignKey(p => p.StaffId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
