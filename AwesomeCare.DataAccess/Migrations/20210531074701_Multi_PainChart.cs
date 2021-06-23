using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_PainChart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_PainChart_OfficerToAct",
                columns: table => new
                {
                    PainChartOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PainChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PainChart_OfficerToAct", x => x.PainChartOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_OfficerToAct_tbl_Client_PainChart_PainChartId",
                        column: x => x.PainChartId,
                        principalTable: "tbl_Client_PainChart",
                        principalColumn: "PainChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PainChart_StaffName",
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
                        name: "FK_tbl_PainChart_StaffName_tbl_Client_PainChart_PainChartId",
                        column: x => x.PainChartId,
                        principalTable: "tbl_Client_PainChart",
                        principalColumn: "PainChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PainChart_Physician",
                columns: table => new
                {
                    PainChartPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PainChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PainChart_Physician", x => x.PainChartPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_Physician_tbl_Client_PainChart_PainChartId",
                        column: x => x.PainChartId,
                        principalTable: "tbl_Client_PainChart",
                        principalColumn: "PainChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChart_OfficerToAct_PainChartId",
                table: "tbl_PainChart_OfficerToAct",
                column: "PainChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChart_Physician_PainChartId",
                table: "tbl_PainChart_Physician",
                column: "PainChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChart_StaffName_PainChartId",
                table: "tbl_PainChart_StaffName",
                column: "PainChartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_PainChart_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_PainChart_Physician");
            migrationBuilder.DropTable(
                name: "tbl_PainChart_StaffName");
        }
    }
}
