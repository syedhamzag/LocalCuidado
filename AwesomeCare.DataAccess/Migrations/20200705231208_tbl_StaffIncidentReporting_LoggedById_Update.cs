using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffIncidentReporting_LoggedById_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LoggedById",
                table: "tbl_StaffIncidentReporting",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LoggedById",
                table: "tbl_StaffIncidentReporting",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 225);
        }
    }
}
