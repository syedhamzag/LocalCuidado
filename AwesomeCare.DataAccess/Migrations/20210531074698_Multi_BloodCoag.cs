using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_BloodCoag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BloodCoag_OfficerToAct",
                columns: table => new
                {
                    BloodCoagOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodCoag_OfficerToAct", x => x.BloodCoagOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_OfficerToAct_tbl_Client_BloodCoagulationRecord_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_Client_BloodCoagulationRecord",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodCoag_StaffName",
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
                        name: "FK_tbl_BloodCoag_StaffName_tbl_Client_BloodCoagulationRecord_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_Client_BloodCoagulationRecord",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodCoag_Physician",
                columns: table => new
                {
                    BloodCoagPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodCoag_Physician", x => x.BloodCoagPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_Physician_tbl_Client_BloodCoagulationRecord_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_Client_BloodCoagulationRecord",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoag_OfficerToAct_BloodRecordId",
                table: "tbl_BloodCoag_OfficerToAct",
                column: "BloodRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoag_Physician_BloodRecordId",
                table: "tbl_BloodCoag_Physician",
                column: "BloodRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoag_StaffName_BloodRecordId",
                table: "tbl_BloodCoag_StaffName",
                column: "BloodRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BloodCoag_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_BloodCoag_Physician");
            migrationBuilder.DropTable(
                name: "tbl_BloodCoag_StaffName");
        }
    }
}
