﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiVisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_VisitOfficerToAct",
                columns: table => new
                {
                    VisitOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_VisitOfficerToAct", x => x.VisitOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_VisitOfficerToAct_tbl_ClientMgtVisit_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_ClientMgtVisit",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_VisitOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_VisitStaffName",
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
                        name: "FK_tbl_VisitStaffName_tbl_ClientMgtVisit_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_ClientMgtVisit",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_VisitStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_VisitOfficerToAct_BloodRecordId",
                table: "tbl_VisitOfficerToAct",
                column: "BloodRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_VisitStaffBestSupport_BloodRecordId",
                table: "tbl_VisitStaffBestSupport",
                column: "BloodRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_VisitOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_VisitStaffName");
        }
    }
}
