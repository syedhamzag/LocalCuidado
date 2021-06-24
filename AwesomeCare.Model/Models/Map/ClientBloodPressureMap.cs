using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientBloodPressureMap : IEntityTypeConfiguration<ClientBloodPressure>
    {
        public void Configure(EntityTypeBuilder<ClientBloodPressure> builder)
        {
            builder.ToTable("tbl_Client_BloodPressure");
            builder.HasKey(k => k.BloodPressureId);

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

            builder.Property(p => p.GoalSystolic)
               .HasColumnName("GoalSystolic")
               .IsRequired();

            builder.Property(p => p.GoalDiastolic)
               .HasColumnName("GoalDiastolic")
               .IsRequired();

            builder.Property(p => p.ReadingSystolic)
               .HasColumnName("ReadingSystolic")
               .IsRequired();

            builder.Property(p => p.ReadingDiastolic)
               .HasColumnName("ReadingDiastolic")
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
                 .WithMany(p => p.ClientBloodPressure)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
