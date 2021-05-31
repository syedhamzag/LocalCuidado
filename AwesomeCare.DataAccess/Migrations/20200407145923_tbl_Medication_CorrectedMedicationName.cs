using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Medication_CorrectedMedicationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicatiionName",
                table: "tbl_Medication");

            migrationBuilder.AddColumn<string>(
                name: "MedicationName",
                table: "tbl_Medication",
                maxLength: 225,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicationName",
                table: "tbl_Medication");

            migrationBuilder.AddColumn<string>(
                name: "MedicatiionName",
                table: "tbl_Medication",
                type: "nvarchar(225)",
                maxLength: 225,
                nullable: false,
                defaultValue: "");
        }
    }
}
