using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ShiftBooking_PublishTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PublishTo",
                table: "tbl_ShiftBooking",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublishTo",
                table: "tbl_ShiftBooking",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
