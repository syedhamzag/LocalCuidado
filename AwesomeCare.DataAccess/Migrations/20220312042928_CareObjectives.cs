using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class CareObjectives : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientCareObj",
                columns: table => new
                {
                    CareObjId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientCareObj", x => x.CareObjId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientCareObj_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CareObjPersonToAct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareObjId = table.Column<int>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CareObjPersonToAct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_CareObjPersonToAct_tbl_ClientCareObj_CareObjId",
                        column: x => x.CareObjId,
                        principalTable: "tbl_ClientCareObj",
                        principalColumn: "CareObjId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_CareObjPersonToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CareObjPersonToAct_CareObjId",
                table: "tbl_CareObjPersonToAct",
                column: "CareObjId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CareObjPersonToAct_StaffPersonalInfoId",
                table: "tbl_CareObjPersonToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientCareObj_ClientId",
                table: "tbl_ClientCareObj",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_CareObjPersonToAct");

            migrationBuilder.DropTable(
                name: "tbl_ClientCareObj");
        }
    }
}
