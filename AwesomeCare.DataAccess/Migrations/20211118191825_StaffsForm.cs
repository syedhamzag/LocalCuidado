using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class StaffsForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffCompetenceTest",
                columns: table => new
                {
                    StaffCompetenceTestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Heading = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffCompetenceTest", x => x.StaffCompetenceTestId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffCompetenceTest_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffHealth",
                columns: table => new
                {
                    StaffHealthId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Heading = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffHealth", x => x.StaffHealthId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffHealth_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffInterview",
                columns: table => new
                {
                    StaffInterviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Heading = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffInterview", x => x.StaffInterviewId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffInterview_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffShadowing",
                columns: table => new
                {
                    StaffShadowingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Heading = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffShadowing", x => x.StaffShadowingId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffShadowing_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffCompetenceTestTask",
                columns: table => new
                {
                    StaffCompetenceTestTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffCompetenceTestId = table.Column<int>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    Answer = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    Point = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffCompetenceTestTask", x => x.StaffCompetenceTestTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffCompetenceTestTask_tbl_StaffCompetenceTest_StaffCompetenceTestId",
                        column: x => x.StaffCompetenceTestId,
                        principalTable: "tbl_StaffCompetenceTest",
                        principalColumn: "StaffCompetenceTestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffHealthTask",
                columns: table => new
                {
                    StaffHealthTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffHealthId = table.Column<int>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    Answer = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    Point = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffHealthTask", x => x.StaffHealthTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffHealthTask_tbl_StaffHealth_StaffHealthId",
                        column: x => x.StaffHealthId,
                        principalTable: "tbl_StaffHealth",
                        principalColumn: "StaffHealthId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffInterviewTask",
                columns: table => new
                {
                    StaffInterviewTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffInterviewId = table.Column<int>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    Answer = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    Point = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffInterviewTask", x => x.StaffInterviewTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffInterviewTask_tbl_StaffInterview_StaffInterviewId",
                        column: x => x.StaffInterviewId,
                        principalTable: "tbl_StaffInterview",
                        principalColumn: "StaffInterviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffShadowingTask",
                columns: table => new
                {
                    StaffShadowingTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffShadowingId = table.Column<int>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    Answer = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    Point = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffShadowingTask", x => x.StaffShadowingTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffShadowingTask_tbl_StaffShadowing_StaffShadowingId",
                        column: x => x.StaffShadowingId,
                        principalTable: "tbl_StaffShadowing",
                        principalColumn: "StaffShadowingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffCompetenceTest_StaffPersonalInfoId",
                table: "tbl_StaffCompetenceTest",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffCompetenceTestTask_StaffCompetenceTestId",
                table: "tbl_StaffCompetenceTestTask",
                column: "StaffCompetenceTestId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffHealth_StaffPersonalInfoId",
                table: "tbl_StaffHealth",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffHealthTask_StaffHealthId",
                table: "tbl_StaffHealthTask",
                column: "StaffHealthId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffInterview_StaffPersonalInfoId",
                table: "tbl_StaffInterview",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffInterviewTask_StaffInterviewId",
                table: "tbl_StaffInterviewTask",
                column: "StaffInterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffShadowing_StaffPersonalInfoId",
                table: "tbl_StaffShadowing",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffShadowingTask_StaffShadowingId",
                table: "tbl_StaffShadowingTask",
                column: "StaffShadowingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffCompetenceTestTask");

            migrationBuilder.DropTable(
                name: "tbl_StaffHealthTask");

            migrationBuilder.DropTable(
                name: "tbl_StaffInterviewTask");

            migrationBuilder.DropTable(
                name: "tbl_StaffShadowingTask");

            migrationBuilder.DropTable(
                name: "tbl_StaffCompetenceTest");

            migrationBuilder.DropTable(
                name: "tbl_StaffHealth");

            migrationBuilder.DropTable(
                name: "tbl_StaffInterview");

            migrationBuilder.DropTable(
                name: "tbl_StaffShadowing");
        }
    }
}
