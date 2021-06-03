using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientMgtVisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientMgtVisit",
                columns: table => new
                {
                    VisitId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    RateServiceRecieving = table.Column<int>(nullable: false),
                    RateManagers = table.Column<int>(nullable: false),
                    StaffBestSupport = table.Column<int>(nullable: false),
                    HowToComplain = table.Column<int>(nullable: false),
                    ServiceRecommended = table.Column<int>(nullable: false),
                    ImprovementExpect = table.Column<string>(maxLength: 255, nullable: false),
                    Observation = table.Column<string>(maxLength: 255, nullable: false),
                    ActionRequired = table.Column<string>(maxLength: 255, nullable: false),
                    OfficerToAct = table.Column<int>(nullable: false),
                    ActionsTakenByMPCC = table.Column<string>(maxLength: 255, nullable: false),
                    EvidenceOfActionTaken = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    RotCause = table.Column<string>(maxLength: 50, nullable: false),
                    LessonLearntAndShared = table.Column<string>(maxLength: 255, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 255, nullable: false),
                    URL = table.Column<string>(maxLength: 255, nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientMgtVisit", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMgtVisit_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMgtVisit_tbl_StaffPersonalInfo_OfficerToAct",
                        column: x => x.OfficerToAct,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientMgtVisit_VisitId",
                    table: "tbl_ClientMgtVisit",
                    column: "VisitId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientMgtVisit");
        }
    }
}
