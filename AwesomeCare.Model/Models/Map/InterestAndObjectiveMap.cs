using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class InterestAndObjectiveMap : IEntityTypeConfiguration<InterestAndObjective>
    {
        public void Configure(EntityTypeBuilder<InterestAndObjective> builder)
        {
            builder.ToTable("tbl_InterestAndObjective");
            builder.HasKey(k => k.GoalId);

            #region Properties
            builder.Property(p => p.GoalId)
               .HasColumnName("GoalId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.CareGoal)
               .HasColumnName("CareGoal")
               .IsRequired();

            builder.Property(p => p.Brief)
               .HasColumnName("Brief")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.InterestAndObjective)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Interest)
                .WithOne(p => p.InterestAndObjective)
                .HasForeignKey(p => p.GoalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PersonalityTest)
                .WithOne(p => p.InterestAndObjective)
                .HasForeignKey(p => p.GoalId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
