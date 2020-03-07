using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Untowards_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Others",
                table: "tbl_Untowards",
                maxLength: 225,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 225);

            migrationBuilder.AlterColumn<string>(
                name: "HospitalExitDetails",
                table: "tbl_Untowards",
                maxLength: 225,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 225);

            migrationBuilder.AlterColumn<string>(
                name: "HospitalEntryReason",
                table: "tbl_Untowards",
                maxLength: 225,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 225);

            migrationBuilder.AlterColumn<string>(
                name: "ExpectedDateAndTimeOfFeedback",
                table: "tbl_Untowards",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 225);

            migrationBuilder.AlterColumn<string>(
                name: "Attachment",
                table: "tbl_Untowards",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Others",
                table: "tbl_Untowards",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 225,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HospitalExitDetails",
                table: "tbl_Untowards",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 225,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HospitalEntryReason",
                table: "tbl_Untowards",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 225,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpectedDateAndTimeOfFeedback",
                table: "tbl_Untowards",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Attachment",
                table: "tbl_Untowards",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
