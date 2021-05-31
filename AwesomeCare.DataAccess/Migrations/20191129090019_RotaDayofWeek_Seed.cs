using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class RotaDayofWeek_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "tbl_RotaDayofWeek",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "tbl_RotaDayofWeek",
                columns: new[] { "RotaDayofWeekId", "DayofWeek", "Deleted" },
                values: new object[,]
                {
                    { 1, "Monday", false },
                    { 2, "Tuesday", false },
                    { 3, "Wednesday", false },
                    { 4, "Thursday", false },
                    { 5, "Friday", false },
                    { 6, "Saturday", false },
                    { 7, "Sunday", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbl_RotaDayofWeek",
                keyColumn: "RotaDayofWeekId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbl_RotaDayofWeek",
                keyColumn: "RotaDayofWeekId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tbl_RotaDayofWeek",
                keyColumn: "RotaDayofWeekId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tbl_RotaDayofWeek",
                keyColumn: "RotaDayofWeekId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tbl_RotaDayofWeek",
                keyColumn: "RotaDayofWeekId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tbl_RotaDayofWeek",
                keyColumn: "RotaDayofWeekId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tbl_RotaDayofWeek",
                keyColumn: "RotaDayofWeekId",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "tbl_RotaDayofWeek");
        }
    }
}
