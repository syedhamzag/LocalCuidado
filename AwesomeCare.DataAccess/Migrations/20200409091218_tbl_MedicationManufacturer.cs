using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_MedicationManufacturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_MedicationManufacturer",
                columns: table => new
                {
                    MedicationManufacturerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    Manufacturer = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MedicationManufacturer", x => x.MedicationManufacturerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedicationManufacturer_Manufacturer",
                table: "tbl_MedicationManufacturer",
                column: "Manufacturer",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_MedicationManufacturer");
        }
    }
}
