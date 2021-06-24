using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_Visit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Visit_OfficerToAct",
                columns: table => new
                {
                    VisitOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Visit_OfficerToAct", x => x.VisitOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Visit_OfficerToAct_tbl_Client_MgtVisit_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_Client_MgtVisit",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Visit_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Visit_StaffName",
                columns: table => new
                {
                    VisitStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_VisitStaffBestSupport", x => x.VisitStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Visit_StaffName_tbl_Client_MgtVisit_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_Client_MgtVisit",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Visit_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Visit_OfficerToAct_BloodRecordId",
                table: "tbl_Visit_OfficerToAct",
                column: "BloodRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Visit_StaffName_BloodRecordId",
                table: "tbl_Visit_StaffName",
                column: "BloodRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Visit_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_Visit_StaffName");
        }
    }
}
