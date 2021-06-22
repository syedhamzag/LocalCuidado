using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientBMIChartMap : IEntityTypeConfiguration<ClientBMIChart>
    {
        public void Configure(EntityTypeBuilder<ClientBMIChart> builder)
        {
            builder.ToTable("tbl_ClientBMIChart");
            builder.HasKey(k => k.BMIChartId);

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

            builder.Property(p => p.Height)
               .HasColumnName("Height")
               .IsRequired();

            builder.Property(p => p.Weight)
               .HasColumnName("Weight")
               .IsRequired();

            builder.Property(p => p.NumberRange)
               .HasColumnName("NumberRange")
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
                 .WithMany(p => p.ClientBMIChart)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<BMIChartPhysician>(p => p.Physician)
                .WithOne(p => p.BMIChart)
                .HasForeignKey(p => p.BMIChartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<BMIChartStaffName>(p => p.StaffName)
                .WithOne(p => p.BMIChart)
                .HasForeignKey(p => p.BMIChartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<BMIChartOfficerToAct>(p => p.OfficerToAct)
                .WithOne(p => p.BMIChart)
                .HasForeignKey(p => p.BMIChartId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
