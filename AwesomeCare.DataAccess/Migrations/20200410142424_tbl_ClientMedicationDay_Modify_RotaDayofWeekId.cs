using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientMedicationDay_Modify_RotaDayofWeekId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name:"IX_tbl_ClientMedicationDay_RotaDayofWeekId",table: "tbl_ClientMedicationDay");
            migrationBuilder.CreateIndex(
               name: "IX_tbl_ClientMedicationDay_RotaDayofWeekId",
               table: "tbl_ClientMedicationDay",
               column: "RotaDayofWeekId",
               unique: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
