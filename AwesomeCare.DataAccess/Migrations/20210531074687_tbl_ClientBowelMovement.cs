using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientBowelMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientBowelMovement",
                columns: table => new
                {
                    BowelMovementId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TypeAttach = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    Color = table.Column<int>(nullable: false),
                    ColorAttach = table.Column<string>(nullable: true),
                    StatusImage = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: false),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientBowelMovement", x => x.BowelMovementId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientBowelMovement_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientBowelMovement_BowelMovementId",
                    table: "tbl_ClientBowelMovement",
                    column: "BowelMovementId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientBowelMovement");
        }
    }
}
