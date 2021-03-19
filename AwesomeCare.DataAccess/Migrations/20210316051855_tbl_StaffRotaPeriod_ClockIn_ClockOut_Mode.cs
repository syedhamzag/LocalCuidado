using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRotaPeriod_ClockIn_ClockOut_Mode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClockInMode",
                table: "tbl_StaffRotaPeriod",
                maxLength: 225,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockOutMode",
                table: "tbl_StaffRotaPeriod",
                maxLength: 225,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockInMode",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "ClockOutMode",
                table: "tbl_StaffRotaPeriod");
        }
    }
}
