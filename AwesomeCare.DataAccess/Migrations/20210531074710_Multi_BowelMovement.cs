using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_BowelMovement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BowelMovement_OfficerToAct",
                columns: table => new
                {
                    BowelMovementOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BowelMovementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BowelMovement_OfficerToAct", x => x.BowelMovementOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_OfficerToAct_tbl_Client_BowelMovement_BowelMovementId",
                        column: x => x.BowelMovementId,
                        principalTable: "tbl_Client_BowelMovement",
                        principalColumn: "BowelMovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BowelMovement_StaffName",
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
                        name: "FK_tbl_BowelMovement_StaffName_tbl_Client_BowelMovement_BowelMovementId",
                        column: x => x.BowelMovementId,
                        principalTable: "tbl_Client_BowelMovement",
                        principalColumn: "BowelMovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BowelMovement_Physician",
                columns: table => new
                {
                    BowelMovementPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BowelMovementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BowelMovement_Physician", x => x.BowelMovementPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_Physician_tbl_Client_BowelMovement_BowelMovementId",
                        column: x => x.BowelMovementId,
                        principalTable: "tbl_Client_BowelMovement",
                        principalColumn: "BowelMovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovement_OfficerToAct_BowelMovementId",
                table: "tbl_BowelMovement_OfficerToAct",
                column: "BowelMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovement_Physician_BowelMovementId",
                table: "tbl_BowelMovement_Physician",
                column: "BowelMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovement_StaffName_BowelMovementId",
                table: "tbl_BowelMovement_StaffName",
                column: "BowelMovementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BowelMovement_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_BowelMovement_Physician");
            migrationBuilder.DropTable(
                name: "tbl_BowelMovement_StaffName");
        }
    }
}
