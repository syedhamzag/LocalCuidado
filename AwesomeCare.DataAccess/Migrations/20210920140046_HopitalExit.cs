using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class HopitalExit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_HospitalExit",
                columns: table => new
                {
                    HospitalExitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Reference = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    PurposeofAdmission = table.Column<string>(nullable: false),
                    ConditionOnDischarge = table.Column<int>(nullable: false),
                    NumberOfStaffRequiredOnDischarge = table.Column<int>(nullable: false),
                    IsGrosSriesAvaible = table.Column<int>(nullable: false),
                    IsHomeCleaned = table.Column<int>(nullable: false),
                    IsMedicationAvaialable = table.Column<int>(nullable: false),
                    IsServiceUseronRota = table.Column<int>(nullable: false),
                    isRotaTeamInformed = table.Column<int>(nullable: false),
                    isLittleCashAvailableForServiceUser = table.Column<int>(nullable: false),
                    ModeOfMeansOfTrasportBackHome = table.Column<int>(nullable: false),
                    URLLINK = table.Column<string>(nullable: false),
                    AreEqipmentNeededAvailable = table.Column<int>(nullable: false),
                    AreStaffTrainnedOnEquipmentNeeded = table.Column<int>(nullable: false),
                    AreContinentProductNeedAndAvailable = table.Column<int>(nullable: false),
                    AreLocalSupportOrProgramNeeded = table.Column<int>(nullable: false),
                    WhichSupportIsNeeded = table.Column<string>(nullable: false),
                    IsCarePlanUpdated = table.Column<int>(nullable: false),
                    ReablementRequired = table.Column<int>(nullable: false),
                    ContactIncaseOfReAdmission = table.Column<string>(nullable: false),
                    Remark = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HospitalExit", x => x.HospitalExitId);
                    table.ForeignKey(
                        name: "FK_tbl_HospitalExit_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HospitalExitOfficerToTakeAction",
                columns: table => new
                {
                    HospitalExitOfficerToTakeActionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalExitId = table.Column<int>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HospitalExitOfficerToTakeAction", x => x.HospitalExitOfficerToTakeActionId);
                    table.ForeignKey(
                        name: "FK_tbl_HospitalExitOfficerToTakeAction_tbl_HospitalExit_HospitalExitId",
                        column: x => x.HospitalExitId,
                        principalTable: "tbl_HospitalExit",
                        principalColumn: "HospitalExitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HospitalExitOfficerToTakeAction_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HospitalExit_ClientId",
                table: "tbl_HospitalExit",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HospitalExitOfficerToTakeAction_HospitalExitId",
                table: "tbl_HospitalExitOfficerToTakeAction",
                column: "HospitalExitId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HospitalExitOfficerToTakeAction_StaffPersonalInfoId",
                table: "tbl_HospitalExitOfficerToTakeAction",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_HospitalExitOfficerToTakeAction");

            migrationBuilder.DropTable(
                name: "tbl_HospitalExit");
        }
    }
}
