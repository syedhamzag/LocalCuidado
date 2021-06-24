using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_KeyWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_KeyWorker_OfficerToAct",
                columns: table => new
                {
                    KeyWorkerOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    KeyWorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_KeyWorker_OfficerToAct", x => x.KeyWorkerOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorker_OfficerToAct_tbl_ClientKeyWorker_KeyWorkerId",
                        column: x => x.KeyWorkerId,
                        principalTable: "tbl_ClientKeyWorker",
                        principalColumn: "KeyWorkerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorker_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_KeyWorker_StaffName",
                columns: table => new
                {
                    KeyWorkerStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    KeyWorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_KeyWorkerStaffInvolved", x => x.KeyWorkerStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorker_StaffName_tbl_ClientKeyWorker_KeyWorkerId",
                        column: x => x.KeyWorkerId,
                        principalTable: "tbl_ClientKeyWorker",
                        principalColumn: "KeyWorkerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorker_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_tbl_KeyWorker_OfficerToAct_KeyWorkerId",
                table: "tbl_KeyWorker_OfficerToAct",
                column: "KeyWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_KeyWorker_StaffName_KeyWorkerId",
                table: "tbl_KeyWorker_StaffName",
                column: "KeyWorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_KeyWorker_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_KeyWorker_StaffName");
        }
    }
}
