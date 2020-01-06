using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientCareDetailsMap : IEntityTypeConfiguration<ClientCareDetails>
    {
        public void Configure(EntityTypeBuilder<ClientCareDetails> builder)
        {
            builder.ToTable("tbl_ClientCareDetails");
            builder.HasKey(k => k.ClientCareDetailsId);


            #region Properties

            builder.Property(p => p.ClientCareDetailsId)
                   .HasColumnName("ClientCareDetailsId")
                   .IsRequired();

            builder.Property(p => p.ClientCareDetailsTaskId)
                     .HasColumnName("ClientCareDetailsTaskId")
                     .IsRequired();

            builder.Property(p => p.ClientId)
                 .HasColumnName("ClientId")
                 .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnName("Description")
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(p => p.Risk)
               .HasColumnName("Risk")
               .HasMaxLength(250)
               .IsRequired();

            builder.Property(p => p.Mitigation)
              .HasColumnName("Mitigation")
              .HasMaxLength(250)
              .IsRequired(false);

            builder.Property(p => p.Location)
              .HasColumnName("Location")
              .HasMaxLength(250)
              .IsRequired(false);

            builder.Property(p => p.Remark)
             .HasColumnName("Remark")
             .HasMaxLength(250)
             .IsRequired(false);
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                .WithMany(p => p.ClientCareDetails)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ClientCareDetailsTask)
                .WithMany(p => p.ClientCareDetails)
                .HasForeignKey(p => p.ClientCareDetailsTaskId);

            #endregion
        }
    }
}
