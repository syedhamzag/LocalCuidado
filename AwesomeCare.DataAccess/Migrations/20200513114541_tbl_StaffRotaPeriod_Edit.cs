using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRotaPeriod_Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClickInAddress",
                table: "tbl_StaffRotaPeriod",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClickOutAddress",
                table: "tbl_StaffRotaPeriod",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "tbl_StaffRotaPeriod",
                maxLength: 225,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "tbl_StaffRotaPeriod",
                maxLength: 225,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HandOver",
                table: "tbl_StaffRotaPeriod",
                maxLength: 225,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickInAddress",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "ClickOutAddress",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "HandOver",
                table: "tbl_StaffRotaPeriod");
        }
    }
}
