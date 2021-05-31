using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientMedication_Add_ClientId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "tbl_ClientMedication",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientMedication_ClientId",
                table: "tbl_ClientMedication",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ClientMedication_tbl_Client_ClientId",
                table: "tbl_ClientMedication",
                column: "ClientId",
                principalTable: "tbl_Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ClientMedication_tbl_Client_ClientId",
                table: "tbl_ClientMedication");

            migrationBuilder.DropIndex(
                name: "IX_tbl_ClientMedication_ClientId",
                table: "tbl_ClientMedication");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "tbl_ClientMedication");
        }
    }
}
