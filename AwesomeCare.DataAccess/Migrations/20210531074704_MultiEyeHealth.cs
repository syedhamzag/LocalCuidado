using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiEyeHealth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_EyeHealthOfficerToAct",
                columns: table => new
                {
                    EyeHealthOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    EyeHealthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EyeHealthOfficerToAct", x => x.EyeHealthOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealthOfficerToAct_tbl_ClientEyeHealthMonitoring_EyeHealthId",
                        column: x => x.EyeHealthId,
                        principalTable: "tbl_ClientEyeHealthMonitoring",
                        principalColumn: "EyeHealthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealthOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EyeHealthStaffName",
                columns: table => new
                {
                    EyeHealthStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    EyeHealthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EyeHealthStaffInvolved", x => x.EyeHealthStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealthStaffName_tbl_ClientEyeHealthMonitoring_EyeHealthId",
                        column: x => x.EyeHealthId,
                        principalTable: "tbl_ClientEyeHealthMonitoring",
                        principalColumn: "EyeHealthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealthStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EyeHealthPhysician",
                columns: table => new
                {
                    EyeHealthPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    EyeHealthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EyeHealthPhysician", x => x.EyeHealthPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealthPhysician_tbl_ClientEyeHealthMonitoring_EyeHealthId",
                        column: x => x.EyeHealthId,
                        principalTable: "tbl_ClientEyeHealthMonitoring",
                        principalColumn: "EyeHealthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealthPhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealthOfficerToAct_EyeHealthId",
                table: "tbl_EyeHealthOfficerToAct",
                column: "EyeHealthId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealthPhysician_EyeHealthId",
                table: "tbl_EyeHealthPhysician",
                column: "EyeHealthId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealthStaffName_EyeHealthId",
                table: "tbl_EyeHealthStaffName",
                column: "EyeHealthId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_EyeHealthOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_EyeHealthPhysician");
            migrationBuilder.DropTable(
                name: "tbl_EyeHealthStaffName");
        }
    }
}
