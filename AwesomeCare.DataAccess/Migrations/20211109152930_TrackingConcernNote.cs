using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class TrackingConcernNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_TrackingConcernNote",
                columns: table => new
                {
                    Ref = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    ConcernNote = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    DateOfIncident = table.Column<DateTime>(nullable: false),
                    ExpectedDeadline = table.Column<DateTime>(nullable: false),
                    StaffNotify = table.Column<int>(nullable: false),
                    ManagerCopied = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TrackingConcernNote", x => x.Ref);
                });

            migrationBuilder.CreateTable(
                name: "tbl_TrackingConcernManager",
                columns: table => new
                {
                    TrackingConcernManagerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    TrackingConcernNoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TrackingConcernManager", x => x.TrackingConcernManagerId);
                    table.ForeignKey(
                        name: "FK_tbl_TrackingConcernManager_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_TrackingConcernManager_tbl_TrackingConcernNote_TrackingConcernNoteId",
                        column: x => x.TrackingConcernNoteId,
                        principalTable: "tbl_TrackingConcernNote",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_TrackingConcernStaff",
                columns: table => new
                {
                    TrackingConcernManagerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    TrackingConcernNoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TrackingConcernStaff", x => x.TrackingConcernManagerId);
                    table.ForeignKey(
                        name: "FK_tbl_TrackingConcernStaff_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_TrackingConcernStaff_tbl_TrackingConcernNote_TrackingConcernNoteId",
                        column: x => x.TrackingConcernNoteId,
                        principalTable: "tbl_TrackingConcernNote",
                        principalColumn: "Ref",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackingConcernManager_StaffPersonalInfoId",
                table: "tbl_TrackingConcernManager",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackingConcernManager_TrackingConcernNoteId",
                table: "tbl_TrackingConcernManager",
                column: "TrackingConcernNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackingConcernStaff_StaffPersonalInfoId",
                table: "tbl_TrackingConcernStaff",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TrackingConcernStaff_TrackingConcernNoteId",
                table: "tbl_TrackingConcernStaff",
                column: "TrackingConcernNoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_TrackingConcernManager");

            migrationBuilder.DropTable(
                name: "tbl_TrackingConcernStaff");

            migrationBuilder.DropTable(
                name: "tbl_TrackingConcernNote");
        }
    }
}
