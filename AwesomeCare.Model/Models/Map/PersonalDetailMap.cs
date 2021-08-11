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

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();
            #endregion


            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.PersonalDetail)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Capacity>(p => p.Capacity)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<ConsentCare>(p => p.ConsentCare)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<ConsentData>(p => p.ConsentData)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<ConsentLandLine>(p => p.ConsentLandLine)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Equipment>(p => p.Equipment)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<KeyIndicators>(p => p.KeyIndicators)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Personal>(p => p.Personal)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<PersonCentred>(p => p.PersonCentred)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Review>(p => p.Review)
                .WithOne(p => p.PersonalDetail)
                .HasForeignKey(p => p.PersonalDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
