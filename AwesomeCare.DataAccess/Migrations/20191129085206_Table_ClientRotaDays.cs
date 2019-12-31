using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Table_ClientRotaDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientRotaDays",
                columns: table => new
                {
                    ClientRotaDaysId = table.Column<int>(nullable: false),
                    ClientRotaId = table.Column<int>(nullable: false),
                    RotaDayofWeekId = table.Column<int>(nullable: false),
                    StartTime = table.Column<string>(maxLength: 25, nullable: false),
                    StopTime = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientRotaDays", x => x.ClientRotaDaysId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientRotaDays_tbl_ClientRota_ClientRotaDaysId",
                        column: x => x.ClientRotaDaysId,
                        principalTable: "tbl_ClientRota",
                        principalColumn: "ClientRotaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientRotaDays_tbl_RotaDayofWeek_RotaDayofWeekId",
                        column: x => x.RotaDayofWeekId,
                        principalTable: "tbl_RotaDayofWeek",
                        principalColumn: "RotaDayofWeekId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientRotaDays_RotaDayofWeekId",
                table: "tbl_ClientRotaDays",
                column: "RotaDayofWeekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientRotaDays");
        }
    }
}
