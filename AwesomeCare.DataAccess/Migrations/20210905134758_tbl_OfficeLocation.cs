using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_OfficeLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_InfectionControl",
                columns: table => new
                {
                    InfectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Cleaning = table.Column<int>(nullable: false),
                    CleaningFreq = table.Column<string>(nullable: false),
                    CleaningTools = table.Column<DateTime>(nullable: false),
                    DesiredCleaning = table.Column<int>(nullable: false),
                    DirtyLaundry = table.Column<string>(nullable: false),
                    DryLaundry = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_InfectionControl", x => x.InfectionId);
                    table.ForeignKey(
                        name: "FK_tbl_InfectionControl_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OfficeLocation",
                columns: table => new
                {
                    OfficeLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueId = table.Column<string>(maxLength: 15, nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    Latitude = table.Column<string>(maxLength: 255, nullable: true),
                    Longitude = table.Column<string>(maxLength: 255, nullable: true),
                    ContactPersonFullName = table.Column<string>(maxLength: 255, nullable: true),
                    ContactPersonEmail = table.Column<string>(maxLength: 255, nullable: true),
                    ContactPersonPhoneNumber = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OfficeLocation", x => x.OfficeLocationId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PersonalHygiene",
                columns: table => new
                {
                    HygieneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Cleaning = table.Column<int>(nullable: false),
                    CleaningTools = table.Column<int>(nullable: false),
                    WhoClean = table.Column<int>(nullable: false),
                    DesiredCleaning = table.Column<int>(nullable: false),
                    CleaningFreq = table.Column<int>(nullable: false),
                    GeneralAppliance = table.Column<int>(nullable: false),
                    DirtyLaundry = table.Column<int>(nullable: false),
                    DryLaundry = table.Column<int>(nullable: false),
                    WashingMachine = table.Column<int>(nullable: false),
                    Ironing = table.Column<int>(nullable: false),
                    LaundryGuide = table.Column<string>(nullable: false),
                    LaundrySupport = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PersonalHygiene", x => x.HygieneId);
                    table.ForeignKey(
                        name: "FK_tbl_PersonalHygiene_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_InfectionControl_ClientId",
                table: "tbl_InfectionControl",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PersonalHygiene_ClientId",
                table: "tbl_PersonalHygiene",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_InfectionControl");

            migrationBuilder.DropTable(
                name: "tbl_OfficeLocation");

            migrationBuilder.DropTable(
                name: "tbl_PersonalHygiene");
        }
    }
}
