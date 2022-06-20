using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class UpdateClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Aid",
                table: "tbl_Client",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientManager",
                table: "tbl_Client",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Denture",
                table: "tbl_Client",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aid",
                table: "tbl_Client");

            migrationBuilder.DropColumn(
                name: "ClientManager",
                table: "tbl_Client");

            migrationBuilder.DropColumn(
                name: "Denture",
                table: "tbl_Client");
        }
    }
}
