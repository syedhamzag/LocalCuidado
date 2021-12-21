using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class PhysicalAbilityMap : IEntityTypeConfiguration<PhysicalAbility>
    {
        public void Configure(EntityTypeBuilder<PhysicalAbility> builder)
        {
            builder.ToTable("tbl_PhysicalAbility");
            builder.HasKey(k => k.PhysicalId);

            #region Properties
            builder.Property(p => p.PhysicalId)
               .HasColumnName("PhysicalId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Description)
               .HasColumnName("Description")
               .IsRequired();

            builder.Property(p => p.Mobility)
               .HasColumnName("Mobility")
               .IsRequired();

            builder.Property(p => p.Name)
               .HasColumnName("Name")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();
            #endregion
        }
    }
}
