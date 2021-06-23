using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_Voice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Voice_OfficerToAct",
                columns: table => new
                {
                    VoiceOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Voice_OfficerToAct", x => x.VoiceOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_OfficerToAct_tbl_Client_Voice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_Client_Voice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Voice_CallerName",
                columns: table => new
                {
                    VoiceCallerNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Voice_CallerName", x => x.VoiceCallerNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_CallerName_tbl_Client_Voice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_Client_Voice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_CallerName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateTable(
                name: "tbl_Voice_GoodStaff",
                columns: table => new
                {
                    VoiceGoodStaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Voice_GoodStaff", x => x.VoiceGoodStaffId);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_GoodStaff_tbl_Client_Voice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_Client_Voice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_GoodStaff_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateTable(
                name: "tbl_Voice_PoorStaff",
                columns: table => new
                {
                    VoicePoorStaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Voice_PoorStaff", x => x.VoicePoorStaffId);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_PoorStaff_tbl_Client_Voice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_Client_Voice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_PoorStaff_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
                


            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_OfficerToAct_VoiceId",
                table: "tbl_Voice_OfficerToAct",
                column: "VoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_CallerName_VoiceId",
                table: "tbl_Voice_CallerName",
                column: "VoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_GoodStaff_VoiceId",
                table: "tbl_Voice_GoodStaff",
                column: "VoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_PoorStaff_VoiceId",
                table: "tbl_Voice_PoorStaff",
                column: "VoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Voice_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_Voice_CallerName");
            migrationBuilder.DropTable(
                name: "tbl_Voice_GoodStaff");
            migrationBuilder.DropTable(
                name: "tbl_Voice_PoorStaff");
        }
    }
}
