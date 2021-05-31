using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientLogAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientLogAudit",
                columns: table => new
                {
                    LogAuditId = table.Column<int>(nullable:false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextDueDate = table.Column<DateTime>(nullable: false),
                    IsCareExpected = table.Column<string>(maxLength: 255, nullable: false),
                    IsCareDifference = table.Column<string>(maxLength: 255, nullable: false),
                    ProperDocumentation = table.Column<string>(maxLength: 255, nullable: false),
                    ImproperDocumentation = table.Column<string>(maxLength: 255, nullable: false),
                    Communication = table.Column<string>(maxLength: 255, nullable: false),
                    ThinkingServiceUsers = table.Column<string>(maxLength: 255, nullable: false),
                    ThinkingStaff = table.Column<string>(maxLength: 255, nullable: false),
                    ThinkingStaffStop = table.Column<string>(maxLength: 255, nullable: false),
                    Observations = table.Column<string>(maxLength: 255, nullable: false),
                    NameOfAuditor = table.Column<string>(maxLength: 255, nullable: false),
                    ActionRecommended = table.Column<string>(maxLength: 255, nullable: false),
                    ActionTaken = table.Column<string>(maxLength: 255, nullable: false),
                    EvidenceOfActionTaken = table.Column<string>(nullable: false),
                    OfficerToTakeAction = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 255, nullable: false),
                    RepeatOfIncident = table.Column<int>(nullable: false),
                    RotCause = table.Column<string>(maxLength: 50, nullable: false),
                    LessonLearntAndShared = table.Column<string>(maxLength: 255, nullable: false),
                    LogURL = table.Column<string>(maxLength: 255, nullable: false),
                    EvidenceFilePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientLogAudit", x => x.LogAuditId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientLogAudit_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientLogAudit_LogAuditId",
                    table: "tbl_ClientLogAudit",
                    column: "LogAuditId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientLogAudit");
        }
    }
}
