using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class InterestMap : IEntityTypeConfiguration<Interest>
    {
        public void Configure(EntityTypeBuilder<Interest> builder)
        {
            builder.ToTable("tbl_Interest");
            builder.HasKey(k => k.InterestId);

            #region Properties
            builder.Property(p => p.InterestId)
               .HasColumnName("InterestId")
               .IsRequired();

            builder.Property(p => p.GoalId)
               .HasColumnName("GoalId")
               .IsRequired();

            builder.Property(p => p.LeisureActivity)
               .HasColumnName("LeisureActivity")
               .IsRequired();

            builder.Property(p => p.CommunityActivity)
               .HasColumnName("CommunityActivity")
               .IsRequired();

            builder.Property(p => p.InformalActivity)
               .HasColumnName("InformalActivity")
               .IsRequired();

            builder.Property(p => p.MaintainContact)
                .HasColumnName("MaintainContact")
                .IsRequired();

            builder.Property(p => p.EventAwarness)
               .HasColumnName("EventAwarness")
               .IsRequired();

            builder.Property(p => p.GoalAndObjective)
               .HasColumnName("GoalAndObjective")
               .IsRequired();
            #endregion
        }
    }
}
