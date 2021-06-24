using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientBodyTempMap : IEntityTypeConfiguration<ClientBodyTemp>
    {
        public void Configure(EntityTypeBuilder<ClientBodyTemp> builder)
        {
            builder.ToTable("tbl_Client_BodyTemp");
            builder.HasKey(k => k.BodyTempId);

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

            builder.Property(p => p.TargetTemp)
               .HasColumnName("TargetTemp")
               .IsRequired();

            builder.Property(p => p.TargetTempAttach)
               .HasColumnName("TargetTempAttach");

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
                 .WithMany(p => p.ClientBodyTemp)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
