using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiMedAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_MedAuditOfficerToAct",
                columns: table => new
                {
                    MedAuditOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    MedAuditId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MedAuditOfficerToAct", x => x.MedAuditOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_MedAuditOfficerToAct_tbl_ClientMedAudit_MedAuditId",
                        column: x => x.MedAuditId,
                        principalTable: "tbl_ClientMedAudit",
                        principalColumn: "MedAuditId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_MedAuditOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_MedAuditStaffName",
                columns: table => new
                {
                    MedAuditStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    MedAuditId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MedAuditAuditorName", x => x.MedAuditStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_MedAuditStaffName_tbl_ClientMedAudit_MedAuditId",
                        column: x => x.MedAuditId,
                        principalTable: "tbl_ClientMedAudit",
                        principalColumn: "MedAuditId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_MedAuditStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedAuditOfficerToAct_MedAuditId",
                table: "tbl_MedAuditOfficerToAct",
                column: "MedAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedAuditAuditorName_MedAuditId",
                table: "tbl_MedAuditAuditorName",
                column: "MedAuditId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_MedAuditOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_MedAuditStaffName");
        }
    }
}
