using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class StaffTeamLead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffTeamLead",
                columns: table => new
                {
                    TeamLeadId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Rota = table.Column<int>(nullable: false),
                    ClientInvolved = table.Column<int>(nullable: false),
                    StaffInvolved = table.Column<int>(nullable: false),
                    DidYouObserved = table.Column<string>(nullable: false),
                    DidYouDo = table.Column<string>(nullable: false),
                    OfficeToDo = table.Column<string>(nullable: false),
                    StaffStoppedWorking = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffTeamLead", x => x.TeamLeadId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffTeamLead_tbl_Client_ClientInvolved",
                        column: x => x.ClientInvolved,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_StaffTeamLead_tbl_StaffPersonalInfo_StaffInvolved",
                        column: x => x.StaffInvolved,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffTeamLeadTasks",
                columns: table => new
                {
                    TeamLeadTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    TeamLeadId = table.Column<int>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Comments = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffTeamLeadTasks", x => x.TeamLeadTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffTeamLeadTasks_tbl_StaffTeamLead_TeamLeadId",
                        column: x => x.TeamLeadId,
                        principalTable: "tbl_StaffTeamLead",
                        principalColumn: "TeamLeadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffTeamLead_ClientInvolved",
                table: "tbl_StaffTeamLead",
                column: "ClientInvolved");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffTeamLead_StaffInvolved",
                table: "tbl_StaffTeamLead",
                column: "StaffInvolved");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffTeamLeadTasks_TeamLeadId",
                table: "tbl_StaffTeamLeadTasks",
                column: "TeamLeadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffTeamLeadTasks");

            migrationBuilder.DropTable(
                name: "tbl_StaffTeamLead");
        }
    }
}
