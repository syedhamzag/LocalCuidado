using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRating_ChangeColumn_RatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatedBy",
                table: "tbl_StaffRating");

            migrationBuilder.AddColumn<int>(
                name: "SubmittedBy",
                table: "tbl_StaffRating",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedBy",
                table: "tbl_StaffRating");

            migrationBuilder.AddColumn<int>(
                name: "RatedBy",
                table: "tbl_StaffRating",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
