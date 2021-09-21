using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class TaskBoard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_TaskBoard",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(nullable: false),
                    AssignedBy = table.Column<int>(nullable: false),
                    TaskImage = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TaskBoard", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_TaskBoardAssignedTo",
                columns: table => new
                {
                    TaskBoardAssignedToId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    TaskBoardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TaskBoardAssignedTo", x => x.TaskBoardAssignedToId);
                    table.ForeignKey(
                        name: "FK_tbl_TaskBoardAssignedTo_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_TaskBoardAssignedTo_tbl_TaskBoard_TaskBoardId",
                        column: x => x.TaskBoardId,
                        principalTable: "tbl_TaskBoard",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TaskBoardAssignedTo_StaffPersonalInfoId",
                table: "tbl_TaskBoardAssignedTo",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TaskBoardAssignedTo_TaskBoardId",
                table: "tbl_TaskBoardAssignedTo",
                column: "TaskBoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_TaskBoardAssignedTo");

            migrationBuilder.DropTable(
                name: "tbl_TaskBoard");
        }
    }
}
