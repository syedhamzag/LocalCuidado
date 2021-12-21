using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class HospitalEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_HospitalEntry",
                columns: table => new
                {
                    HospitalEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    PurposeofAdmission = table.Column<string>(nullable: false),
                    CauseofAdmission = table.Column<string>(nullable: false),
                    LastDateofAdmission = table.Column<DateTime>(nullable: false),
                    ConditionOfAdmission = table.Column<int>(nullable: false),
                    IsFamilyInformed = table.Column<int>(nullable: false),
                    PossibleDateReturn = table.Column<DateTime>(nullable: false),
                    IsHomeCleaned = table.Column<int>(nullable: false),
                    NameParamedicStaff = table.Column<int>(nullable: false),
                    ParamicStaffTeamNo = table.Column<int>(nullable: false),
                    URLLINK = table.Column<string>(nullable: false),
                    MeansOfTransport = table.Column<int>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    Remark = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HospitalEntry", x => x.HospitalEntryId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HospitalEntryPersonToTakeAction",
                columns: table => new
                {
                    HospitalEntryPersonToTakeActionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalEntryId = table.Column<int>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HospitalEntryPersonToTakeAction", x => x.HospitalEntryPersonToTakeActionId);
                    table.ForeignKey(
                        name: "FK_tbl_HospitalEntryPersonToTakeAction_tbl_HospitalEntry_HospitalEntryId",
                        column: x => x.HospitalEntryId,
                        principalTable: "tbl_HospitalEntry",
                        principalColumn: "HospitalEntryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HospitalEntryPersonToTakeAction_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HospitalEntryStaffInvolved",
                columns: table => new
                {
                    HospitalEntryStaffInvolvedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalEntryId = table.Column<int>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HospitalEntryStaffInvolved", x => x.HospitalEntryStaffInvolvedId);
                    table.ForeignKey(
                        name: "FK_tbl_HospitalEntryStaffInvolved_tbl_HospitalEntry_HospitalEntryId",
                        column: x => x.HospitalEntryId,
                        principalTable: "tbl_HospitalEntry",
                        principalColumn: "HospitalEntryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HospitalEntryStaffInvolved_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HospitalEntryPersonToTakeAction_HospitalEntryId",
                table: "tbl_HospitalEntryPersonToTakeAction",
                column: "HospitalEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HospitalEntryPersonToTakeAction_StaffPersonalInfoId",
                table: "tbl_HospitalEntryPersonToTakeAction",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HospitalEntryStaffInvolved_HospitalEntryId",
                table: "tbl_HospitalEntryStaffInvolved",
                column: "HospitalEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HospitalEntryStaffInvolved_StaffPersonalInfoId",
                table: "tbl_HospitalEntryStaffInvolved",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_HospitalEntryPersonToTakeAction");

            migrationBuilder.DropTable(
                name: "tbl_HospitalEntryStaffInvolved");


            migrationBuilder.DropTable(
                name: "tbl_HospitalEntry");

        }
    }
}
