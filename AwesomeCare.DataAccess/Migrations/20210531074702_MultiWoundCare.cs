using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiWoundCare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_WoundCareOfficerToAct",
                columns: table => new
                {
                    WoundCareOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WoundCareId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WoundCareOfficerToAct", x => x.WoundCareOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCareOfficerToAct_tbl_ClientWoundCare_WoundCareId",
                        column: x => x.WoundCareId,
                        principalTable: "tbl_ClientWoundCare",
                        principalColumn: "WoundCareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCareOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WoundCareStaffName",
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
                        name: "FK_tbl_WoundCareStaffName_tbl_ClientWoundCare_WoundCareId",
                        column: x => x.WoundCareId,
                        principalTable: "tbl_ClientWoundCare",
                        principalColumn: "WoundCareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCareStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WoundCarePhysician",
                columns: table => new
                {
                    WoundCarePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WoundCareId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WoundCarePhysician", x => x.WoundCarePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCarePhysician_tbl_ClientWoundCare_WoundCareId",
                        column: x => x.WoundCareId,
                        principalTable: "tbl_ClientWoundCare",
                        principalColumn: "WoundCareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCarePhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCareOfficerToAct_WoundCareId",
                table: "tbl_WoundCareOfficerToAct",
                column: "WoundCareId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCarePhysician_WoundCareId",
                table: "tbl_WoundCarePhysician",
                column: "WoundCareId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCareStaffName_WoundCareId",
                table: "tbl_WoundCareStaffName",
                column: "WoundCareId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_WoundCareOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_WoundCarePhysician");
            migrationBuilder.DropTable(
                name: "tbl_WoundCareStaffName");
        }
    }
}
