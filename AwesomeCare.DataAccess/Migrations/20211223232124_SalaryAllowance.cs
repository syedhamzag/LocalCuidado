using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class SalaryAllowance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryAllowance_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "SalaryAllowance");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryDeduction_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "SalaryDeduction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaryDeduction",
                table: "SalaryDeduction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaryAllowance",
                table: "SalaryAllowance");

            migrationBuilder.RenameTable(
                name: "SalaryDeduction",
                newName: "tbl_SalaryDeduction");

            migrationBuilder.RenameTable(
                name: "SalaryAllowance",
                newName: "tbl_SalaryAllowance");

            migrationBuilder.RenameIndex(
                name: "IX_SalaryDeduction_StaffPersonalInfoId",
                table: "tbl_SalaryDeduction",
                newName: "IX_tbl_SalaryDeduction_StaffPersonalInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_SalaryAllowance_StaffPersonalInfoId",
                table: "tbl_SalaryAllowance",
                newName: "IX_tbl_SalaryAllowance_StaffPersonalInfoId");

            migrationBuilder.AlterColumn<string>(
                name: "Reoccurent",
                table: "tbl_SalaryDeduction",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reoccurent",
                table: "tbl_SalaryAllowance",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_SalaryDeduction",
                table: "tbl_SalaryDeduction",
                column: "SalaryDeductionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_SalaryAllowance",
                table: "tbl_SalaryAllowance",
                column: "SalaryAllowanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_SalaryAllowance_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_SalaryAllowance",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_SalaryDeduction_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_SalaryDeduction",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_SalaryAllowance_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_SalaryAllowance");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_SalaryDeduction_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_SalaryDeduction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_SalaryDeduction",
                table: "tbl_SalaryDeduction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_SalaryAllowance",
                table: "tbl_SalaryAllowance");

            migrationBuilder.RenameTable(
                name: "tbl_SalaryDeduction",
                newName: "SalaryDeduction");

            migrationBuilder.RenameTable(
                name: "tbl_SalaryAllowance",
                newName: "SalaryAllowance");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_SalaryDeduction_StaffPersonalInfoId",
                table: "SalaryDeduction",
                newName: "IX_SalaryDeduction_StaffPersonalInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_SalaryAllowance_StaffPersonalInfoId",
                table: "SalaryAllowance",
                newName: "IX_SalaryAllowance_StaffPersonalInfoId");

            migrationBuilder.AlterColumn<string>(
                name: "Reoccurent",
                table: "SalaryDeduction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Reoccurent",
                table: "SalaryAllowance",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaryDeduction",
                table: "SalaryDeduction",
                column: "SalaryDeductionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaryAllowance",
                table: "SalaryAllowance",
                column: "SalaryAllowanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryAllowance_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "SalaryAllowance",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryDeduction_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "SalaryDeduction",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
