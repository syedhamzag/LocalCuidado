using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffSurvey",
                columns: table => new
                {
                    StaffSurveyID = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(maxLength: 50, nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    WorkTeam = table.Column<int>(nullable: false),
                    AdequateTrainingReceived = table.Column<int>(nullable: false),
                    HealthCareServicesSatisfaction = table.Column<int>(nullable: false),
                    SupportFromCompany = table.Column<int>(nullable: false),
                    CompanyManagement = table.Column<int>(nullable: false),
                    AccessToPolicies = table.Column<int>(nullable: false),
                    WorkEnvironmentSuggestions = table.Column<string>(nullable: false),
                    AreaRequiringImprovements = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    OfficerToAct = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffSurvey", x => x.StaffSurveyID);
                    table.ForeignKey(
                        name: "FK_tbl_StaffSurvey_tbl_StaffPersonalInfo_StaffId",
                        column: x => x.StaffId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffSurvey_StaffSurveyId",
                table: "tbl_StaffSurvey",
                column: "StaffSurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffSurvey");
        }
    }
}