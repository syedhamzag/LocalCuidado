using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientShoppingMap : IEntityTypeConfiguration<ClientShopping>
    {
        public void Configure(EntityTypeBuilder<ClientShopping> builder)
        {
            builder.ToTable("tbl_ClientShopping");
            builder.HasKey(p => p.ShoppingId);

            #region Properties
            builder.Property(p => p.ShoppingId)
                .HasColumnName("ShoppingId")
                .IsRequired();

            builder.Property(p => p.NutritionId)
               .HasColumnName("NutritionId")
               .IsRequired();

            builder.Property(p => p.Quantity)
              .HasColumnName("Quantity")
              .IsRequired();

            builder.Property(p => p.MeansOfPurchase)
             .HasColumnName("MeansOfPurchase")
             .HasMaxLength(100)
             .IsRequired();

            builder.Property(p => p.LocationOfPurchase)
             .HasColumnName("LocationOfPurchase")
              .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.Item)
             .HasColumnName("Item")
              .HasMaxLength(100)
             .IsRequired();

            builder.Property(p => p.Description)
             .HasColumnName("Description")
              .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.Amount)
             .HasColumnName("Amount")
             .IsRequired();

            builder.Property(p => p.DAYOFSHOPPING)
             .HasColumnName("DAYOFSHOPPING")
              .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.Image)
             .HasColumnName("Image")
             .IsRequired();

            builder.Property(p => p.STAFFId)
                .HasColumnName("STAFFId")
                .IsRequired();

            builder.Property(p => p.DATEFROM)
                .HasColumnName("DATEFROM")
                .IsRequired();

            builder.Property(p => p.DATETO)
                .HasColumnName("DATETO")
                .IsRequired();

            #endregion

            #region Relationships
            builder.HasOne(p => p.ClientNutrition)
                .WithMany(p => p.ClientShopping)
                .HasForeignKey(p => p.NutritionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.StaffPersonalInfo)
                .WithMany(p => p.ClientShopping)
                .HasForeignKey(p => p.STAFFId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
