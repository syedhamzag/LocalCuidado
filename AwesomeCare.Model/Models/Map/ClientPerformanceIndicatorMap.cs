using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientPerformanceIndicatorMap : IEntityTypeConfiguration<ClientPerformanceIndicator>
    {
        public void Configure(EntityTypeBuilder<ClientPerformanceIndicator> builder)
        {
            builder.ToTable("tbl_ClientPerformanceIndicator");
            builder.HasKey(k => k.PerformanceIndicatorId);

            #region Properties

            builder.Property(p => p.Heading)
               .HasColumnName("Heading")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.DueDate)
               .HasColumnName("DueDate")
               .IsRequired();

            builder.Property(p => p.Rating)
               .HasColumnName("Rating")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .IsRequired();

            builder.Property(p => p.ClientId)
                .HasColumnName("ClientId")
                .IsRequired();

            #endregion

            #region Relationship
            builder.HasMany<ClientPerformanceIndicatorTask>(p => p.ClientPerformanceIndicatorTask)
                .WithOne(p => p.ClientPerformanceIndicator)
                .HasForeignKey(p => p.PerformanceIndicatorId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
