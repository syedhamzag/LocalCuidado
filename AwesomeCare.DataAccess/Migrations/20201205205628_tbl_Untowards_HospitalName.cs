using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Untowards_HospitalName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntryHospitalName",
                table: "tbl_Untowards",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExitHospitalName",
                table: "tbl_Untowards",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryHospitalName",
                table: "tbl_Untowards");

            migrationBuilder.DropColumn(
                name: "ExitHospitalName",
                table: "tbl_Untowards");
        }
    }
}
