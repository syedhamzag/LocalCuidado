using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_EyeHealth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_EyeHealth_OfficerToAct",
                columns: table => new
                {
                    EyeHealthOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    EyeHealthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EyeHealth_OfficerToAct", x => x.EyeHealthOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_OfficerToAct_tbl_Client_EyeHealthMonitoring_EyeHealthId",
                        column: x => x.EyeHealthId,
                        principalTable: "tbl_Client_EyeHealthMonitoring",
                        principalColumn: "EyeHealthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EyeHealth_StaffName",
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
                        name: "FK_tbl_EyeHealth_StaffName_tbl_Client_EyeHealthMonitoring_EyeHealthId",
                        column: x => x.EyeHealthId,
                        principalTable: "tbl_Client_EyeHealthMonitoring",
                        principalColumn: "EyeHealthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EyeHealth_Physician",
                columns: table => new
                {
                    EyeHealthPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    EyeHealthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EyeHealth_Physician", x => x.EyeHealthPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_Physician_tbl_Client_EyeHealthMonitoring_EyeHealthId",
                        column: x => x.EyeHealthId,
                        principalTable: "tbl_Client_EyeHealthMonitoring",
                        principalColumn: "EyeHealthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealth_OfficerToAct_EyeHealthId",
                table: "tbl_EyeHealth_OfficerToAct",
                column: "EyeHealthId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealth_Physician_EyeHealthId",
                table: "tbl_EyeHealth_Physician",
                column: "EyeHealthId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealth_StaffName_EyeHealthId",
                table: "tbl_EyeHealth_StaffName",
                column: "EyeHealthId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_EyeHealth_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_EyeHealth_Physician");
            migrationBuilder.DropTable(
                name: "tbl_EyeHealth_StaffName");
        }
    }
}
