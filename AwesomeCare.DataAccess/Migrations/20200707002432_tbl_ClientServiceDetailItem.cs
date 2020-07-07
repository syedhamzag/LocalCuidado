using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientServiceDetailItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientServiceDetailItem",
                columns: table => new
                {
                    ClientServiceDetailItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientServiceDetailId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 225, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientServiceDetailItem", x => x.ClientServiceDetailItemId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientServiceDetailItem_tbl_ClientServiceDetail_ClientServiceDetailId",
                        column: x => x.ClientServiceDetailId,
                        principalTable: "tbl_ClientServiceDetail",
                        principalColumn: "ClientServiceDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientServiceDetailItem_ClientServiceDetailId",
                table: "tbl_ClientServiceDetailItem",
                column: "ClientServiceDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientServiceDetailItem");
        }
    }
}
