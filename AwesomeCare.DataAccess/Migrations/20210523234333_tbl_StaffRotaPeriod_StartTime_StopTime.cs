using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRotaPeriod_StartTime_StopTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "tbl_StaffRotaPeriod",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StopTime",
                table: "tbl_StaffRotaPeriod",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "StopTime",
                table: "tbl_StaffRotaPeriod");
        }
    }
}
