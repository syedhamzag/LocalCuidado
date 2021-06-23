using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_WoundCare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_WoundCare_OfficerToAct",
                columns: table => new
                {
                    WoundCareOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WoundCareId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WoundCare_OfficerToAct", x => x.WoundCareOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_OfficerToAct_tbl_Client_WoundCare_WoundCareId",
                        column: x => x.WoundCareId,
                        principalTable: "tbl_Client_WoundCare",
                        principalColumn: "WoundCareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WoundCare_StaffName",
                columns: table => new
                {
                    WoundCareStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WoundCareId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WoundCareStaffInvolved", x => x.WoundCareStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_StaffName_tbl_Client_WoundCare_WoundCareId",
                        column: x => x.WoundCareId,
                        principalTable: "tbl_Client_WoundCare",
                        principalColumn: "WoundCareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WoundCare_Physician",
                columns: table => new
                {
                    WoundCarePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WoundCareId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WoundCare_Physician", x => x.WoundCarePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_Physician_tbl_Client_WoundCare_WoundCareId",
                        column: x => x.WoundCareId,
                        principalTable: "tbl_Client_WoundCare",
                        principalColumn: "WoundCareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCare_OfficerToAct_WoundCareId",
                table: "tbl_WoundCare_OfficerToAct",
                column: "WoundCareId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCare_Physician_WoundCareId",
                table: "tbl_WoundCare_Physician",
                column: "WoundCareId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCare_StaffName_WoundCareId",
                table: "tbl_WoundCare_StaffName",
                column: "WoundCareId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_WoundCare_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_WoundCare_Physician");
            migrationBuilder.DropTable(
                name: "tbl_WoundCare_StaffName");
        }
    }
}
