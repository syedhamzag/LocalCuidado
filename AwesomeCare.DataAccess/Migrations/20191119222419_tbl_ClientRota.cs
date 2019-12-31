using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientRota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientRota",
                columns: table => new
                {
                    Deleted = table.Column<bool>(nullable: false),
                    RotaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumberOfStaff = table.Column<int>(nullable: false),
                    RotaName = table.Column<string>(maxLength: 225, nullable: false),
                    Gender = table.Column<string>(maxLength: 15, nullable: false),
                    Area = table.Column<string>(maxLength: 225, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientRota", x => x.RotaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientRota");
        }
    }
}
