using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientEyeHealthMonitoringMap : IEntityTypeConfiguration<ClientEyeHealthMonitoring>
    {
        public void Configure(EntityTypeBuilder<ClientEyeHealthMonitoring> builder)
        {
            builder.ToTable("tbl_Client_EyeHealthMonitoring");
            builder.HasKey(k => k.EyeHealthId);

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

            builder.Property(p => p.ToolUsed)
               .HasColumnName("ToolUsed")
               .IsRequired();

            builder.Property(p => p.ToolUsedAttach)
               .HasColumnName("ToolUsedAttach");

            builder.Property(p => p.MethodUsed)
               .HasColumnName("MethodUsed")
               .IsRequired();

            builder.Property(p => p.MethodUsedAttach)
               .HasColumnName("MethodUsedAttach");

            builder.Property(p => p.TargetSet)
               .HasColumnName("TargetSet")
               .IsRequired();

            builder.Property(p => p.CurrentScore)
               .HasColumnName("CurrentScore")
               .IsRequired();

            builder.Property(p => p.PatientGlasses)
               .HasColumnName("PatientGlasses")
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
                 .WithMany(p => p.ClientEyeHealthMonitoring)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<EyeHealthPhysician>(p => p.Physician)
                .WithOne(p => p.EyeHealth)
                .HasForeignKey(p => p.EyeHealthId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<EyeHealthStaffName>(p => p.StaffName)
                .WithOne(p => p.EyeHealth)
                .HasForeignKey(p => p.EyeHealthId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<EyeHealthOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.EyeHealth)
                .HasForeignKey(p => p.EyeHealthId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
