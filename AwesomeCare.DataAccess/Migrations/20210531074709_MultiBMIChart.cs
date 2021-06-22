using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiBMIChart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BMIChartOfficerToAct",
                columns: table => new
                {
                    BMIChartOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BMIChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BMIChartOfficerToAct", x => x.BMIChartOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChartOfficerToAct_tbl_ClientBMIChart_BMIChartId",
                        column: x => x.BMIChartId,
                        principalTable: "tbl_ClientBMIChart",
                        principalColumn: "BMIChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChartOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BMIChartStaffName",
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
                        name: "FK_tbl_BMIChartStaffName_tbl_ClientBMIChart_BMIChartId",
                        column: x => x.BMIChartId,
                        principalTable: "tbl_ClientBMIChart",
                        principalColumn: "BMIChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChartStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BMIChartPhysician",
                columns: table => new
                {
                    BMIChartPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BMIChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BMIChartPhysician", x => x.BMIChartPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChartPhysician_tbl_ClientBMIChart_BMIChartId",
                        column: x => x.BMIChartId,
                        principalTable: "tbl_ClientBMIChart",
                        principalColumn: "BMIChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChartPhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChartOfficerToAct_BMIChartId",
                table: "tbl_BMIChartOfficerToAct",
                column: "BMIChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChartPhysician_BMIChartId",
                table: "tbl_BMIChartPhysician",
                column: "BMIChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChartStaffName_BMIChartId",
                table: "tbl_BMIChartStaffName",
                column: "BMIChartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BMIChartOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_BMIChartPhysician");
            migrationBuilder.DropTable(
                name: "tbl_BMIChartStaffName");
        }
    }
}
