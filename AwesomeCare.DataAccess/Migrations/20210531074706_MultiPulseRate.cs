using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiPulseRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_PulseRateOfficerToAct",
                columns: table => new
                {
                    PulseRateOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PulseRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PulseRateOfficerToAct", x => x.PulseRateOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRateOfficerToAct_tbl_ClientPulseRate_PulseRateId",
                        column: x => x.PulseRateId,
                        principalTable: "tbl_ClientPulseRate",
                        principalColumn: "PulseRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRateOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PulseRateStaffName",
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
                        name: "FK_tbl_PulseRateStaffName_tbl_ClientPulseRate_PulseRateId",
                        column: x => x.PulseRateId,
                        principalTable: "tbl_ClientPulseRate",
                        principalColumn: "PulseRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRateStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PulseRatePhysician",
                columns: table => new
                {
                    PulseRatePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PulseRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PulseRatePhysician", x => x.PulseRatePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRatePhysician_tbl_ClientPulseRate_PulseRateId",
                        column: x => x.PulseRateId,
                        principalTable: "tbl_ClientPulseRate",
                        principalColumn: "PulseRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRatePhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRateOfficerToAct_PulseRateId",
                table: "tbl_PulseRateOfficerToAct",
                column: "PulseRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRatePhysician_PulseRateId",
                table: "tbl_PulseRatePhysician",
                column: "PulseRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRateStaffName_PulseRateId",
                table: "tbl_PulseRateStaffName",
                column: "PulseRateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_PulseRateOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_PulseRatePhysician");
            migrationBuilder.DropTable(
                name: "tbl_PulseRateStaffName");
        }
    }
}
