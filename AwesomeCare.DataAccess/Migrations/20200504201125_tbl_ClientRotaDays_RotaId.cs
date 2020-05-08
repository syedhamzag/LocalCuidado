using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientRotaDays_RotaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RotaId",
                table: "tbl_ClientRotaDays",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientRotaDays_RotaId",
                table: "tbl_ClientRotaDays",
                column: "RotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ClientRotaDays_tbl_ClientRotaName_RotaId",
                table: "tbl_ClientRotaDays",
                column: "RotaId",
                principalTable: "tbl_ClientRotaName",
                principalColumn: "RotaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ClientRotaDays_tbl_ClientRotaName_RotaId",
                table: "tbl_ClientRotaDays");

            migrationBuilder.DropIndex(
                name: "IX_tbl_ClientRotaDays_RotaId",
                table: "tbl_ClientRotaDays");

            migrationBuilder.DropColumn(
                name: "RotaId",
                table: "tbl_ClientRotaDays");
        }
    }
}
