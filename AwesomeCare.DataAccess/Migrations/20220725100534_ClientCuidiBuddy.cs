using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class ClientCuidiBuddy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientHealthCondition",
                columns: table => new
                {
                    CHCId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HCId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientHealthCondition", x => x.CHCId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientHealthCondition_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ClientHobbies",
                columns: table => new
                {
                    CHId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientHobbies", x => x.CHId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientHobbies_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CuidiBuddy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    CuidiBuddyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CuidiBuddy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_CuidiBuddy_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HealthCondition",
                columns: table => new
                {
                    HCId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HealthCondition", x => x.HCId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Hobbies",
                columns: table => new
                {
                    HId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Hobbies", x => x.HId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientHealthCondition_ClientId",
                table: "tbl_ClientHealthCondition",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientHobbies_ClientId",
                table: "tbl_ClientHobbies",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CuidiBuddy_ClientId",
                table: "tbl_CuidiBuddy",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientHealthCondition");

            migrationBuilder.DropTable(
                name: "tbl_ClientHobbies");

            migrationBuilder.DropTable(
                name: "tbl_CuidiBuddy");

            migrationBuilder.DropTable(
                name: "tbl_HealthCondition");

            migrationBuilder.DropTable(
                name: "tbl_Hobbies");
        }
    }
}
