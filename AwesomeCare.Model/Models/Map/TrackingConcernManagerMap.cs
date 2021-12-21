using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class TrackingConcernManagerMap : IEntityTypeConfiguration<TrackingConcernManager>
    {
        public void Configure(EntityTypeBuilder<TrackingConcernManager> builder)
        {
            builder.ToTable("tbl_TrackingConcernManager");
            builder.HasKey(k => k.TrackingConcernManagerId);

            #region Properties
            builder.Property(p => p.TrackingConcernManagerId)
               .HasColumnName("TrackingConcernManagerId")
               .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.TrackingConcernNoteId)
             .HasColumnName("TrackingConcernNoteId")
             .IsRequired();

            #endregion
        }
    }
}
