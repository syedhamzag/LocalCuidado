using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_BodyTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BodyTemp_OfficerToAct",
                columns: table => new
                {
                    BodyTempOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BodyTempId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BodyTemp_OfficerToAct", x => x.BodyTempOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_OfficerToAct_tbl_Client_BodyTemp_BodyTempId",
                        column: x => x.BodyTempId,
                        principalTable: "tbl_Client_BodyTemp",
                        principalColumn: "BodyTempId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BodyTemp_StaffName",
                columns: table => new
                {
                    BodyTempStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BodyTempId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BodyTempStaffInvolved", x => x.BodyTempStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_StaffName_tbl_Client_BodyTemp_BodyTempId",
                        column: x => x.BodyTempId,
                        principalTable: "tbl_Client_BodyTemp",
                        principalColumn: "BodyTempId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BodyTemp_Physician",
                columns: table => new
                {
                    BodyTempPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BodyTempId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BodyTemp_Physician", x => x.BodyTempPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_Physician_tbl_Client_BodyTemp_BodyTempId",
                        column: x => x.BodyTempId,
                        principalTable: "tbl_Client_BodyTemp",
                        principalColumn: "BodyTempId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTemp_OfficerToAct_BodyTempId",
                table: "tbl_BodyTemp_OfficerToAct",
                column: "BodyTempId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTemp_Physician_BodyTempId",
                table: "tbl_BodyTemp_Physician",
                column: "BodyTempId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTemp_StaffName_BodyTempId",
                table: "tbl_BodyTemp_StaffName",
                column: "BodyTempId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BodyTemp_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_BodyTemp_Physician");
            migrationBuilder.DropTable(
                name: "tbl_BodyTemp_StaffName");
        }
    }
}
