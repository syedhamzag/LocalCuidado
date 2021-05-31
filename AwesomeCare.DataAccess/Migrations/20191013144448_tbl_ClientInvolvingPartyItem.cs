using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientInvolvingPartyItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientInvolvingPartyItem",
                columns: table => new
                {
                    Deleted = table.Column<bool>(nullable: false),
                    ClientInvolvingPartyItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 225, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientInvolvingPartyItem", x => x.ClientInvolvingPartyItemId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_IdNumber",
                table: "tbl_Client",
                column: "IdNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientInvolvingPartyItem_ItemName",
                table: "tbl_ClientInvolvingPartyItem",
                column: "ItemName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientInvolvingPartyItem");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Client_IdNumber",
                table: "tbl_Client");
        }
    }
}
