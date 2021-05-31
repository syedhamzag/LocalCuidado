using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Client_UniqueId_NotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "tbl_Client",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueId",
                table: "tbl_Client",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
