using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class ClientRota_Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_ClientRota",
                table: "tbl_ClientRota");

            migrationBuilder.RenameTable(
                name: "tbl_ClientRota",
                newName: "tbl_ClientRotaName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_ClientRotaName",
                table: "tbl_ClientRotaName",
                column: "RotaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_ClientRotaName",
                table: "tbl_ClientRotaName");

            migrationBuilder.RenameTable(
                name: "tbl_ClientRotaName",
                newName: "tbl_ClientRota");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_ClientRota",
                table: "tbl_ClientRota",
                column: "RotaId");
        }
    }
}
