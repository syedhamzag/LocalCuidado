using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class HospitalEntryUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "tbl_HospitalEntry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HospitalEntry_ClientId",
                table: "tbl_HospitalEntry",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_HospitalEntry_tbl_Client_ClientId",
                table: "tbl_HospitalEntry",
                column: "ClientId",
                principalTable: "tbl_Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_HospitalEntry_tbl_Client_ClientId",
                table: "tbl_HospitalEntry");

            migrationBuilder.DropIndex(
                name: "IX_tbl_HospitalEntry_ClientId",
                table: "tbl_HospitalEntry");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "tbl_HospitalEntry");
        }
    }
}
