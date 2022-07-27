using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class HealthConditionMap : IEntityTypeConfiguration<HealthCondition>
    {
        public void Configure(EntityTypeBuilder<HealthCondition> builder)
        {
            builder.ToTable("tbl_HealthCondition");
            builder.HasKey(k => k.HCId);

            #region Properties
            builder.Property(p => p.Name)
               .HasColumnName("Name")
               .IsRequired();

            builder.Property(p => p.Description)
               .HasColumnName("Description")
               .IsRequired();
            #endregion
        }
    }
}
