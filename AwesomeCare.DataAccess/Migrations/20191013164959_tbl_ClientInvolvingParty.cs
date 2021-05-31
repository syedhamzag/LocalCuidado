using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientInvolvingParty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientInvolvingParty",
                columns: table => new
                {
                    ClientInvolvingPartyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientInvolvingPartyItemId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 225, nullable: false),
                    Email = table.Column<string>(maxLength: 125, nullable: false),
                    Telephone = table.Column<string>(maxLength: 50, nullable: false),
                    Relationship = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientInvolvingParty", x => x.ClientInvolvingPartyId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientInvolvingParty_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientInvolvingParty_tbl_ClientInvolvingPartyItem_ClientInvolvingPartyItemId",
                        column: x => x.ClientInvolvingPartyItemId,
                        principalTable: "tbl_ClientInvolvingPartyItem",
                        principalColumn: "ClientInvolvingPartyItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientInvolvingParty_ClientId",
                table: "tbl_ClientInvolvingParty",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientInvolvingParty_ClientInvolvingPartyItemId",
                table: "tbl_ClientInvolvingParty",
                column: "ClientInvolvingPartyItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientInvolvingParty");
        }
    }
}
