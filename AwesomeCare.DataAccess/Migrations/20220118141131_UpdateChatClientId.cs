using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class UpdateChatClientId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ClientChat_tbl_Client_SenderId",
                table: "tbl_ClientChat");

            migrationBuilder.DropIndex(
                name: "IX_tbl_ClientChat_SenderId",
                table: "tbl_ClientChat");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientChat_ReceiverId",
                table: "tbl_ClientChat",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ClientChat_tbl_Client_ReceiverId",
                table: "tbl_ClientChat",
                column: "ReceiverId",
                principalTable: "tbl_Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ClientChat_tbl_Client_ReceiverId",
                table: "tbl_ClientChat");

            migrationBuilder.DropIndex(
                name: "IX_tbl_ClientChat_ReceiverId",
                table: "tbl_ClientChat");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientChat_SenderId",
                table: "tbl_ClientChat",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ClientChat_tbl_Client_SenderId",
                table: "tbl_ClientChat",
                column: "SenderId",
                principalTable: "tbl_Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
