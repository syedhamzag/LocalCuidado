using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiFoodIntake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_FoodIntakeOfficerToAct",
                columns: table => new
                {
                    FoodIntakeOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    FoodIntakeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FoodIntakeOfficerToAct", x => x.FoodIntakeOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntakeOfficerToAct_tbl_ClientFoodIntake_FoodIntakeId",
                        column: x => x.FoodIntakeId,
                        principalTable: "tbl_ClientFoodIntake",
                        principalColumn: "FoodIntakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntakeOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_FoodIntakeStaffName",
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
                        name: "FK_tbl_FoodIntakeStaffName_tbl_ClientFoodIntake_FoodIntakeId",
                        column: x => x.FoodIntakeId,
                        principalTable: "tbl_ClientFoodIntake",
                        principalColumn: "FoodIntakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntakeStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_FoodIntakePhysician",
                columns: table => new
                {
                    FoodIntakePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    FoodIntakeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FoodIntakePhysician", x => x.FoodIntakePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntakePhysician_tbl_ClientFoodIntake_FoodIntakeId",
                        column: x => x.FoodIntakeId,
                        principalTable: "tbl_ClientFoodIntake",
                        principalColumn: "FoodIntakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntakePhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntakeOfficerToAct_FoodIntakeId",
                table: "tbl_FoodIntakeOfficerToAct",
                column: "FoodIntakeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntakePhysician_FoodIntakeId",
                table: "tbl_FoodIntakePhysician",
                column: "FoodIntakeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntakeStaffName_FoodIntakeId",
                table: "tbl_FoodIntakeStaffName",
                column: "FoodIntakeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_FoodIntakeOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_FoodIntakePhysician");
            migrationBuilder.DropTable(
                name: "tbl_FoodIntakeStaffName");
        }
    }
}
