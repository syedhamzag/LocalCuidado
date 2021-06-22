using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiBowelMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BowelMovementOfficerToAct",
                columns: table => new
                {
                    BowelMovementOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BowelMovementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BowelMovementOfficerToAct", x => x.BowelMovementOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovementOfficerToAct_tbl_ClientBowelMovement_BowelMovementId",
                        column: x => x.BowelMovementId,
                        principalTable: "tbl_ClientBowelMovement",
                        principalColumn: "BowelMovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovementOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BowelMovementStaffName",
                columns: table => new
                {
                    BowelMovementStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BowelMovementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BowelMovementStaffInvolved", x => x.BowelMovementStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovementStaffName_tbl_ClientBowelMovement_BowelMovementId",
                        column: x => x.BowelMovementId,
                        principalTable: "tbl_ClientBowelMovement",
                        principalColumn: "BowelMovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovementStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BowelMovementPhysician",
                columns: table => new
                {
                    BowelMovementPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BowelMovementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BowelMovementPhysician", x => x.BowelMovementPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovementPhysician_tbl_ClientBowelMovement_BowelMovementId",
                        column: x => x.BowelMovementId,
                        principalTable: "tbl_ClientBowelMovement",
                        principalColumn: "BowelMovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovementPhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovementOfficerToAct_BowelMovementId",
                table: "tbl_BowelMovementOfficerToAct",
                column: "BowelMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovementPhysician_BowelMovementId",
                table: "tbl_BowelMovementPhysician",
                column: "BowelMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovementStaffName_BowelMovementId",
                table: "tbl_BowelMovementStaffName",
                column: "BowelMovementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BowelMovementOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_BowelMovementPhysician");
            migrationBuilder.DropTable(
                name: "tbl_BowelMovementStaffName");
        }
    }
}
