using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiBloodCoag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BloodCoagOfficerToAct",
                columns: table => new
                {
                    BloodCoagOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodCoagOfficerToAct", x => x.BloodCoagOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoagOfficerToAct_tbl_ClientBloodCoagulationRecord_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_ClientBloodCoagulationRecord",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoagOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodCoagStaffName",
                columns: table => new
                {
                    BloodCoagStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodCoagStaffInvolved", x => x.BloodCoagStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoagStaffName_tbl_ClientBloodCoagulationRecord_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_ClientBloodCoagulationRecord",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoagStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodCoagPhysician",
                columns: table => new
                {
                    BloodCoagPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodCoagPhysician", x => x.BloodCoagPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoagPhysician_tbl_ClientBloodCoagulationRecord_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_ClientBloodCoagulationRecord",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoagPhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoagOfficerToAct_BloodRecordId",
                table: "tbl_BloodCoagOfficerToAct",
                column: "BloodRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoagPhysician_BloodRecordId",
                table: "tbl_BloodCoagPhysician",
                column: "BloodRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoagStaffName_BloodRecordId",
                table: "tbl_BloodCoagStaffName",
                column: "BloodRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BloodCoagOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_BloodCoagPhysician");
            migrationBuilder.DropTable(
                name: "tbl_BloodCoagStaffName");
        }
    }
}
