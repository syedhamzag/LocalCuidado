using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiHeartRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_HeartRateOfficerToAct",
                columns: table => new
                {
                    HeartRateOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    HeartRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HeartRateOfficerToAct", x => x.HeartRateOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRateOfficerToAct_tbl_ClientHeartRate_HeartRateId",
                        column: x => x.HeartRateId,
                        principalTable: "tbl_ClientHeartRate",
                        principalColumn: "HeartRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRateOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HeartRateStaffName",
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
                        name: "FK_tbl_HeartRateStaffName_tbl_ClientHeartRate_HeartRateId",
                        column: x => x.HeartRateId,
                        principalTable: "tbl_ClientHeartRate",
                        principalColumn: "HeartRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRateStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HeartRatePhysician",
                columns: table => new
                {
                    HeartRatePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    HeartRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HeartRatePhysician", x => x.HeartRatePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRatePhysician_tbl_ClientHeartRate_HeartRateId",
                        column: x => x.HeartRateId,
                        principalTable: "tbl_ClientHeartRate",
                        principalColumn: "HeartRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRatePhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRateOfficerToAct_HeartRateId",
                table: "tbl_HeartRateOfficerToAct",
                column: "HeartRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRatePhysician_HeartRateId",
                table: "tbl_HeartRatePhysician",
                column: "HeartRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRateStaffName_HeartRateId",
                table: "tbl_HeartRateStaffName",
                column: "HeartRateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_HeartRateOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_HeartRatePhysician");
            migrationBuilder.DropTable(
                name: "tbl_HeartRateStaffName");
        }
    }
}
