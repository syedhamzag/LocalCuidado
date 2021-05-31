using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Client_Address_Geolocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "tbl_Client",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "tbl_Client",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "tbl_Client",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "tbl_Client");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "tbl_Client");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "tbl_Client");
        }
    }
}
