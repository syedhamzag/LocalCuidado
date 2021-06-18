using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffKeyWorkerVoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffKeyWorkerVoice",
                columns: table => new
                {
                    KeyWorkerId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(maxLength: 50, nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    TeamYouWorkFor = table.Column<int>(nullable: false),
                    NotComfortableServices = table.Column<int>(nullable: false),
                    ServicesRequiresTime = table.Column<int>(nullable: false),
                    ServicesRequiresServices = table.Column<int>(nullable: false),
                    WellSupportedServices = table.Column<int>(nullable: false),
                    ChangesWeNeed = table.Column<string>(nullable: false),
                    NutritionalChanges = table.Column<string>(nullable: false),
                    HealthAndWellNessChanges = table.Column<string>(nullable: false),
                    MedicationChanges = table.Column<string>(nullable: false),
                    MovingAndHandling = table.Column<string>(nullable: false),
                    RiskAssessment = table.Column<string>(nullable: false),
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
                    table.PrimaryKey("PK_tbl_StaffKeyWorkerVoice", x => x.KeyWorkerId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffKeyWorkerVoice_tbl_StaffPersonalInfo_StaffId",
                        column: x => x.StaffId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffKeyWorkerVoice_KeyWorkerId",
                table: "tbl_StaffKeyWorkerVoice",
                column: "KeyWorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffKeyWorkerVoice");
        }
    }
}