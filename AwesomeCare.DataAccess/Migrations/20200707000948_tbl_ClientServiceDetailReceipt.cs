using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientServiceDetailReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientServiceDetailReceipt",
                columns: table => new
                {
                    ClientServiceDetailReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientServiceDetailId = table.Column<int>(nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientServiceDetailReceipt", x => x.ClientServiceDetailReceiptId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientServiceDetailReceipt_tbl_ClientServiceDetail_ClientServiceDetailId",
                        column: x => x.ClientServiceDetailId,
                        principalTable: "tbl_ClientServiceDetail",
                        principalColumn: "ClientServiceDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientServiceDetailReceipt_ClientServiceDetailId",
                table: "tbl_ClientServiceDetailReceipt",
                column: "ClientServiceDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientServiceDetailReceipt");
        }
    }
}
