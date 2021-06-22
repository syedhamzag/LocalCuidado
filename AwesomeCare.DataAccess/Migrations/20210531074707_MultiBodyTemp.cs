using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiBodyTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BodyTempOfficerToAct",
                columns: table => new
                {
                    BodyTempOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BodyTempId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BodyTempOfficerToAct", x => x.BodyTempOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTempOfficerToAct_tbl_ClientBodyTemp_BodyTempId",
                        column: x => x.BodyTempId,
                        principalTable: "tbl_ClientBodyTemp",
                        principalColumn: "BodyTempId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTempOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BodyTempStaffName",
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
                        name: "FK_tbl_BodyTempStaffName_tbl_ClientBodyTemp_BodyTempId",
                        column: x => x.BodyTempId,
                        principalTable: "tbl_ClientBodyTemp",
                        principalColumn: "BodyTempId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTempStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BodyTempPhysician",
                columns: table => new
                {
                    BodyTempPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BodyTempId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BodyTempPhysician", x => x.BodyTempPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTempPhysician_tbl_ClientBodyTemp_BodyTempId",
                        column: x => x.BodyTempId,
                        principalTable: "tbl_ClientBodyTemp",
                        principalColumn: "BodyTempId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTempPhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTempOfficerToAct_BodyTempId",
                table: "tbl_BodyTempOfficerToAct",
                column: "BodyTempId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTempPhysician_BodyTempId",
                table: "tbl_BodyTempPhysician",
                column: "BodyTempId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTempStaffName_BodyTempId",
                table: "tbl_BodyTempStaffName",
                column: "BodyTempId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BodyTempOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_BodyTempPhysician");
            migrationBuilder.DropTable(
                name: "tbl_BodyTempStaffName");
        }
    }
}
