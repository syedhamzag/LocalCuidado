using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_staffrotaperiod_clockin_clockout_distance_count : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClockInCount",
                table: "tbl_StaffRotaPeriod",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockInDistance",
                table: "tbl_StaffRotaPeriod",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockOutCount",
                table: "tbl_StaffRotaPeriod",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockOutDistance",
                table: "tbl_StaffRotaPeriod",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockInCount",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "ClockInDistance",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "ClockOutCount",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "ClockOutDistance",
                table: "tbl_StaffRotaPeriod");
        }
    }
}
