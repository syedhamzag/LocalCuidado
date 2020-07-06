using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffIncidentReporting_LoggedById_LoggedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoggedById",
                table: "tbl_StaffIncidentReporting",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LoggedDate",
                table: "tbl_StaffIncidentReporting",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedById",
                table: "tbl_StaffIncidentReporting");

            migrationBuilder.DropColumn(
                name: "LoggedDate",
                table: "tbl_StaffIncidentReporting");
        }
    }
}
