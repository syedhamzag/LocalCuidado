using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class StaffPersonalInfoMap : IEntityTypeConfiguration<StaffPersonalInfo>
    {
        public void Configure(EntityTypeBuilder<StaffPersonalInfo> builder)
        {
            builder.ToTable("tbl_StaffPersonalInfo");
            builder.HasKey(k => k.StaffPersonalInfoId);

            #region Properties
          
            builder.Property(p => p.StaffPersonalInfoId)
                .HasColumnName("StaffPersonalInfoId")
                .IsRequired(true);

            builder.Property(p => p.RegistrationId)
               .HasColumnName("RegistrationId")
               .HasMaxLength(20)
               .IsRequired(false);

            builder.Property(p => p.FirstName)
               .HasColumnName("FirstName")
               .HasMaxLength(50)
               .IsRequired(true);

            builder.Property(p => p.MiddleName)
               .HasColumnName("MiddleName")
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(p => p.LastName)
               .HasColumnName("LastName")
               .HasMaxLength(50)
               .IsRequired(true);

            builder.Property(p => p.DateOfBirth)
               .HasColumnName("DateOfBirth")
               .HasMaxLength(50)
               .IsRequired(true);

            builder.Property(p => p.Telephone)
               .HasColumnName("Telephone")
               .HasMaxLength(50)
               .IsRequired(true);

            builder.Property(p => p.ProfilePix)
               .HasColumnName("ProfilePix")
               .IsRequired(true);

            builder.Property(p => p.Address)
               .HasColumnName("Address")
               .HasMaxLength(225)
               .IsRequired(true);

            builder.Property(p => p.AboutMe)
               .HasColumnName("AboutMe")
               .HasMaxLength(225)
               .IsRequired(false);

            builder.Property(p => p.Hobbies)
               .HasColumnName("Hobbies")
               .HasMaxLength(225)
               .IsRequired(false);

            builder.Property(p => p.Email)
               .HasColumnName("Email")
               .HasMaxLength(225)
               .IsRequired(true);

            builder.Property(p => p.StartDate)
               .HasColumnName("StartDate")
               .IsRequired(true);

            builder.Property(p => p.EndDate)
               .HasColumnName("EndDate")
               .IsRequired(false);

            builder.Property(p => p.Keyworker)
               .HasColumnName("Keyworker")
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(p => p.IdNumber)
               .HasColumnName("IdNumber")
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(p => p.Gender)
               .HasColumnName("Gender")
               .HasMaxLength(7)
               .IsRequired(true);

            builder.Property(p => p.PostCode)
               .HasColumnName("PostCode")
               .HasMaxLength(50)
               .IsRequired(true);

            builder.Property(p => p.Rate)
              .HasColumnName("Rate")
              .IsRequired(false);

            builder.Property(p => p.TeamLeader)
               .HasColumnName("TeamLeader")
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(p => p.WorkTeam)
               .HasColumnName("WorkTeam")
               .HasMaxLength(50)
               .IsRequired(false);

            builder.Property(p => p.StaffWorkTeamId)
              .HasColumnName("StaffWorkTeamId")
              .IsRequired(false);


            builder.Property(p => p.Passcode)
              .HasColumnName("Passcode")
              .HasMaxLength(15)
              .IsRequired(false);

            builder.Property(p => p.CanDrive)
               .HasColumnName("CanDrive")
                .HasMaxLength(3)
               .IsRequired(true);

            builder.Property(p => p.DrivingLicense)
               .HasColumnName("DrivingLicense")
               .IsRequired(false);

            builder.Property(p => p.DrivingLicenseExpiryDate)
              .HasColumnName("DrivingLicenseExpiryDate")
              .IsRequired(false);

            builder.Property(p => p.RightToWork)
               .HasColumnName("RightToWork")
               .HasMaxLength(3)
               .IsRequired(true);

            builder.Property(p => p.RightToWorkAttachment)
              .HasColumnName("RightToWorkAttachment")
              .IsRequired(false);

            builder.Property(p => p.RightToWorkExpiryDate)
               .HasColumnName("RightToWorkExpiryDate")
               .IsRequired(false);

            builder.Property(p => p.DBS)
              .HasColumnName("DBS")
               .HasMaxLength(3)
              .IsRequired(true);

            builder.Property(p => p.DBSAttachment)
               .HasColumnName("DBSAttachment")
               .IsRequired(false);

            builder.Property(p => p.DBSExpiryDate)
              .HasColumnName("DBSExpiryDate")
              .IsRequired(false);

            builder.Property(p => p.DBSUpdateNo)
               .HasColumnName("DBSUpdateNo")
                .HasMaxLength(50)
               .IsRequired(false);
            
            builder.Property(p => p.NI)
              .HasColumnName("NI")
              .HasMaxLength(3)
              .IsRequired(true);

            builder.Property(p => p.NIAttachment)
              .HasColumnName("NIAttachment")
              .IsRequired(false);

            builder.Property(p => p.NIExpiryDate)
              .HasColumnName("NIExpiryDate")
              .IsRequired(false);

            builder.Property(p => p.CV)
              .HasColumnName("CV")
              .IsRequired(true);

            builder.Property(p => p.CoverLetter)
              .HasColumnName("CoverLetter")
              .IsRequired(true);

            builder.Property(p => p.Self_PYE)
            .HasColumnName("Self_PYE")
            .HasMaxLength(3)
            .IsRequired(true);

            builder.Property(p => p.Self_PYEAttachment)
            .HasColumnName("Self_PYEAttachment")
            .IsRequired(false);

            builder.Property(p => p.Status)
                .HasColumnName("Status")
                .IsRequired();

            #endregion
            builder.HasIndex(p => p.RegistrationId)
                     .HasName("IX_tbl_StaffPersonalInfo_RegistrationId")
                     .IsUnique(true);
        builder.HasOne<ApplicationUser>(p=>p.ApplicationUser)
                .WithOne(p=>p.StaffPersonalInfo)
                .HasForeignKey<StaffPersonalInfo>(f=>f.ApplicationUserId);
        }
    }
}
