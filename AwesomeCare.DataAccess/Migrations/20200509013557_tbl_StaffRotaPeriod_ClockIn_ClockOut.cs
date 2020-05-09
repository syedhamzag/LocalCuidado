using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRotaPeriod_ClockIn_ClockOut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClockInTime",
                table: "tbl_StaffRotaPeriod",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockOutTime",
                table: "tbl_StaffRotaPeriod",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockInTime",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "ClockOutTime",
                table: "tbl_StaffRotaPeriod");
        }
    }
}
