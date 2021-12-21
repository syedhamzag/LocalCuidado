using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class SpecialHealthConditionMap : IEntityTypeConfiguration<SpecialHealthCondition>
    {
        public void Configure(EntityTypeBuilder<SpecialHealthCondition> builder)
        {
            builder.ToTable("tbl_SpecialHealthCondition");
            builder.HasKey(k => k.HealthCondId);

            #region Properties
            builder.Property(p => p.HealthCondId)
               .HasColumnName("HealthCondId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.ClientAction)
               .HasColumnName("ClientAction")
               .IsRequired();

            builder.Property(p => p.ClinicRecommendation)
               .HasColumnName("ClinicRecommendation")
               .IsRequired();

            builder.Property(p => p.ConditionName)
               .HasColumnName("ConditionName")
               .IsRequired();

            builder.Property(p => p.FeelingAfterIncident)
               .HasColumnName("FeelingAfterIncident")
               .IsRequired();

            builder.Property(p => p.FeelingBeforeIncident)
               .HasColumnName("FeelingBeforeIncident")
               .IsRequired();

            builder.Property(p => p.Frequency)
               .HasColumnName("Frequency")
               .IsRequired();

            builder.Property(p => p.LifestyleSupport)
               .HasColumnName("LifestyleSupport")
               .IsRequired();

            builder.Property(p => p.LivingActivities)
               .HasColumnName("LivingActivities")
               .IsRequired();

            builder.Property(p => p.PlanningHealthCondition)
               .HasColumnName("PlanningHealthCondition")
               .IsRequired();

            builder.Property(p => p.SourceInformation)
               .HasColumnName("SourceInformation")
               .IsRequired();

            builder.Property(p => p.Trigger)
               .HasColumnName("Trigger")
               .IsRequired();
            #endregion
        }
    }
}
