using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class ClientRotaType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientRotaType",
                columns: table => new
                {
                    Deleted = table.Column<bool>(nullable: false),
                    ClientRotaTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RotaType = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientRotaType", x => x.ClientRotaTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientRotaType_RotaType",
                table: "tbl_ClientRotaType",
                column: "RotaType",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientRotaType");
        }
    }
}
