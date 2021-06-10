using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientVoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientVoice",
                columns: table => new
                {
                    VoiceId = table.Column<int>(nullable:false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    RateServiceRecieving = table.Column<int>(nullable: false),
                    RateStaffAttending = table.Column<int>(nullable: false),
                    StaffBestSupport = table.Column<int>(nullable: false),
                    StaffPoorSupport = table.Column<int>(nullable: false),
                    OfficeStaffSupport = table.Column<int>(nullable: false),
                    AreasOfImprovements = table.Column<string>(maxLength: 255, nullable: false),
                    SomethingSpecial = table.Column<string>(maxLength: 255, nullable: false),
                    InterestedInPrograms = table.Column<int>(nullable: false),
                    HealthGoalShortTerm = table.Column<string>(maxLength: 255, nullable: false),
                    HealthGoalLongTerm = table.Column<string>(maxLength: 255, nullable: false),
                    NameOfCaller = table.Column<int>(nullable: false),
                    ActionRequired = table.Column<string>(maxLength: 255, nullable: false),
                    ActionsTakenByMPCC = table.Column<string>(maxLength: 255, nullable: false),
                    EvidenceOfActionTaken = table.Column<string>(maxLength: 255, nullable: false),
                    OfficerToAct = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 255, nullable: false),
                    RotCause = table.Column<string>(maxLength: 50, nullable: false),
                    LessonLearntAndShared = table.Column<string>(maxLength: 255, nullable: false),
                    URL = table.Column<string>(maxLength: 255, nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientVoice", x => x.VoiceId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientVoice_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientVoice_VoiceId",
                    table: "tbl_ClientVoice",
                    column: "VoiceId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientVoice");
        }
    }
}
