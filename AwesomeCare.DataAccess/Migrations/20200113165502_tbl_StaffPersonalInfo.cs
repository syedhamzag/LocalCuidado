using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffPersonalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffPersonalInfo",
                columns: table => new
                {
                    StaffPersonalInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegistrationId = table.Column<string>(maxLength: 20, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(maxLength: 25, nullable: false),
                    ProfilePix = table.Column<string>(nullable: false),
                    Address = table.Column<string>(maxLength: 225, nullable: false),
                    AboutMe = table.Column<string>(maxLength: 225, nullable: true),
                    Hobbies = table.Column<string>(maxLength: 225, nullable: true),
                    Email = table.Column<string>(maxLength: 225, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Keyworker = table.Column<string>(maxLength: 50, nullable: false),
                    IdNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Gender = table.Column<string>(maxLength: 7, nullable: false),
                    PostCode = table.Column<string>(maxLength: 50, nullable: false),
                    Rate = table.Column<decimal>(nullable: true),
                    TeamLeader = table.Column<string>(maxLength: 50, nullable: true),
                    WorkTeam = table.Column<string>(maxLength: 50, nullable: true),
                    Passcode = table.Column<string>(maxLength: 15, nullable: true),
                    CanDrive = table.Column<string>(maxLength: 3, nullable: false),
                    DrivingLicense = table.Column<string>(nullable: true),
                    DrivingLicenseExpiryDate = table.Column<DateTime>(nullable: true),
                    RightToWork = table.Column<string>(maxLength: 3, nullable: false),
                    RightToWorkAttachment = table.Column<string>(nullable: true),
                    RightToWorkExpiryDate = table.Column<DateTime>(nullable: true),
                    DBS = table.Column<string>(maxLength: 3, nullable: false),
                    DBSAttachment = table.Column<string>(nullable: true),
                    DBSExpiryDate = table.Column<DateTime>(nullable: true),
                    DBSUpdateNo = table.Column<string>(maxLength: 50, nullable: true),
                    NI = table.Column<string>(maxLength: 3, nullable: false),
                    NIAttachment = table.Column<string>(nullable: true),
                    NIExpiryDate = table.Column<DateTime>(nullable: true),
                    CV = table.Column<string>(nullable: false),
                    CoverLetter = table.Column<string>(nullable: false),
                    Self_PYE = table.Column<string>(maxLength: 3, nullable: false),
                    Self_PYEAttachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffPersonalInfo", x => x.StaffPersonalInfoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffPersonalInfo_RegistrationId",
                table: "tbl_StaffPersonalInfo",
                column: "RegistrationId",
                unique: true,
                filter: "[RegistrationId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffPersonalInfo");
        }
    }
}
