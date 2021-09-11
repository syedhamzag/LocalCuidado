using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PetsMap : IEntityTypeConfiguration<Pets>
    {
        public void Configure(EntityTypeBuilder<Pets> builder)
        {
            builder.ToTable("tbl_Pets");
            builder.HasKey(k => k.PetsId);

            #region Properties
            builder.Property(p => p.PetsId)
               .HasColumnName("PetsId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Name)
               .HasColumnName("Name")
               .IsRequired();

            builder.Property(p => p.Type)
               .HasColumnName("Type")
               .IsRequired();

            builder.Property(p => p.Age)
               .HasColumnName("Age")
               .IsRequired();

            builder.Property(p => p.Gender)
               .HasColumnName("Gender")
               .IsRequired();

            builder.Property(p => p.PetActivities)
                .HasColumnName("PetActivities")
                .IsRequired();

            builder.Property(p => p.MealStorage)
               .HasColumnName("MealStorage")
               .IsRequired();

            builder.Property(p => p.VetVisit)
               .HasColumnName("VetVisit")
               .IsRequired();

            builder.Property(p => p.PetInsurance)
               .HasColumnName("PetInsurance")
               .IsRequired();

            builder.Property(p => p.PetCare)
               .HasColumnName("PetCare")
               .IsRequired();

            builder.Property(p => p.MealPattern)
               .HasColumnName("MealPattern")
               .IsRequired();
            #endregion
        }
    }
}
