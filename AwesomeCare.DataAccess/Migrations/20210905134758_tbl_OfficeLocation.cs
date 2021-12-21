using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_OfficeLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "tbl_OfficeLocation");
        }
    }
}