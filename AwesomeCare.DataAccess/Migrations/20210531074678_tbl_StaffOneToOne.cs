﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffOneToOne",
                columns: table => new
                {
                    OneToOneId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(maxLength: 50, nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Purpose = table.Column<string>(nullable: false),
                    PreviousSupervision = table.Column<int>(nullable: false),
                    StaffImprovedInAreas = table.Column<string>(nullable: false),
                    CurrentEventArea = table.Column<string>(nullable: false),
                    StaffConclusion = table.Column<string>(nullable: false),
                    DecisionsReached = table.Column<string>(nullable: false),
                    ImprovementRecorded = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    //OfficerToAct = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffOneToOne", x => x.OneToOneId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffOneToOne_tbl_StaffPersonalInfo_StaffId",
                        column: x => x.StaffId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffOneToOne_OneToOneId",
                table: "tbl_StaffOneToOne",
                column: "OneToOneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffOneToOne");
        }
    }
}
