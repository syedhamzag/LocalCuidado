using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PersonalHygieneMap : IEntityTypeConfiguration<PersonalHygiene>
    {
        public void Configure(EntityTypeBuilder<PersonalHygiene> builder)
        {
            builder.ToTable("tbl_PersonalHygiene");
            builder.HasKey(k => k.HygieneId);

            #region Properties
            builder.Property(p => p.HygieneId)
               .HasColumnName("HygieneId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Cleaning)
               .HasColumnName("Cleaning")
               .IsRequired();

            builder.Property(p => p.CleaningFreq)
               .HasColumnName("CleaningFreq")
               .IsRequired();

            builder.Property(p => p.CleaningTools)
               .HasColumnName("CleaningTools")
               .IsRequired();

            builder.Property(p => p.DesiredCleaning)
               .HasColumnName("DesiredCleaning")
               .IsRequired();

            builder.Property(p => p.DirtyLaundry)
               .HasColumnName("DirtyLaundry")
               .IsRequired();

            builder.Property(p => p.DryLaundry)
               .HasColumnName("DryLaundry")
               .IsRequired();

            builder.Property(p => p.GeneralAppliance)
               .HasColumnName("GeneralAppliance")
               .IsRequired();

            builder.Property(p => p.Ironing)
               .HasColumnName("Ironing")
               .IsRequired();

            builder.Property(p => p.LaundryGuide)
               .HasColumnName("LaundryGuide")
               .IsRequired();

            builder.Property(p => p.LaundrySupport)
               .HasColumnName("LaundrySupport")
               .IsRequired();

            builder.Property(p => p.WashingMachine)
               .HasColumnName("WashingMachine")
               .IsRequired();

            builder.Property(p => p.WhoClean)
               .HasColumnName("WhoClean")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.PersonalHygiene)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
