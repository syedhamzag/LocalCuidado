using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientMealTypeMap : IEntityTypeConfiguration<ClientMealType>
    {
        public void Configure(EntityTypeBuilder<ClientMealType> builder)
        {
            builder.ToTable("tbl_ClientMealType");
            builder.HasKey(p => p.ClientMealTypeId);

            #region Properties
            builder.Property(p => p.ClientMealTypeId)
                .HasColumnName("ClientMealTypeId")
                .IsRequired();

            builder.Property(p => p.MealType)
                .HasColumnName("MealType")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(p => p.Deleted)
               .HasColumnName("Deleted")
               .IsRequired();

            builder.HasIndex(i => i.MealType)
                .IsUnique(true)
                .HasName("IX_tbl_ClientMealType_MealType");
            #endregion
        }
    }
}
