using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Whisttle_Blower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Whisttle_Blower",
                columns: table => new
                {
                    WhisttleBlowerId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<int>(nullable: false),
                    StaffName = table.Column<string>(nullable: false),
                    IncidentDate = table.Column<string>(nullable: false),
                    Happening = table.Column<string>(nullable: false),
                    Evidence = table.Column<string>(nullable: true),
                    Witness = table.Column<int>(nullable: false),
                    LikeCalling = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Whisttle_BlowerId", x => x.WhisttleBlowerId);
                    table.ForeignKey(
                        name: "FK_tbl_Whisttle_Blower_tbl_Client_UserName",
                        column: x => x.UserName,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                }); ;
            migrationBuilder.CreateIndex(
                name: "IX_tbl_Whisttle_Blower_WhisttleBlowerId",
                table: "tbl_Whisttle_Blower",
                column: "WhisttleBlowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Whisttle_Blower");
        }
    }
}
