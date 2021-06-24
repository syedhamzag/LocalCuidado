using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_Supervision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Supervision_OfficerToAct",
                columns: table => new
                {
                    SupervisionOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    StaffSupervisionAppraisalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Supervision_OfficerToAct", x => x.SupervisionOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Supervision_OfficerToAct_tbl_ClientSupervision_StaffSupervisionAppraisalId",
                        column: x => x.StaffSupervisionAppraisalId,
                        principalTable: "tbl_ClientSupervision",
                        principalColumn: "StaffSupervisionAppraisalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Supervision_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Supervision_StaffName",
                columns: table => new
                {
                    SupervisionStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    StaffSupervisionAppraisalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SupervisionStaffInvolved", x => x.SupervisionStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Supervision_StaffName_tbl_ClientSupervision_StaffSupervisionAppraisalId",
                        column: x => x.StaffSupervisionAppraisalId,
                        principalTable: "tbl_ClientSupervision",
                        principalColumn: "StaffSupervisionAppraisalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Supervision_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Supervision_OfficerToAct_StaffSupervisionAppraisalId",
                table: "tbl_Supervision_OfficerToAct",
                column: "StaffSupervisionAppraisalId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Supervision_StaffName_StaffSupervisionAppraisalId",
                table: "tbl_Supervision_StaffName",
                column: "StaffSupervisionAppraisalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Supervision_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_Supervision_StaffName");
        }
    }
}
