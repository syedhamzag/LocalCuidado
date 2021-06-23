using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiVoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_VoiceOfficerToAct",
                columns: table => new
                {
                    VoiceOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_VoiceOfficerToAct", x => x.VoiceOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_VoiceOfficerToAct_tbl_ClientVoice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_ClientVoice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_VoiceOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_VoiceCallerName",
                columns: table => new
                {
                    VoiceCallerNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_VoiceCallerName", x => x.VoiceCallerNameId);
                    table.ForeignKey(
                        name: "FK_tbl_VoiceCallerName_tbl_ClientVoice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_ClientVoice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_VoiceCallerName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateTable(
                name: "tbl_VoiceGoodStaff",
                columns: table => new
                {
                    VoiceGoodStaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_VoiceGoodStaff", x => x.VoiceGoodStaffId);
                    table.ForeignKey(
                        name: "FK_tbl_VoiceGoodStaff_tbl_ClientVoice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_ClientVoice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_VoiceGoodStaff_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateTable(
                name: "tbl_VoicePoorStaff",
                columns: table => new
                {
                    VoicePoorStaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_VoicePoorStaff", x => x.VoicePoorStaffId);
                    table.ForeignKey(
                        name: "FK_tbl_VoicePoorStaff_tbl_ClientVoice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_ClientVoice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_VoicePoorStaff_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
                


            migrationBuilder.CreateIndex(
                name: "IX_tbl_VoiceOfficerToAct_VoiceId",
                table: "tbl_VoiceOfficerToAct",
                column: "VoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_VoiceCallerName_VoiceId",
                table: "tbl_VoiceCallerName",
                column: "VoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_VoiceGoodStaff_VoiceId",
                table: "tbl_VoiceGoodStaff",
                column: "VoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_VoicePoorStaff_VoiceId",
                table: "tbl_VoicePoorStaff",
                column: "VoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_VoiceOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_VoiceCallerName");
            migrationBuilder.DropTable(
                name: "tbl_VoiceGoodStaff");
            migrationBuilder.DropTable(
                name: "tbl_VoicePoorStaff");
        }
    }
}
