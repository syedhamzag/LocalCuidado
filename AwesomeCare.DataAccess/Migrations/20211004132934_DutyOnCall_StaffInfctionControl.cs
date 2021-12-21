using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class DutyOnCall_StaffInfctionControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_DutyOnCall",
                columns: table => new
                {
                    DutyOnCallId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefNo = table.Column<string>(nullable: false),
                    TypeOfDutyCall = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    ClientInitial = table.Column<string>(nullable: false),
                    DateOfIncident = table.Column<DateTime>(nullable: false),
                    TypeOfIncident = table.Column<int>(nullable: false),
                    ReportedBy = table.Column<string>(nullable: false),
                    TelephoneToCall = table.Column<int>(nullable: false),
                    PositionOfReporting = table.Column<int>(nullable: false),
                    DateOfCall = table.Column<DateTime>(nullable: false),
                    TimeOfCall = table.Column<DateTime>(nullable: false),
                    DetailsOfIncident = table.Column<string>(nullable: false),
                    ActionTaken = table.Column<string>(nullable: false),
                    DetailsRequired = table.Column<string>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    NotifyPerson = table.Column<bool>(nullable: false),
                    StaffBlacklisted = table.Column<bool>(nullable: false),
                    NotifyStaffInvolved = table.Column<bool>(nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DutyOnCall", x => x.DutyOnCallId);
                    table.ForeignKey(
                        name: "FK_tbl_DutyOnCall_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffInfectionControl",
                columns: table => new
                {
                    InfectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Guideline = table.Column<string>(nullable: false),
                    TestDate = table.Column<DateTime>(nullable: false),
                    VaccStatus = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffInfectionControl", x => x.InfectionId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffInfectionControl_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DutyOnCallPersonResponsible",
                columns: table => new
                {
                    PersonResponsibleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    DutyOnCallId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DutyOnCallPersonResponsible", x => x.PersonResponsibleId);
                    table.ForeignKey(
                        name: "FK_tbl_DutyOnCallPersonResponsible_tbl_DutyOnCall_DutyOnCallId",
                        column: x => x.DutyOnCallId,
                        principalTable: "tbl_DutyOnCall",
                        principalColumn: "DutyOnCallId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_DutyOnCallPersonResponsible_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DutyOnCallPersonToAct",
                columns: table => new
                {
                    PersonToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    DutyOnCallId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DutyOnCallPersonToAct", x => x.PersonToActId);
                    table.ForeignKey(
                        name: "FK_tbl_DutyOnCallPersonToAct_tbl_DutyOnCall_DutyOnCallId",
                        column: x => x.DutyOnCallId,
                        principalTable: "tbl_DutyOnCall",
                        principalColumn: "DutyOnCallId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_DutyOnCallPersonToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DutyOnCall_ClientId",
                table: "tbl_DutyOnCall",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DutyOnCallPersonResponsible_DutyOnCallId",
                table: "tbl_DutyOnCallPersonResponsible",
                column: "DutyOnCallId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DutyOnCallPersonResponsible_StaffPersonalInfoId",
                table: "tbl_DutyOnCallPersonResponsible",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DutyOnCallPersonToAct_DutyOnCallId",
                table: "tbl_DutyOnCallPersonToAct",
                column: "DutyOnCallId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_DutyOnCallPersonToAct_StaffPersonalInfoId",
                table: "tbl_DutyOnCallPersonToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffInfectionControl_StaffPersonalInfoId",
                table: "tbl_StaffInfectionControl",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_DutyOnCallPersonResponsible");

            migrationBuilder.DropTable(
                name: "tbl_DutyOnCallPersonToAct");

            migrationBuilder.DropTable(
                name: "tbl_StaffInfectionControl");

            migrationBuilder.DropTable(
                name: "tbl_DutyOnCall");
        }
    }
}
