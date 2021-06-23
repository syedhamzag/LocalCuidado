using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Client_MedAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Client_MedAudit",
                columns: table => new
                {
                    MedAuditId = table.Column<int>(nullable:false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(maxLength: 50, nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextDueDate = table.Column<DateTime>(nullable: false),
                    GapsInAdmistration = table.Column<int>(nullable: false),
                    RightsOfMedication = table.Column<string>(nullable: false),
                    MarChartReview = table.Column<int>(nullable: false),
                    MedicationConcern = table.Column<int>(nullable: false),
                    HardCopyReview = table.Column<int>(nullable: false),
                    ThinkingServiceUsers = table.Column<string>(nullable: false),
                    MedicationSupplyEfficiency = table.Column<int>(nullable: false),
                    MedicationInfoUploadEefficiency = table.Column<int>(nullable: false),
                    Observations = table.Column<string>(nullable: false),
                    ActionRecommended = table.Column<string>(nullable: false),
                    ActionTaken = table.Column<string>(nullable: false),
                    EvidenceOfActionTaken = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    RepeatOfIncident = table.Column<int>(nullable: false),
                    RotCause = table.Column<string>(nullable: false),
                    LessonLearntAndShared = table.Column<string>(nullable: false),
                    LogURL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_MedAudit", x => x.MedAuditId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_MedAudit_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_Client_MedAudit_MedAuditId",
                    table: "tbl_Client_MedAudit",
                    column: "MedAuditId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Client_MedAudit");
        }
    }
}
