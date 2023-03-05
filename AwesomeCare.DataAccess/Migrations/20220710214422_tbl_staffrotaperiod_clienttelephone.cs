using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_staffrotaperiod_clienttelephone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClockInClientTelephone",
                table: "tbl_StaffRotaPeriod",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockOutClientTelephone",
                table: "tbl_StaffRotaPeriod",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockInClientTelephone",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "ClockOutClientTelephone",
                table: "tbl_StaffRotaPeriod");
        }
    }
}
