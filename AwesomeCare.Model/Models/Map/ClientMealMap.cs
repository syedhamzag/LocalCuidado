using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientMealMap : IEntityTypeConfiguration<ClientMeal>
    {
        public void Configure(EntityTypeBuilder<ClientMeal> builder)
        {
            builder.ToTable("tbl_ClientMeal");
            builder.HasKey(p => p.ClientMealId);

            #region Properties
            builder.Property(p => p.ClientMealId)
                .HasColumnName("ClientMealId")
                .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.ClientMealTypeId)
               .HasColumnName("ClientMealTypeId")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientMeal)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ClientMealType)
                .WithMany(p => p.ClientMeal)
                .HasForeignKey(p => p.ClientMealTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
