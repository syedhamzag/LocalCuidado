using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Table_ClientRotaDays_Fk_Change_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tbl_ClientRotaDays",
                newName: "ClientRotaDaysId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientRotaDaysId",
                table: "tbl_ClientRotaDays",
                newName: "Id");
        }
    }
}
