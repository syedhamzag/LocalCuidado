using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_MedAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_MedAudit_OfficerToAct",
                columns: table => new
                {
                    MedAuditOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    MedAuditId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MedAudit_OfficerToAct", x => x.MedAuditOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_MedAudit_OfficerToAct_tbl_Client_MedAudit_MedAuditId",
                        column: x => x.MedAuditId,
                        principalTable: "tbl_Client_MedAudit",
                        principalColumn: "MedAuditId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_MedAudit_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_MedAudit_AuditorName",
                columns: table => new
                {
                    MedAuditStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    MedAuditId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MedAudit_AuditorName", x => x.MedAuditStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_MedAuditStaffName_tbl_Client_MedAudit_MedAuditId",
                        column: x => x.MedAuditId,
                        principalTable: "tbl_Client_MedAudit",
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
                name: "IX_tbl_MedAudit_OfficerToAct_MedAuditId",
                table: "tbl_MedAudit_OfficerToAct",
                column: "MedAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedAudit_AuditorName_MedAuditId",
                table: "tbl_MedAudit_AuditorName",
                column: "MedAuditId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_MedAudit_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_MedAudit_AuditorName");
        }
    }
}
