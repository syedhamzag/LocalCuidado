using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientOxygenLvlMap : IEntityTypeConfiguration<ClientOxygenLvl>
    {
        public void Configure(EntityTypeBuilder<ClientOxygenLvl> builder)
        {
            builder.ToTable("tbl_Client_Oxygenlvl");
            builder.HasKey(k => k.OxygenLvlId);

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

            builder.Property(p => p.TargetOxygen)
               .HasColumnName("TargetOxygen")
               .IsRequired();

            builder.Property(p => p.TargetOxygenAttach)
               .HasColumnName("TargetOxygenAttach");

            builder.Property(p => p.CurrentReading)
               .HasColumnName("CurrentReading")
               .IsRequired();

            builder.Property(p => p.SeeChart)
               .HasColumnName("SeeChart")
               .IsRequired();

            builder.Property(p => p.SeeChartAttach)
               .HasColumnName("SeeChartAttach");

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
                 .WithMany(p => p.ClientOxygenLvl)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<OxygenLvlPhysician>(p => p.Physician)
                .WithOne(p => p.OxygenLvl)
                .HasForeignKey(p => p.OxygenLvlId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<OxygenLvlStaffName>(p => p.StaffName)
                .WithOne(p => p.OxygenLvl)
                .HasForeignKey(p => p.OxygenLvlId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<OxygenLvlOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.OxygenLvl)
                .HasForeignKey(p => p.OxygenLvlId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
