﻿// <auto-generated />
using System;
using AwesomeCare.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AwesomeCare.DataAccess.Migrations
{
    [DbContext(typeof(AwesomeCareDbContext))]
    [Migration("20200102170131_tbl_ClientCareDetails")]
    partial class tbl_ClientCareDetails
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AwesomeCare.Model.Models.BaseRecordItemModel", b =>
                {
                    b.Property<int>("BaseRecordItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BaseRecordItemId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BaseRecordId")
                        .HasColumnName("BaseRecordId");

                    b.Property<bool>("Deleted")
                        .HasColumnName("Deleted");

                    b.Property<string>("ValueName")
                        .IsRequired()
                        .HasColumnName("ValueName")
                        .HasMaxLength(225);

                    b.HasKey("BaseRecordItemId");

                    b.HasIndex("BaseRecordId");

                    b.ToTable("tbl_BaseRecordItem");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.BaseRecordModel", b =>
                {
                    b.Property<int>("BaseRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BaseRecordId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnName("Description")
                        .HasMaxLength(255);

                    b.Property<string>("KeyName")
                        .IsRequired()
                        .HasColumnName("KeyName")
                        .HasMaxLength(50);

                    b.HasKey("BaseRecordId");

                    b.ToTable("tbl_BaseRecord");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnName("About")
                        .HasMaxLength(255);

                    b.Property<int>("AreaCodeId")
                        .HasColumnName("AreaCodeId");

                    b.Property<int>("CapacityId")
                        .HasColumnName("CapacityId");

                    b.Property<int>("ChoiceOfStaffId")
                        .HasColumnName("ChoiceOfStaffId");

                    b.Property<string>("DateOfBirth")
                        .IsRequired()
                        .HasColumnName("DateOfBirth")
                        .HasMaxLength(15);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnName("EndDate");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnName("Firstname")
                        .HasMaxLength(50);

                    b.Property<int>("GenderId")
                        .HasColumnName("GenderId");

                    b.Property<string>("Hobbies")
                        .IsRequired()
                        .HasColumnName("Hobbies")
                        .HasMaxLength(255);

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnName("IdNumber")
                        .HasMaxLength(50);

                    b.Property<string>("KeySafe")
                        .IsRequired()
                        .HasColumnName("KeySafe")
                        .HasMaxLength(50);

                    b.Property<string>("Keyworker")
                        .IsRequired()
                        .HasColumnName("Keyworker")
                        .HasMaxLength(50);

                    b.Property<int>("LanguageId")
                        .HasColumnName("LanguageId");

                    b.Property<string>("Middlename")
                        .IsRequired()
                        .HasColumnName("Middlename")
                        .HasMaxLength(50);

                    b.Property<int>("NumberOfCalls")
                        .HasColumnName("NumberOfCalls");

                    b.Property<int>("NumberOfStaff")
                        .HasColumnName("NumberOfStaff");

                    b.Property<string>("PassportFilePath")
                        .HasColumnName("PassportFilePath");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnName("PostCode")
                        .HasMaxLength(50);

                    b.Property<string>("ProviderReference")
                        .IsRequired()
                        .HasColumnName("ProviderReference")
                        .HasMaxLength(50);

                    b.Property<string>("ProvisionVenue")
                        .IsRequired()
                        .HasColumnName("ProvisionVenue")
                        .HasMaxLength(50);

                    b.Property<decimal>("Rate")
                        .HasColumnName("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ServiceId")
                        .HasColumnName("ServiceId");

                    b.Property<DateTime>("StartDate")
                        .HasColumnName("StartDate");

                    b.Property<int>("StatusId")
                        .HasColumnName("StatusId");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnName("Surname")
                        .HasMaxLength(50);

                    b.Property<string>("TeamLeader")
                        .IsRequired()
                        .HasColumnName("TeamLeader")
                        .HasMaxLength(50);

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnName("Telephone")
                        .HasMaxLength(50);

                    b.Property<int>("TeritoryId")
                        .HasColumnName("TeritoryId");

                    b.Property<string>("UniqueId")
                        .IsRequired()
                        .HasColumnName("UniqueId");

                    b.HasKey("ClientId");

                    b.HasIndex("IdNumber")
                        .IsUnique();

                    b.ToTable("tbl_Client");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientCareDetails", b =>
                {
                    b.Property<int>("ClientCareDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientCareDetailsId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientCareDetailsTaskId")
                        .HasColumnName("ClientCareDetailsTaskId");

                    b.Property<int>("ClientId")
                        .HasColumnName("ClientId");

                    b.Property<string>("Description")
                        .HasColumnName("Description")
                        .HasMaxLength(250);

                    b.Property<string>("Location")
                        .HasColumnName("Location")
                        .HasMaxLength(250);

                    b.Property<string>("Mitigation")
                        .HasColumnName("Mitigation")
                        .HasMaxLength(250);

                    b.Property<string>("Remark")
                        .HasColumnName("Remark")
                        .HasMaxLength(250);

                    b.Property<string>("Risk")
                        .IsRequired()
                        .HasColumnName("Risk")
                        .HasMaxLength(250);

                    b.HasKey("ClientCareDetailsId");

                    b.HasIndex("ClientCareDetailsTaskId");

                    b.HasIndex("ClientId");

                    b.ToTable("tbl_ClientCareDetails");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientCareDetailsHeading", b =>
                {
                    b.Property<int>("ClientCareDetailsHeadingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientCareDetailsHeadingId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasColumnName("Heading")
                        .HasMaxLength(225);

                    b.HasKey("ClientCareDetailsHeadingId");

                    b.HasIndex("Heading")
                        .IsUnique()
                        .HasName("IX_tbl_ClientCareDetailsHeading_Heading");

                    b.ToTable("tbl_ClientCareDetailsHeading");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientCareDetailsTask", b =>
                {
                    b.Property<int>("ClientCareDetailsTaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientCareDetailsTaskId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientCareDetailsHeadingId")
                        .HasColumnName("ClientCareDetailsHeadingId");

                    b.Property<string>("Task")
                        .IsRequired()
                        .HasColumnName("Task")
                        .HasMaxLength(225);

                    b.HasKey("ClientCareDetailsTaskId");

                    b.HasIndex("ClientCareDetailsHeadingId");

                    b.ToTable("tbl_ClientCareDetailsTask");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientInvolvingParty", b =>
                {
                    b.Property<int>("ClientInvolvingPartyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientInvolvingPartyId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnName("Address")
                        .HasMaxLength(225);

                    b.Property<int>("ClientId")
                        .HasColumnName("ClientId");

                    b.Property<int>("ClientInvolvingPartyItemId")
                        .HasColumnName("ClientInvolvingPartyItemId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasMaxLength(125);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasMaxLength(50);

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasColumnName("Relationship")
                        .HasMaxLength(50);

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnName("Telephone")
                        .HasMaxLength(50);

                    b.HasKey("ClientInvolvingPartyId");

                    b.HasIndex("ClientId");

                    b.HasIndex("ClientInvolvingPartyItemId");

                    b.ToTable("tbl_ClientInvolvingParty");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientInvolvingPartyItem", b =>
                {
                    b.Property<int>("ClientInvolvingPartyItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientInvolvingPartyItemId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnName("Deleted");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasMaxLength(225);

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnName("ItemName")
                        .HasMaxLength(100);

                    b.HasKey("ClientInvolvingPartyItemId");

                    b.HasIndex("ItemName")
                        .IsUnique()
                        .HasName("IX_tbl_ClientInvolvingPartyItem_ItemName");

                    b.ToTable("tbl_ClientInvolvingPartyItem");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientRegulatoryContact", b =>
                {
                    b.Property<int>("ClientRegulatoryContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientRegulatoryContactId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BaseRecordItemId")
                        .HasColumnName("BaseRecordItemId");

                    b.Property<int>("ClientId")
                        .HasColumnName("ClientId");

                    b.Property<DateTime?>("DatePerformed")
                        .HasColumnName("DatePerformed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnName("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Evidence")
                        .HasColumnName("Evidence");

                    b.HasKey("ClientRegulatoryContactId");

                    b.HasIndex("BaseRecordItemId");

                    b.HasIndex("ClientId");

                    b.ToTable("tbl_ClientRegulatoryContact");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientRota", b =>
                {
                    b.Property<int>("ClientRotaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientRotaId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnName("ClientId");

                    b.Property<int>("ClientRotaTypeId")
                        .HasColumnName("ClientRotaTypeId");

                    b.HasKey("ClientRotaId");

                    b.HasIndex("ClientId");

                    b.HasIndex("ClientRotaTypeId");

                    b.ToTable("tbl_ClientRota");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientRotaDays", b =>
                {
                    b.Property<int>("ClientRotaDaysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientRotaDaysId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientRotaId")
                        .HasColumnName("ClientRotaId");

                    b.Property<int>("RotaDayofWeekId")
                        .HasColumnName("RotaDayofWeekId");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnName("StartTime")
                        .HasMaxLength(25);

                    b.Property<string>("StopTime")
                        .IsRequired()
                        .HasColumnName("StopTime")
                        .HasMaxLength(25);

                    b.HasKey("ClientRotaDaysId");

                    b.HasIndex("ClientRotaId");

                    b.HasIndex("RotaDayofWeekId");

                    b.ToTable("tbl_ClientRotaDays");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientRotaTask", b =>
                {
                    b.Property<int>("ClientRotaTaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientRotaTaskId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientRotaDaysId")
                        .HasColumnName("ClientRotaDaysId");

                    b.Property<int>("RotaTaskId")
                        .HasColumnName("RotaTaskId");

                    b.HasKey("ClientRotaTaskId");

                    b.HasIndex("ClientRotaDaysId");

                    b.HasIndex("RotaTaskId");

                    b.ToTable("tbl_ClientRotaTask");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientRotaType", b =>
                {
                    b.Property<int>("ClientRotaTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientRotaTypeId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnName("Deleted");

                    b.Property<string>("RotaType")
                        .IsRequired()
                        .HasColumnName("RotaType")
                        .HasMaxLength(15);

                    b.HasKey("ClientRotaTypeId");

                    b.HasIndex("RotaType")
                        .IsUnique()
                        .HasName("IX_tbl_ClientRotaType_RotaType");

                    b.ToTable("tbl_ClientRotaType");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.CompanyContactModel", b =>
                {
                    b.Property<int>("CompanyContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CompanyContactId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId")
                        .HasColumnName("CompanyId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("ContactEmail")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("ContactName")
                        .HasMaxLength(255);

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnName("ContactTelephone")
                        .HasMaxLength(255);

                    b.HasKey("CompanyContactId");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("tbl_CompanyContact");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.CompanyModel", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CompanyId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnName("Address")
                        .HasMaxLength(255);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnName("CompanyName")
                        .HasMaxLength(255);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasMaxLength(255);

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnName("Language")
                        .HasMaxLength(255);

                    b.Property<string>("LogoUrl")
                        .HasColumnName("LogoUrl");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnName("Website")
                        .HasMaxLength(255);

                    b.HasKey("CompanyId");

                    b.ToTable("tbl_Company");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.Rota", b =>
                {
                    b.Property<int>("RotaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Area")
                        .HasColumnName("Area")
                        .HasMaxLength(225);

                    b.Property<bool>("Deleted")
                        .HasColumnName("Deleted");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnName("Gender")
                        .HasMaxLength(15);

                    b.Property<int>("NumberOfStaff")
                        .HasColumnName("NumberOfStaff");

                    b.Property<string>("RotaName")
                        .IsRequired()
                        .HasColumnName("RotaName")
                        .HasMaxLength(225);

                    b.HasKey("RotaId");

                    b.ToTable("tbl_ClientRotaName");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.RotaDayofWeek", b =>
                {
                    b.Property<int>("RotaDayofWeekId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RotaDayofWeekId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DayofWeek")
                        .IsRequired()
                        .HasColumnName("DayofWeek")
                        .HasMaxLength(15);

                    b.Property<bool>("Deleted")
                        .HasColumnName("Deleted");

                    b.HasKey("RotaDayofWeekId");

                    b.ToTable("tbl_RotaDayofWeek");

                    b.HasData(
                        new { RotaDayofWeekId = 1, DayofWeek = "Monday", Deleted = false },
                        new { RotaDayofWeekId = 2, DayofWeek = "Tuesday", Deleted = false },
                        new { RotaDayofWeekId = 3, DayofWeek = "Wednesday", Deleted = false },
                        new { RotaDayofWeekId = 4, DayofWeek = "Thursday", Deleted = false },
                        new { RotaDayofWeekId = 5, DayofWeek = "Friday", Deleted = false },
                        new { RotaDayofWeekId = 6, DayofWeek = "Saturday", Deleted = false },
                        new { RotaDayofWeekId = 7, DayofWeek = "Sunday", Deleted = false }
                    );
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.RotaTask", b =>
                {
                    b.Property<int>("RotaTaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RotaTaskId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnName("Deleted");

                    b.Property<string>("GivenAcronym")
                        .IsRequired()
                        .HasColumnName("GivenAcronym")
                        .HasMaxLength(50);

                    b.Property<string>("NotGivenAcronym")
                        .IsRequired()
                        .HasColumnName("NotGivenAcronym")
                        .HasMaxLength(50);

                    b.Property<string>("Remark")
                        .HasColumnName("Remark")
                        .HasMaxLength(125);

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnName("TaskName")
                        .HasMaxLength(125);

                    b.HasKey("RotaTaskId");

                    b.HasIndex("GivenAcronym")
                        .IsUnique()
                        .HasName("IX_tbl_RotaTask_GivenAcronym");

                    b.HasIndex("NotGivenAcronym")
                        .IsUnique()
                        .HasName("IX_tbl_RotaTask_NotGivenAcronym");

                    b.HasIndex("TaskName")
                        .IsUnique()
                        .HasName("IX_tbl_RotaTask_TaskName");

                    b.ToTable("tbl_RotaTask");
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.BaseRecordItemModel", b =>
                {
                    b.HasOne("AwesomeCare.Model.Models.BaseRecordModel", "BaseRecord")
                        .WithMany("BaseRecordItems")
                        .HasForeignKey("BaseRecordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientCareDetails", b =>
                {
                    b.HasOne("AwesomeCare.Model.Models.ClientCareDetailsTask", "ClientCareDetailsTask")
                        .WithMany("ClientCareDetails")
                        .HasForeignKey("ClientCareDetailsTaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AwesomeCare.Model.Models.Client", "Client")
                        .WithMany("ClientCareDetails")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientCareDetailsTask", b =>
                {
                    b.HasOne("AwesomeCare.Model.Models.ClientCareDetailsHeading", "ClientCareDetailsHeading")
                        .WithMany("ClientCareDetailsTasks")
                        .HasForeignKey("ClientCareDetailsHeadingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientInvolvingParty", b =>
                {
                    b.HasOne("AwesomeCare.Model.Models.Client", "Client")
                        .WithMany("InvolvingParties")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AwesomeCare.Model.Models.ClientInvolvingPartyItem", "ClientInvolvingPartyItem")
                        .WithMany("ClientInvolvingParty")
                        .HasForeignKey("ClientInvolvingPartyItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientRegulatoryContact", b =>
                {
                    b.HasOne("AwesomeCare.Model.Models.BaseRecordItemModel", "BaseRecordItem")
                        .WithMany()
                        .HasForeignKey("BaseRecordItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AwesomeCare.Model.Models.Client", "Client")
                        .WithMany("RegulatoryContact")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientRota", b =>
                {
                    b.HasOne("AwesomeCare.Model.Models.Client", "Client")
                        .WithMany("ClientRota")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AwesomeCare.Model.Models.ClientRotaType", "ClientRotaType")
                        .WithMany("ClientRota")
                        .HasForeignKey("ClientRotaTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientRotaDays", b =>
                {
                    b.HasOne("AwesomeCare.Model.Models.ClientRota", "ClientRota")
                        .WithMany("ClientRotaDays")
                        .HasForeignKey("ClientRotaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AwesomeCare.Model.Models.RotaDayofWeek", "RotaDayofWeek")
                        .WithMany("ClientRotaDays")
                        .HasForeignKey("RotaDayofWeekId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.ClientRotaTask", b =>
                {
                    b.HasOne("AwesomeCare.Model.Models.ClientRotaDays", "ClientRotaDays")
                        .WithMany("ClientRotaTask")
                        .HasForeignKey("ClientRotaDaysId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AwesomeCare.Model.Models.RotaTask", "RotaTask")
                        .WithMany("ClientRotaTask")
                        .HasForeignKey("RotaTaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AwesomeCare.Model.Models.CompanyContactModel", b =>
                {
                    b.HasOne("AwesomeCare.Model.Models.CompanyModel", "Company")
                        .WithOne("CompanyContact")
                        .HasForeignKey("AwesomeCare.Model.Models.CompanyContactModel", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
