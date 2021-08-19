using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Nutrition_Updatd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_AdlObs_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_AdlObs");

            migrationBuilder.AlterColumn<string>(
                name: "SEEVIDEO",
                table: "tbl_Client_MealDay",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "MEALDETAILS",
                table: "tbl_Client_MealDay",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "HOWTOPREPARE",
                table: "tbl_Client_MealDay",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_AdlObs_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_AdlObs",
                column: "StaffId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_AdlObs_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_AdlObs");

            migrationBuilder.AlterColumn<string>(
                name: "SEEVIDEO",
                table: "tbl_Client_MealDay",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "MEALDETAILS",
                table: "tbl_Client_MealDay",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "HOWTOPREPARE",
                table: "tbl_Client_MealDay",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_AdlObs_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_AdlObs",
                column: "StaffId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
