using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_HeartRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_HeartRate_OfficerToAct",
                columns: table => new
                {
                    HeartRateOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    HeartRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HeartRate_OfficerToAct", x => x.HeartRateOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_OfficerToAct_tbl_Client_HeartRate_HeartRateId",
                        column: x => x.HeartRateId,
                        principalTable: "tbl_Client_HeartRate",
                        principalColumn: "HeartRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HeartRate_StaffName",
                columns: table => new
                {
                    HeartRateStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    HeartRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HeartRateStaffInvolved", x => x.HeartRateStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_StaffName_tbl_Client_HeartRate_HeartRateId",
                        column: x => x.HeartRateId,
                        principalTable: "tbl_Client_HeartRate",
                        principalColumn: "HeartRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HeartRate_Physician",
                columns: table => new
                {
                    HeartRatePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    HeartRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HeartRate_Physician", x => x.HeartRatePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_Physician_tbl_Client_HeartRate_HeartRateId",
                        column: x => x.HeartRateId,
                        principalTable: "tbl_Client_HeartRate",
                        principalColumn: "HeartRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRate_OfficerToAct_HeartRateId",
                table: "tbl_HeartRate_OfficerToAct",
                column: "HeartRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRate_Physician_HeartRateId",
                table: "tbl_HeartRate_Physician",
                column: "HeartRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRate_StaffName_HeartRateId",
                table: "tbl_HeartRate_StaffName",
                column: "HeartRateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_HeartRate_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_HeartRate_Physician");
            migrationBuilder.DropTable(
                name: "tbl_HeartRate_StaffName");
        }
    }
}
