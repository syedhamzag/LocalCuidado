using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PersonalDetailMap : IEntityTypeConfiguration<PersonalDetail>
    {
        public void Configure(EntityTypeBuilder<PersonalDetail> builder)
        {
            builder.ToTable("tbl_PersonalDetail");
            builder.HasKey(k => k.PersonalDetailId);

            #region Properties

            builder.Property(p => p.PersonalDetailId)
               .HasColumnName("PersonalDetailId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();
            #endregion


            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.PersonalDetail)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Capacity)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey<Capacity>(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ConsentCare)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey<ConsentCare>(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ConsentData)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey<ConsentData>(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ConsentLandLine)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey<ConsentLandLine>(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Equipment)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.KeyIndicators)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey<KeyIndicators>(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Personal)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey<Personal>(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PersonCentred)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Review)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey<Review>(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
