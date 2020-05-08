using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_staffRota_RotaDate_DateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RotaDate",
                table: "tbl_StaffRota",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 15);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RotaDate",
                table: "tbl_StaffRota",
                type: "datetime2",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
