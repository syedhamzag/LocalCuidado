using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiBloodPressure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BloodPressureOfficerToAct",
                columns: table => new
                {
                    BloodPressureOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodPressureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodPressureOfficerToAct", x => x.BloodPressureOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressureOfficerToAct_tbl_ClientBloodPressure_BloodPressureId",
                        column: x => x.BloodPressureId,
                        principalTable: "tbl_ClientBloodPressure",
                        principalColumn: "BloodPressureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressureOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodPressureStaffName",
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
                        name: "FK_tbl_BloodPressureStaffName_tbl_ClientBloodPressure_BloodPressureId",
                        column: x => x.BloodPressureId,
                        principalTable: "tbl_ClientBloodPressure",
                        principalColumn: "BloodPressureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressureStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodPressurePhysician",
                columns: table => new
                {
                    BloodPressurePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodPressureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodPressurePhysician", x => x.BloodPressurePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressurePhysician_tbl_ClientBloodPressure_BloodPressureId",
                        column: x => x.BloodPressureId,
                        principalTable: "tbl_ClientBloodPressure",
                        principalColumn: "BloodPressureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressurePhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressureOfficerToAct_BloodPressureId",
                table: "tbl_BloodPressureOfficerToAct",
                column: "BloodPressureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressurePhysician_BloodPressureId",
                table: "tbl_BloodPressurePhysician",
                column: "BloodPressureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressureStaffName_BloodPressureId",
                table: "tbl_BloodPressureStaffName",
                column: "BloodPressureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BloodPressureOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_BloodPressurePhysician");
            migrationBuilder.DropTable(
                name: "tbl_BloodPressureStaffName");
        }
    }
}
