using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_BloodPressure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BloodPressure_OfficerToAct",
                columns: table => new
                {
                    BloodPressureOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodPressureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodPressure_OfficerToAct", x => x.BloodPressureOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_OfficerToAct_tbl_Client_BloodPressure_BloodPressureId",
                        column: x => x.BloodPressureId,
                        principalTable: "tbl_Client_BloodPressure",
                        principalColumn: "BloodPressureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodPressure_StaffName",
                columns: table => new
                {
                    BloodPressureStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodPressureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodPressureStaffInvolved", x => x.BloodPressureStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_StaffName_tbl_Client_BloodPressure_BloodPressureId",
                        column: x => x.BloodPressureId,
                        principalTable: "tbl_Client_BloodPressure",
                        principalColumn: "BloodPressureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodPressure_Physician",
                columns: table => new
                {
                    BloodPressurePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodPressureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodPressure_Physician", x => x.BloodPressurePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_Physician_tbl_Client_BloodPressure_BloodPressureId",
                        column: x => x.BloodPressureId,
                        principalTable: "tbl_Client_BloodPressure",
                        principalColumn: "BloodPressureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressure_OfficerToAct_BloodPressureId",
                table: "tbl_BloodPressure_OfficerToAct",
                column: "BloodPressureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressure_Physician_BloodPressureId",
                table: "tbl_BloodPressure_Physician",
                column: "BloodPressureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressure_StaffName_BloodPressureId",
                table: "tbl_BloodPressure_StaffName",
                column: "BloodPressureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BloodPressure_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_BloodPressure_Physician");
            migrationBuilder.DropTable(
                name: "tbl_BloodPressure_StaffName");
        }
    }
}
