using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_staffrotaperiod_clockinoutaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClockInGeolocation",
                table: "tbl_StaffRotaPeriod",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockOutGeolocation",
                table: "tbl_StaffRotaPeriod",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockInGeolocation",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "ClockOutGeolocation",
                table: "tbl_StaffRotaPeriod");
        }
    }
}
