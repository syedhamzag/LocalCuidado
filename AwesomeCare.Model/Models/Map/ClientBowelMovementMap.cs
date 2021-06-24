using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientBowelMovementMap : IEntityTypeConfiguration<ClientBowelMovement>
    {
        public void Configure(EntityTypeBuilder<ClientBowelMovement> builder)
        {
            builder.ToTable("tbl_Client_BowelMovement");
            builder.HasKey(k => k.BowelMovementId);

            #region Properties
            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.Time)
               .HasColumnName("Time")
               .IsRequired();

            builder.Property(p => p.Type)
               .HasColumnName("Type")
               .IsRequired();

            builder.Property(p => p.TypeAttach)
               .HasColumnName("TypeAttach");

            builder.Property(p => p.Size)
               .HasColumnName("Size")
               .IsRequired();

            builder.Property(p => p.Color)
               .HasColumnName("Color")
               .IsRequired();

            builder.Property(p => p.ColorAttach)
               .HasColumnName("ColorAttach");

            builder.Property(p => p.StatusImage)
               .HasColumnName("StatusImage")
               .IsRequired();

            builder.Property(p => p.StatusAttach)
               .HasColumnName("StatusAttach");

            builder.Property(p => p.Comment)
               .HasColumnName("Comment")
               .IsRequired();

            builder.Property(p => p.PhysicianResponse)
               .HasColumnName("PhysicianResponse")
               .IsRequired();

            builder.Property(p => p.Deadline)
               .HasColumnName("Deadline")
               .IsRequired();

            builder.Property(p => p.Remarks)
               .HasColumnName("Remarks")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.ClientBowelMovement)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
