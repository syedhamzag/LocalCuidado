using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class ClientPerformanceIndicator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientPerformanceIndicator",
                columns: table => new
                {
                    PerformanceIndicatorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Heading = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientPerformanceIndicator", x => x.PerformanceIndicatorId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ClientPerformanceIndicatorTask",
                columns: table => new
                {
                    PerformanceIndicatorTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffCompetenceTestId = table.Column<int>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientPerformanceIndicatorTask", x => x.PerformanceIndicatorTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientPerformanceIndicatorTask_tbl_ClientPerformanceIndicator_StaffCompetenceTestId",
                        column: x => x.StaffCompetenceTestId,
                        principalTable: "tbl_ClientPerformanceIndicator",
                        principalColumn: "PerformanceIndicatorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientPerformanceIndicatorTask_StaffCompetenceTestId",
                table: "tbl_ClientPerformanceIndicatorTask",
                column: "StaffCompetenceTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientPerformanceIndicatorTask");

            migrationBuilder.DropTable(
                name: "tbl_ClientPerformanceIndicator");
        }
    }
}
