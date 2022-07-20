using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class RotaPin_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "tbl_RotaPin",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "KeyworkerId",
                table: "tbl_Client",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamLeaderId",
                table: "tbl_Client",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "tbl_RotaPin");

            migrationBuilder.DropColumn(
                name: "KeyworkerId",
                table: "tbl_Client");

            migrationBuilder.DropColumn(
                name: "TeamLeaderId",
                table: "tbl_Client");
        }
    }
}
