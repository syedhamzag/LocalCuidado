using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class InvestigationMap : IEntityTypeConfiguration<Investigation>
    {
        public void Configure(EntityTypeBuilder<Investigation> builder)
        {
            builder.ToTable("tbl_Investigation");
            builder.HasKey(k => k.InvestigationId);

            #region Properties
            builder.Property(p => p.InvestigationId)
                .HasColumnName("InvestigationId")
                .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.IncidentClass)
               .HasColumnName("IncidentClass")
               .IsRequired();

            builder.Property(p => p.Remark)
               .HasColumnName("Remark")
               .HasMaxLength(500)
               .IsRequired();

            builder.Property(p => p.IncidentDate)
               .HasColumnName("IncidentDate")
               .IsRequired();

            builder.Property(p => p.ConclusionDate)
               .HasColumnName("ConclusionDate")
               .IsRequired(false);
            #endregion
        }
    }
}
