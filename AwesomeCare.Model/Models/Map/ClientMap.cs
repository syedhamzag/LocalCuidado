﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models.Map
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("tbl_Client");
            builder.HasKey(k => k.ClientId);

            #region Properties
            builder.Property(p => p.ClientId)
               .HasColumnName("ClientId")
               .IsRequired();

            builder.Property(p => p.Firstname)
                .HasColumnName("Firstname")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Middlename)
               .HasColumnName("Middlename")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Surname)
               .HasColumnName("Surname")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.PreferredName)
               .HasColumnName("PreferredName")
               .HasMaxLength(50)
               .IsRequired(false);


            builder.Property(p => p.Email)
               .HasColumnName("Email")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.About)
               .HasColumnName("About")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Hobbies)
               .HasColumnName("Hobbies")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.StartDate)
               .HasColumnName("StartDate")
               .IsRequired();

            builder.Property(p => p.EndDate)
               .HasColumnName("EndDate")
               .IsRequired(false);

            builder.Property(p => p.Keyworker)
               .HasColumnName("Keyworker")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.IdNumber)
               .HasColumnName("IdNumber")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.GenderId)
               .HasColumnName("GenderId")
               .IsRequired();

            builder.Property(p => p.NumberOfCalls)
               .HasColumnName("NumberOfCalls")
               .IsRequired();

            builder.Property(p => p.AreaCodeId)
               .HasColumnName("AreaCodeId")
               .IsRequired();

            builder.Property(p => p.TeritoryId)
               .HasColumnName("TeritoryId")
               .IsRequired();

            builder.Property(p => p.ServiceId)
               .HasColumnName("ServiceId")
               .IsRequired();

            builder.Property(p => p.ProvisionVenue)
             .HasColumnName("ProvisionVenue")
             .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.PostCode)
               .HasColumnName("PostCode")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.Rate)
               .HasColumnName("Rate")
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder.Property(p => p.TeamLeader)
             .HasColumnName("TeamLeader")
             .HasMaxLength(50)
             .IsRequired();

            builder.Property(p => p.DateOfBirth)
               .HasColumnName("DateOfBirth")
               .HasMaxLength(15)
               .IsRequired();

            builder.Property(p => p.Telephone)
               .HasColumnName("Telephone")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(p => p.LanguageId)
              .HasColumnName("LanguageId")
              .IsRequired();

            builder.Property(p => p.KeySafe)
              .HasColumnName("KeySafe")
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(p => p.ChoiceOfStaffId)
              .HasColumnName("ChoiceOfStaffId")
              .IsRequired();

            builder.Property(p => p.StatusId)
              .HasColumnName("StatusId")
              .IsRequired();

            builder.Property(p => p.CapacityId)
              .HasColumnName("CapacityId")
              .IsRequired();

            builder.Property(p => p.ProviderReference)
            .HasColumnName("ProviderReference")
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(p => p.NumberOfStaff)
              .HasColumnName("NumberOfStaff")
              .IsRequired();

            builder.Property(p => p.Pin)
              .HasColumnName("Pin")
              .IsRequired();

            builder.Property(p => p.UniqueId)
              .HasColumnName("UniqueId")
              .IsRequired(false);

            builder.Property(p => p.PassportFilePath)
             .HasColumnName("PassportFilePath")
             .IsRequired(false);

            builder.Property(p => p.Address)
           .HasColumnName("Address")
           .HasMaxLength(250)
           .IsRequired(false);

            builder.Property(p => p.Latitude)
          .HasColumnName("Latitude")
          .HasMaxLength(250)
          .IsRequired(false);

            builder.Property(p => p.Longitude)
          .HasColumnName("Longitude")
          .HasMaxLength(250)
          .IsRequired(false);

            builder.Property(p => p.ClientManager)
          .HasColumnName("ClientManager")
          .HasDefaultValue(0);

            builder.Property(p => p.Denture)
          .HasColumnName("Denture")
          .HasDefaultValue(0);

            builder.Property(p => p.Aid)
          .HasColumnName("Aid")
          .HasDefaultValue(0);

            builder.Property(p => p.TeamLeaderId)
          .HasColumnName("TeamLeaderId")
          .HasDefaultValue(0);

            builder.Property(p => p.KeyworkerId)
              .HasColumnName("KeyworkerId")
              .HasDefaultValue(0);

            builder.Property(p => p.LocationDistance)
             .HasColumnName("LocationDistance")
             .HasDefaultValue(0.01);

            builder.HasMany(p => p.RegulatoryContact)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            builder.HasMany<ClientNutrition>(p => p.ClientNutrition)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
