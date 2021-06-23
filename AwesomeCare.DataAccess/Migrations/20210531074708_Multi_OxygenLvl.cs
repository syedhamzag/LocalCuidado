using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_OxygenLvl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_OxygenLvl_OfficerToAct",
                columns: table => new
                {
                    OxygenLvlOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    OxygenLvlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OxygenLvl_OfficerToAct", x => x.OxygenLvlOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_OfficerToAct_tbl_Client_Oxygenlvl_OxygenLvlId",
                        column: x => x.OxygenLvlId,
                        principalTable: "tbl_Client_Oxygenlvl",
                        principalColumn: "OxygenLvlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OxygenLvl_StaffName",
                columns: table => new
                {
                    OxygenLvlStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    OxygenLvlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OxygenLvlStaffInvolved", x => x.OxygenLvlStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_StaffName_tbl_Client_Oxygenlvl_OxygenLvlId",
                        column: x => x.OxygenLvlId,
                        principalTable: "tbl_Client_Oxygenlvl",
                        principalColumn: "OxygenLvlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OxygenLvl_Physician",
                columns: table => new
                {
                    OxygenLvlPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    OxygenLvlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OxygenLvl_Physician", x => x.OxygenLvlPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_Physician_tbl_Client_Oxygenlvl_OxygenLvlId",
                        column: x => x.OxygenLvlId,
                        principalTable: "tbl_Client_Oxygenlvl",
                        principalColumn: "OxygenLvlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvl_OfficerToAct_OxygenLvlId",
                table: "tbl_OxygenLvl_OfficerToAct",
                column: "OxygenLvlId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvl_Physician_OxygenLvlId",
                table: "tbl_OxygenLvl_Physician",
                column: "OxygenLvlId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvl_StaffName_OxygenLvlId",
                table: "tbl_OxygenLvl_StaffName",
                column: "OxygenLvlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_OxygenLvl_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_OxygenLvl_Physician");
            migrationBuilder.DropTable(
                name: "tbl_OxygenLvl_StaffName");
        }
    }
}
