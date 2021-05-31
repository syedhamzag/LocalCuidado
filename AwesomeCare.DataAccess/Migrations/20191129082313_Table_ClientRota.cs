using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Table_ClientRota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientRota",
                columns: table => new
                {
                    ClientRotaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    ClientRotaTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientRota", x => x.ClientRotaId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientRota_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientRota_tbl_ClientRotaType_ClientRotaTypeId",
                        column: x => x.ClientRotaTypeId,
                        principalTable: "tbl_ClientRotaType",
                        principalColumn: "ClientRotaTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientRota_ClientId",
                table: "tbl_ClientRota",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientRota_ClientRotaTypeId",
                table: "tbl_ClientRota",
                column: "ClientRotaTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientRota");
        }
    }
}
