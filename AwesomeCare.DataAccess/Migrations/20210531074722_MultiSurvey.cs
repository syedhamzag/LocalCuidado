using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_SurveyOfficerToAct",
                columns: table => new
                {
                    SurveyOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    StaffSurveyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SurveyOfficerToAct", x => x.SurveyOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_SurveyOfficerToAct_tbl_ClientSurvey_StaffSurveyId",
                        column: x => x.StaffSurveyId,
                        principalTable: "tbl_ClientSurvey",
                        principalColumn: "StaffSurveyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_SurveyOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SurveyStaffName",
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
                        name: "FK_tbl_SurveyStaffName_tbl_ClientSurvey_StaffSurveyId",
                        column: x => x.StaffSurveyId,
                        principalTable: "tbl_ClientSurvey",
                        principalColumn: "StaffSurveyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_SurveyStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SurveyOfficerToAct_StaffSurveyId",
                table: "tbl_SurveyOfficerToAct",
                column: "StaffSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SurveyStaffName_StaffSurveyId",
                table: "tbl_SurveyStaffName",
                column: "StaffSurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_SurveyOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_SurveyStaffName");
        }
    }
}
