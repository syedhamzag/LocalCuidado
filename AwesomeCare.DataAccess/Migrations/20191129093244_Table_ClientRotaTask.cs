using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Table_ClientRotaTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientRotaTask",
                columns: table => new
                {
                    ClientRotaTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RotaTaskId = table.Column<int>(nullable: false),
                    ClientRotaDaysId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientRotaTask", x => x.ClientRotaTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientRotaTask_tbl_ClientRotaDays_ClientRotaDaysId",
                        column: x => x.ClientRotaDaysId,
                        principalTable: "tbl_ClientRotaDays",
                        principalColumn: "ClientRotaDaysId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientRotaTask_tbl_RotaTask_RotaTaskId",
                        column: x => x.RotaTaskId,
                        principalTable: "tbl_RotaTask",
                        principalColumn: "RotaTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientRotaTask_ClientRotaDaysId",
                table: "tbl_ClientRotaTask",
                column: "ClientRotaDaysId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientRotaTask_RotaTaskId",
                table: "tbl_ClientRotaTask",
                column: "RotaTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientRotaTask");
        }
    }
}
