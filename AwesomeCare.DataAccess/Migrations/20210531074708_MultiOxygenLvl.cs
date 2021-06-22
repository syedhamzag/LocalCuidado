using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiOxygenLvl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_OxygenLvlOfficerToAct",
                columns: table => new
                {
                    OxygenLvlOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    OxygenLvlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OxygenLvlOfficerToAct", x => x.OxygenLvlOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvlOfficerToAct_tbl_ClientOxygenLvl_OxygenLvlId",
                        column: x => x.OxygenLvlId,
                        principalTable: "tbl_ClientOxygenLvl",
                        principalColumn: "OxygenLvlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvlOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OxygenLvlStaffName",
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
                        name: "FK_tbl_OxygenLvlStaffName_tbl_ClientOxygenLvl_OxygenLvlId",
                        column: x => x.OxygenLvlId,
                        principalTable: "tbl_ClientOxygenLvl",
                        principalColumn: "OxygenLvlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvlStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OxygenLvlPhysician",
                columns: table => new
                {
                    OxygenLvlPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    OxygenLvlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OxygenLvlPhysician", x => x.OxygenLvlPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvlPhysician_tbl_ClientOxygenLvl_OxygenLvlId",
                        column: x => x.OxygenLvlId,
                        principalTable: "tbl_ClientOxygenLvl",
                        principalColumn: "OxygenLvlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvlPhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvlOfficerToAct_OxygenLvlId",
                table: "tbl_OxygenLvlOfficerToAct",
                column: "OxygenLvlId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvlPhysician_OxygenLvlId",
                table: "tbl_OxygenLvlPhysician",
                column: "OxygenLvlId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvlStaffName_OxygenLvlId",
                table: "tbl_OxygenLvlStaffName",
                column: "OxygenLvlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_OxygenLvlOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_OxygenLvlPhysician");
            migrationBuilder.DropTable(
                name: "tbl_OxygenLvlStaffName");
        }
    }
}
