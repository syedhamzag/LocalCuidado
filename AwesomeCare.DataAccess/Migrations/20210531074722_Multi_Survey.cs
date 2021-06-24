using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_Survey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Survey_OfficerToAct",
                columns: table => new
                {
                    SurveyOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    StaffSurveyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Survey_OfficerToAct", x => x.SurveyOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Survey_OfficerToAct_tbl_ClientSurvey_StaffSurveyId",
                        column: x => x.StaffSurveyId,
                        principalTable: "tbl_ClientSurvey",
                        principalColumn: "StaffSurveyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Survey_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Survey_StaffName",
                columns: table => new
                {
                    SurveyStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    StaffSurveyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SurveyStaffInvolved", x => x.SurveyStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Survey_StaffName_tbl_ClientSurvey_StaffSurveyId",
                        column: x => x.StaffSurveyId,
                        principalTable: "tbl_ClientSurvey",
                        principalColumn: "StaffSurveyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Survey_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Survey_OfficerToAct_StaffSurveyId",
                table: "tbl_Survey_OfficerToAct",
                column: "StaffSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Survey_StaffName_StaffSurveyId",
                table: "tbl_Survey_StaffName",
                column: "StaffSurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Survey_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_Survey_StaffName");
        }
    }
}
