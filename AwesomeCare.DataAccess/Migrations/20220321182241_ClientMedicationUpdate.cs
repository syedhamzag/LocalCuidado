using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class ClientMedicationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientMedImage",
                table: "tbl_ClientMedication",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Means",
                table: "tbl_ClientMedication",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeCritical",
                table: "tbl_ClientMedication",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "tbl_ClientMedication",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientMedImage",
                table: "tbl_ClientMedication");

            migrationBuilder.DropColumn(
                name: "Means",
                table: "tbl_ClientMedication");

            migrationBuilder.DropColumn(
                name: "TimeCritical",
                table: "tbl_ClientMedication");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "tbl_ClientMedication");
        }
    }
}
