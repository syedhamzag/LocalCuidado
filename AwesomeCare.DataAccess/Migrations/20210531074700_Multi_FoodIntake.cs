using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_FoodIntake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_FoodIntake_OfficerToAct",
                columns: table => new
                {
                    FoodIntakeOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    FoodIntakeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FoodIntake_OfficerToAct", x => x.FoodIntakeOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_OfficerToAct_tbl_Client_FoodIntake_FoodIntakeId",
                        column: x => x.FoodIntakeId,
                        principalTable: "tbl_Client_FoodIntake",
                        principalColumn: "FoodIntakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_FoodIntake_StaffName",
                columns: table => new
                {
                    FoodIntakeStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    FoodIntakeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FoodIntakeStaffInvolved", x => x.FoodIntakeStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_StaffName_tbl_Client_FoodIntake_FoodIntakeId",
                        column: x => x.FoodIntakeId,
                        principalTable: "tbl_Client_FoodIntake",
                        principalColumn: "FoodIntakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_FoodIntake_Physician",
                columns: table => new
                {
                    FoodIntakePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    FoodIntakeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FoodIntake_Physician", x => x.FoodIntakePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_Physician_tbl_Client_FoodIntake_FoodIntakeId",
                        column: x => x.FoodIntakeId,
                        principalTable: "tbl_Client_FoodIntake",
                        principalColumn: "FoodIntakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntake_OfficerToAct_FoodIntakeId",
                table: "tbl_FoodIntake_OfficerToAct",
                column: "FoodIntakeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntake_Physician_FoodIntakeId",
                table: "tbl_FoodIntake_Physician",
                column: "FoodIntakeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntake_StaffName_FoodIntakeId",
                table: "tbl_FoodIntake_StaffName",
                column: "FoodIntakeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_FoodIntake_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_FoodIntake_Physician");
            migrationBuilder.DropTable(
                name: "tbl_FoodIntake_StaffName");
        }
    }
}
