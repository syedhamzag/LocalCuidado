using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRotaItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffRotaItem",
                columns: table => new
                {
                    StaffRotaItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffRotaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffRotaItem", x => x.StaffRotaItemId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffRotaItem_tbl_StaffRota_StaffRotaId",
                        column: x => x.StaffRotaId,
                        principalTable: "tbl_StaffRota",
                        principalColumn: "StaffRotaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffRotaItem_StaffRotaId",
                table: "tbl_StaffRotaItem",
                column: "StaffRotaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffRotaItem");
        }
    }
}
