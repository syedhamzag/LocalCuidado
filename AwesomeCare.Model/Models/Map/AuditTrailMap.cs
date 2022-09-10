using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class AuditTrailMap : IEntityTypeConfiguration<AuditTrail>
    {
        public void Configure(EntityTypeBuilder<AuditTrail> builder)
        {
            builder.ToTable("tbl_AuditTrail");
            builder.HasKey(k => k.Id);

            #region Properties
            builder.Property(p => p.UserId)
               .HasColumnName("UserId")
               .IsRequired();

            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Date)
               .HasColumnName("Date")
               .IsRequired();

            builder.Property(p => p.UserName)
               .HasColumnName("UserName")
               .IsRequired();

            builder.Property(p => p.Email)
               .HasColumnName("Email")
               .IsRequired();

            builder.Property(p => p.Duration)
               .HasColumnName("Duration")
               .IsRequired();


            #endregion

            #region Relationship
            builder.HasOne(p => p.Client)
                 .WithMany(p => p.AuditTrail)
                 .HasForeignKey(p => p.ClientId)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
