using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiPainChart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_PainChartOfficerToAct",
                columns: table => new
                {
                    PainChartOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PainChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PainChartOfficerToAct", x => x.PainChartOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_PainChartOfficerToAct_tbl_ClientPainChart_PainChartId",
                        column: x => x.PainChartId,
                        principalTable: "tbl_ClientPainChart",
                        principalColumn: "PainChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PainChartOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PainChartStaffName",
                columns: table => new
                {
                    PainChartStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PainChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PainChartStaffInvolved", x => x.PainChartStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_PainChartStaffName_tbl_ClientPainChart_PainChartId",
                        column: x => x.PainChartId,
                        principalTable: "tbl_ClientPainChart",
                        principalColumn: "PainChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PainChartStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PainChartPhysician",
                columns: table => new
                {
                    PainChartPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PainChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PainChartPhysician", x => x.PainChartPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_PainChartPhysician_tbl_ClientPainChart_PainChartId",
                        column: x => x.PainChartId,
                        principalTable: "tbl_ClientPainChart",
                        principalColumn: "PainChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PainChartPhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChartOfficerToAct_PainChartId",
                table: "tbl_PainChartOfficerToAct",
                column: "PainChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChartPhysician_PainChartId",
                table: "tbl_PainChartPhysician",
                column: "PainChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChartStaffName_PainChartId",
                table: "tbl_PainChartStaffName",
                column: "PainChartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_PainChartOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_PainChartPhysician");
            migrationBuilder.DropTable(
                name: "tbl_PainChartStaffName");
        }
    }
}
