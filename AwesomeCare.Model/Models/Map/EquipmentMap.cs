using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class EquipmentMap : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.ToTable("tbl_Equipment");
            builder.HasKey(k => k.EquipmentId);

            #region Properties

            builder.Property(p => p.EquipmentId)
               .HasColumnName("EquipmentId")
               .IsRequired();

            builder.Property(p => p.PersonalDetailId)
               .HasColumnName("PersonalDetailId")
               .IsRequired();

            builder.Property(p => p.Name)
              .HasColumnName("Name")
              .IsRequired();

            builder.Property(p => p.Type)
             .HasColumnName("Type")
             .IsRequired();

            builder.Property(p => p.Location)
              .HasColumnName("Location")
              .IsRequired();

            builder.Property(p => p.ServiceDate)
             .HasColumnName("ServiceDate")
             .IsRequired();

            builder.Property(p => p.NextServiceDate)
              .HasColumnName("NextServiceDate")
              .IsRequired();

            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired();

            builder.Property(p => p.Status)
             .HasColumnName("Status")
             .IsRequired();

            builder.Property(p => p.PersonToAct)
             .HasColumnName("PersonToAct")
             .IsRequired();

            builder.Property(p => p.StaffId)
             .HasColumnName("StaffId")
             .IsRequired();
            #endregion

            #region Relationship
            builder.HasOne(p => p.Staff)
                 .WithMany(p => p.Equipment)
                 .HasForeignKey(p => p.StaffId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
