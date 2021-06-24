using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_PulseRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_PulseRate_OfficerToAct",
                columns: table => new
                {
                    PulseRateOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PulseRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PulseRate_OfficerToAct", x => x.PulseRateOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_OfficerToAct_tbl_Client_PulseRate_PulseRateId",
                        column: x => x.PulseRateId,
                        principalTable: "tbl_Client_PulseRate",
                        principalColumn: "PulseRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PulseRate_StaffName",
                columns: table => new
                {
                    PulseRateStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PulseRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PulseRateStaffInvolved", x => x.PulseRateStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_StaffName_tbl_Client_PulseRate_PulseRateId",
                        column: x => x.PulseRateId,
                        principalTable: "tbl_Client_PulseRate",
                        principalColumn: "PulseRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PulseRate_Physician",
                columns: table => new
                {
                    PulseRatePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PulseRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PulseRate_Physician", x => x.PulseRatePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_Physician_tbl_Client_PulseRate_PulseRateId",
                        column: x => x.PulseRateId,
                        principalTable: "tbl_Client_PulseRate",
                        principalColumn: "PulseRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRate_OfficerToAct_PulseRateId",
                table: "tbl_PulseRate_OfficerToAct",
                column: "PulseRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRate_Physician_PulseRateId",
                table: "tbl_PulseRate_Physician",
                column: "PulseRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRate_StaffName_PulseRateId",
                table: "tbl_PulseRate_StaffName",
                column: "PulseRateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_PulseRate_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_PulseRate_Physician");
            migrationBuilder.DropTable(
                name: "tbl_PulseRate_StaffName");
        }
    }
}
