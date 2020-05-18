using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffRefereeMap : IEntityTypeConfiguration<StaffReferee>
    {
        public void Configure(EntityTypeBuilder<StaffReferee> builder)
        {
            builder.ToTable("tbl_StaffReferee");
            builder.HasKey(k => k.StaffRefereeId);

            #region Properties
            builder.Property(p => p.StaffRefereeId)
                .HasColumnName("StaffRefereeId")
                .IsRequired();

            builder.Property(p => p.StaffPersonalInfoId)
               .HasColumnName("StaffPersonalInfoId")
               .IsRequired();

            builder.Property(p => p.Referee)
              .HasColumnName("Referee")
              .HasMaxLength(125)
              .IsRequired();

            builder.Property(p => p.CompanyName)
             .HasColumnName("CompanyName")
             .HasMaxLength(125)
             .IsRequired();

            builder.Property(p => p.Address)
             .HasColumnName("Address")
             .HasMaxLength(255)
             .IsRequired();

            builder.Property(p => p.PhoneNumber)
             .HasColumnName("PhoneNumber")
             .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.Email)
           .HasColumnName("Email")
           .HasMaxLength(125)
           .IsRequired();


            builder.Property(p => p.PositionofReferee)
             .HasColumnName("PositionofReferee")
             .HasMaxLength(25)
             .IsRequired();

          
            builder.Property(p => p.Attachment)
             .HasColumnName("Attachment")
             .IsRequired(false);
            #endregion

            #region Relationship
            builder.HasOne<StaffPersonalInfo>(p => p.Staff)
                .WithMany(p => p.References)
                .HasForeignKey(p => p.StaffPersonalInfoId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
