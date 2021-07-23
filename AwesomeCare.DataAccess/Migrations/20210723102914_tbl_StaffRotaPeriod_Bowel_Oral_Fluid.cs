using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRotaPeriod_Bowel_Oral_Fluid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BowelMovement",
                table: "tbl_StaffRotaPeriod",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FluidIntake",
                table: "tbl_StaffRotaPeriod",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OralCare",
                table: "tbl_StaffRotaPeriod",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BowelMovement",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "FluidIntake",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "OralCare",
                table: "tbl_StaffRotaPeriod");
        }
    }
}
