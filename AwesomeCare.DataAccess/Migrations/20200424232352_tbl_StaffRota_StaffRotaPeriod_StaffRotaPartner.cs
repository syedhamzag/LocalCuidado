using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRota_StaffRotaPeriod_StaffRotaPartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffRota",
                columns: table => new
                {
                    StaffRotaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RotaDate = table.Column<string>(maxLength: 15, nullable: false),
                    Staff = table.Column<int>(nullable: false),
                    RotaId = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 225, nullable: false),
                    ReferenceNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffRota", x => x.StaffRotaId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffRota_tbl_ClientRotaName_RotaId",
                        column: x => x.RotaId,
                        principalTable: "tbl_ClientRotaName",
                        principalColumn: "RotaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffRotaPartner",
                columns: table => new
                {
                    StaffRotaPartnerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffRotaId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffRotaPartner", x => x.StaffRotaPartnerId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffRotaPartner_tbl_StaffRota_StaffRotaId",
                        column: x => x.StaffRotaId,
                        principalTable: "tbl_StaffRota",
                        principalColumn: "StaffRotaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffRotaPeriod",
                columns: table => new
                {
                    StaffRotaPeriodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffRotaId = table.Column<int>(nullable: false),
                    ClientRotaTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffRotaPeriod", x => x.StaffRotaPeriodId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffRotaPeriod_tbl_ClientRotaType_ClientRotaTypeId",
                        column: x => x.ClientRotaTypeId,
                        principalTable: "tbl_ClientRotaType",
                        principalColumn: "ClientRotaTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_StaffRotaPeriod_tbl_StaffRota_StaffRotaId",
                        column: x => x.StaffRotaId,
                        principalTable: "tbl_StaffRota",
                        principalColumn: "StaffRotaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffRota_RotaId",
                table: "tbl_StaffRota",
                column: "RotaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffRotaPartner_StaffRotaId",
                table: "tbl_StaffRotaPartner",
                column: "StaffRotaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffRotaPeriod_ClientRotaTypeId",
                table: "tbl_StaffRotaPeriod",
                column: "ClientRotaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffRotaPeriod_StaffRotaId",
                table: "tbl_StaffRotaPeriod",
                column: "StaffRotaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffRotaPartner");

            migrationBuilder.DropTable(
                name: "tbl_StaffRotaPeriod");

            migrationBuilder.DropTable(
                name: "tbl_StaffRota");
        }
    }
}
