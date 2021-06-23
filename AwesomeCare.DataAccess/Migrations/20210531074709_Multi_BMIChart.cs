using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_BMIChart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BMIChart_OfficerToAct",
                columns: table => new
                {
                    BMIChartOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BMIChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BMIChart_OfficerToAct", x => x.BMIChartOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_OfficerToAct_tbl_Client_BMIChart_BMIChartId",
                        column: x => x.BMIChartId,
                        principalTable: "tbl_Client_BMIChart",
                        principalColumn: "BMIChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BMIChart_StaffName",
                columns: table => new
                {
                    BMIChartStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BMIChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BMIChartStaffInvolved", x => x.BMIChartStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_StaffName_tbl_Client_BMIChart_BMIChartId",
                        column: x => x.BMIChartId,
                        principalTable: "tbl_Client_BMIChart",
                        principalColumn: "BMIChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BMIChart_Physician",
                columns: table => new
                {
                    BMIChartPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BMIChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BMIChart_Physician", x => x.BMIChartPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_Physician_tbl_Client_BMIChart_BMIChartId",
                        column: x => x.BMIChartId,
                        principalTable: "tbl_Client_BMIChart",
                        principalColumn: "BMIChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChart_OfficerToAct_BMIChartId",
                table: "tbl_BMIChart_OfficerToAct",
                column: "BMIChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChart_Physician_BMIChartId",
                table: "tbl_BMIChart_Physician",
                column: "BMIChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChart_StaffName_BMIChartId",
                table: "tbl_BMIChart_StaffName",
                column: "BMIChartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BMIChart_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_BMIChart_Physician");
            migrationBuilder.DropTable(
                name: "tbl_BMIChart_StaffName");
        }
    }
}
