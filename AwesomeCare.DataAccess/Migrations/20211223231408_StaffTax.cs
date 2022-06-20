using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class StaffTax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsKeyWorker",
                table: "tbl_StaffPersonalInfo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalaryAllowance",
                columns: table => new
                {
                    SalaryAllowanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    AllowanceType = table.Column<int>(nullable: false),
                    Reoccurent = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Percentage = table.Column<decimal>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryAllowance", x => x.SalaryAllowanceId);
                    table.ForeignKey(
                        name: "FK_SalaryAllowance_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryDeduction",
                columns: table => new
                {
                    SalaryDeductionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    AllowanceType = table.Column<int>(nullable: false),
                    Reoccurent = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Percentage = table.Column<decimal>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryDeduction", x => x.SalaryDeductionId);
                    table.ForeignKey(
                        name: "FK_SalaryDeduction_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffTax",
                columns: table => new
                {
                    StaffTaxId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    NI = table.Column<decimal>(nullable: false),
                    Remarks = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffTax", x => x.StaffTaxId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffTax_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryAllowance_StaffPersonalInfoId",
                table: "SalaryAllowance",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryDeduction_StaffPersonalInfoId",
                table: "SalaryDeduction",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffTax_StaffPersonalInfoId",
                table: "tbl_StaffTax",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryAllowance");

            migrationBuilder.DropTable(
                name: "SalaryDeduction");

            migrationBuilder.DropTable(
                name: "tbl_StaffTax");

            migrationBuilder.DropColumn(
                name: "IsKeyWorker",
                table: "tbl_StaffPersonalInfo");
        }
    }
}
