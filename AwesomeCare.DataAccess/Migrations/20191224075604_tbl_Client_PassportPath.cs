using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Client_PassportPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "tbl_Client",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportFilePath",
                table: "tbl_Client",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassportFilePath",
                table: "tbl_Client");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "tbl_Client",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
