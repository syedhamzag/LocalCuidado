using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiKeyWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_KeyWorkerOfficerToAct",
                columns: table => new
                {
                    KeyWorkerOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    KeyWorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_KeyWorkerOfficerToAct", x => x.KeyWorkerOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorkerOfficerToAct_tbl_ClientKeyWorker_KeyWorkerId",
                        column: x => x.KeyWorkerId,
                        principalTable: "tbl_ClientKeyWorker",
                        principalColumn: "KeyWorkerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorkerOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_KeyWorkerStaffName",
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
                        name: "FK_tbl_KeyWorkerStaffName_tbl_ClientKeyWorker_KeyWorkerId",
                        column: x => x.KeyWorkerId,
                        principalTable: "tbl_ClientKeyWorker",
                        principalColumn: "KeyWorkerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorkerStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_tbl_KeyWorkerOfficerToAct_KeyWorkerId",
                table: "tbl_KeyWorkerOfficerToAct",
                column: "KeyWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_KeyWorkerStaffName_KeyWorkerId",
                table: "tbl_KeyWorkerStaffName",
                column: "KeyWorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_KeyWorkerOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_KeyWorkerStaffName");
        }
    }
}
