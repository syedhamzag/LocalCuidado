using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class PerfrmanceIndicato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_PerformanceIndicator",
                columns: table => new
                {
                    PerformanceIndicatorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Heading = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PerformanceIndicator", x => x.PerformanceIndicatorId);
                    table.ForeignKey(
                        name: "FK_tbl_PerformanceIndicator_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PerformanceIndicatorTask",
                columns: table => new
                {
                    PerformanceIndicatorTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffCompetenceTestId = table.Column<int>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PerformanceIndicatorTask", x => x.PerformanceIndicatorTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_PerformanceIndicatorTask_tbl_PerformanceIndicator_StaffCompetenceTestId",
                        column: x => x.StaffCompetenceTestId,
                        principalTable: "tbl_PerformanceIndicator",
                        principalColumn: "PerformanceIndicatorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PerformanceIndicator_StaffPersonalInfoId",
                table: "tbl_PerformanceIndicator",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PerformanceIndicatorTask_StaffCompetenceTestId",
                table: "tbl_PerformanceIndicatorTask",
                column: "StaffCompetenceTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_PerformanceIndicatorTask");

            migrationBuilder.DropTable(
                name: "tbl_PerformanceIndicator");
        }
    }
}
