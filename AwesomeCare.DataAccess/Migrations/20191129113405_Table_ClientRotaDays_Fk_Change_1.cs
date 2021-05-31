using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Table_ClientRotaDays_Fk_Change_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ClientRotaDays_tbl_ClientRota_ClientRotaDaysId",
                table: "tbl_ClientRotaDays");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ClientRotaTask_tbl_ClientRotaDays_ClientRotaDaysId",
                table: "tbl_ClientRotaTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_ClientRotaDays",
                table: "tbl_ClientRotaDays");

            migrationBuilder.DropColumn(
                name: "ClientRotaDaysId",
                table: "tbl_ClientRotaDays");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "tbl_ClientRotaDays",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_ClientRotaDays",
                table: "tbl_ClientRotaDays",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientRotaDays_ClientRotaId",
                table: "tbl_ClientRotaDays",
                column: "ClientRotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ClientRotaDays_tbl_ClientRota_ClientRotaId",
                table: "tbl_ClientRotaDays",
                column: "ClientRotaId",
                principalTable: "tbl_ClientRota",
                principalColumn: "ClientRotaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ClientRotaTask_tbl_ClientRotaDays_ClientRotaDaysId",
                table: "tbl_ClientRotaTask",
                column: "ClientRotaDaysId",
                principalTable: "tbl_ClientRotaDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ClientRotaDays_tbl_ClientRota_ClientRotaId",
                table: "tbl_ClientRotaDays");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ClientRotaTask_tbl_ClientRotaDays_ClientRotaDaysId",
                table: "tbl_ClientRotaTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_ClientRotaDays",
                table: "tbl_ClientRotaDays");

            migrationBuilder.DropIndex(
                name: "IX_tbl_ClientRotaDays_ClientRotaId",
                table: "tbl_ClientRotaDays");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tbl_ClientRotaDays");

            migrationBuilder.AddColumn<int>(
                name: "ClientRotaDaysId",
                table: "tbl_ClientRotaDays",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_ClientRotaDays",
                table: "tbl_ClientRotaDays",
                column: "ClientRotaDaysId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ClientRotaDays_tbl_ClientRota_ClientRotaDaysId",
                table: "tbl_ClientRotaDays",
                column: "ClientRotaDaysId",
                principalTable: "tbl_ClientRota",
                principalColumn: "ClientRotaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ClientRotaTask_tbl_ClientRotaDays_ClientRotaDaysId",
                table: "tbl_ClientRotaTask",
                column: "ClientRotaDaysId",
                principalTable: "tbl_ClientRotaDays",
                principalColumn: "ClientRotaDaysId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
