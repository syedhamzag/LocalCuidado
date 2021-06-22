using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientFoodIntakeMap : IEntityTypeConfiguration<ClientFoodIntake>
    {
        public void Configure(EntityTypeBuilder<ClientFoodIntake> builder)
        {
            builder.ToTable("tbl_ClientFoodIntake");
            builder.HasKey(k => k.FoodIntakeId);

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

            builder.Property(p => p.Goal)
               .HasColumnName("Goal")
               .IsRequired();

            builder.Property(p => p.CurrentIntake)
               .HasColumnName("CurrentIntake")
               .IsRequired();

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
                 .WithMany(p => p.ClientFoodIntake)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<FoodIntakePhysician>(p => p.Physician)
                .WithOne(p => p.FoodIntake)
                .HasForeignKey(p => p.FoodIntakeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<FoodIntakeStaffName>(p => p.StaffName)
                .WithOne(p => p.FoodIntake)
                .HasForeignKey(p => p.FoodIntakeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<FoodIntakeOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.FoodIntake)
                .HasForeignKey(p => p.FoodIntakeId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
