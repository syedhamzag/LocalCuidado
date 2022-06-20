using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffMedTrackerMap : IEntityTypeConfiguration<StaffMedTracker>
    {
        public void Configure(EntityTypeBuilder<StaffMedTracker> builder)
        {
            builder.ToTable("tbl_StaffMedTracker");
            builder.HasKey(k => k.StaffMedTrackerId);

            builder.Property(p => p.MedTrackDate)
               .HasColumnName("MedTrackDate")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();

            builder.Property(p => p.RotaId)
               .HasColumnName("RotaId")
               .IsRequired();

            builder.Property(p => p.ClientMedId)
               .HasColumnName("ClientMedId")
               .IsRequired();

            builder.Property(p => p.DoseGiven)
               .HasColumnName("DoseGiven")
               .IsRequired();

            builder.Property(p => p.Status)
               .HasColumnName("Status")
               .IsRequired();
        }
    }
}
