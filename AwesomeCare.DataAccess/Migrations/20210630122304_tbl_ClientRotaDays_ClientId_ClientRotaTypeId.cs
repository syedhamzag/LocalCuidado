using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientRotaDays_ClientId_ClientRotaTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "tbl_ClientRotaDays",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientRotaTypeId",
                table: "tbl_ClientRotaDays",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "tbl_ClientRotaDays");

            migrationBuilder.DropColumn(
                name: "ClientRotaTypeId",
                table: "tbl_ClientRotaDays");
        }
    }
}
