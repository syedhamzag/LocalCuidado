using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class ClientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(maxLength: 50, nullable: false),
                    Middlename = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    About = table.Column<string>(maxLength: 255, nullable: false),
                    Hobbies = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Keyworker = table.Column<string>(maxLength: 50, nullable: false),
                    IdNumber = table.Column<string>(maxLength: 50, nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    NumberOfCalls = table.Column<int>(nullable: false),
                    AreaCodeId = table.Column<int>(nullable: false),
                    TeritoryId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    ProvisionVenue = table.Column<string>(maxLength: 50, nullable: false),
                    PostCode = table.Column<string>(maxLength: 50, nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    TeamLeader = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<string>(maxLength: 15, nullable: false),
                    Telephone = table.Column<string>(maxLength: 50, nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    KeySafe = table.Column<string>(maxLength: 50, nullable: false),
                    ChoiceOfStaffId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    CapacityId = table.Column<int>(nullable: false),
                    ProviderReference = table.Column<string>(maxLength: 50, nullable: false),
                    NumberOfStaff = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client", x => x.ClientId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Client");
        }
    }
}
