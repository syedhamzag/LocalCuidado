using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class PerformanceIndicator_Updaed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_PerformanceIndicator_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_PerformanceIndicator");

            migrationBuilder.DropIndex(
                name: "IX_tbl_PerformanceIndicator_StaffPersonalInfoId",
                table: "tbl_PerformanceIndicator");

            migrationBuilder.DropColumn(
                name: "StaffPersonalInfoId",
                table: "tbl_PerformanceIndicator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffPersonalInfoId",
                table: "tbl_PerformanceIndicator",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PerformanceIndicator_StaffPersonalInfoId",
                table: "tbl_PerformanceIndicator",
                column: "StaffPersonalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_PerformanceIndicator_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_PerformanceIndicator",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
