using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRotaPeriod_ColumnRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickInAddress",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "ClickOutAddress",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.AddColumn<string>(
                name: "ClockInAddress",
                table: "tbl_StaffRotaPeriod",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockOutAddress",
                table: "tbl_StaffRotaPeriod",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockInAddress",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "ClockOutAddress",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.AddColumn<string>(
                name: "ClickInAddress",
                table: "tbl_StaffRotaPeriod",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClickOutAddress",
                table: "tbl_StaffRotaPeriod",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
