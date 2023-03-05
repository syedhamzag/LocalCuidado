using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffLatenessMap : IEntityTypeConfiguration<StaffLateness>
    {
        public void Configure(EntityTypeBuilder<StaffLateness> builder)
        {
            builder.ToTable("tbl_StaffLateness");
            builder.HasKey(k => k.StaffLatenessId);

            #region Properties
            builder.Property(p => p.StaffPersonalInfoId)
              .HasColumnName("StaffPersonalInfoId")
              .IsRequired();

            builder.Property(p => p.SN)
              .HasColumnName("SN")
              .IsRequired();

            builder.Property(p => p.Date)
             .HasColumnName("Date")
             .IsRequired();

            builder.Property(p => p.Rota)
             .HasColumnName("Rota")
             .IsRequired();

            builder.Property(p => p.TimeCritical)
             .HasColumnName("TimeCritical")
             .IsRequired();

            builder.Property(p => p.Reason)
             .HasColumnName("Reason")
             .IsRequired();

            builder.Property(p => p.Response)
             .HasColumnName("Response")
             .IsRequired();

            builder.Property(p => p.Status)
             .HasColumnName("Status")
             .IsRequired();

            #endregion

            #region Relationship
            builder.HasOne(p => p.StaffPersonalInfo)
                    .WithMany(m => m.StaffLateness)
                    .HasForeignKey(f => f.StaffPersonalInfoId)
                    .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
