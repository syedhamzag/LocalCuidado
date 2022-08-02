using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Updated_MedicationTracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoseGiven",
                table: "tbl_StaffMedRota",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "tbl_StaffMedRota",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "tbl_StaffMedRota",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Measurement",
                table: "tbl_StaffMedRota",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "tbl_StaffMedRota",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoseGiven",
                table: "tbl_StaffMedRota");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "tbl_StaffMedRota");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "tbl_StaffMedRota");

            migrationBuilder.DropColumn(
                name: "Measurement",
                table: "tbl_StaffMedRota");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "tbl_StaffMedRota");
        }
    }
}
