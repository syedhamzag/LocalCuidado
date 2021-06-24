using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_Seizure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Seizure_OfficerToAct",
                columns: table => new
                {
                    SeizureOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SeizureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Seizure_OfficerToAct", x => x.SeizureOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_OfficerToAct_tbl_Client_Seizure_SeizureId",
                        column: x => x.SeizureId,
                        principalTable: "tbl_Client_Seizure",
                        principalColumn: "SeizureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Seizure_StaffName",
                columns: table => new
                {
                    SeizureStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SeizureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SeizureStaffInvolved", x => x.SeizureStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_StaffName_tbl_Client_Seizure_SeizureId",
                        column: x => x.SeizureId,
                        principalTable: "tbl_Client_Seizure",
                        principalColumn: "SeizureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Seizure_Physician",
                columns: table => new
                {
                    SeizurePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SeizureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Seizure_Physician", x => x.SeizurePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_Physician_tbl_Client_Seizure_SeizureId",
                        column: x => x.SeizureId,
                        principalTable: "tbl_Client_Seizure",
                        principalColumn: "SeizureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Seizure_OfficerToAct_SeizureId",
                table: "tbl_Seizure_OfficerToAct",
                column: "SeizureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Seizure_Physician_SeizureId",
                table: "tbl_Seizure_Physician",
                column: "SeizureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Seizure_StaffName_SeizureId",
                table: "tbl_Seizure_StaffName",
                column: "SeizureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Seizure_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_Seizure_Physician");
            migrationBuilder.DropTable(
                name: "tbl_Seizure_StaffName");
        }
    }
}
