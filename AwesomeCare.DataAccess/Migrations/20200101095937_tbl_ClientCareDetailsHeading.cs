using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientCareDetailsHeading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientCareDetailsHeading",
                columns: table => new
                {
                    Deleted = table.Column<bool>(nullable: false),
                    ClientCareDetailsHeadingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Heading = table.Column<string>(maxLength: 225, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientCareDetailsHeading", x => x.ClientCareDetailsHeadingId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientCareDetailsHeading_Heading",
                table: "tbl_ClientCareDetailsHeading",
                column: "Heading",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientCareDetailsHeading");
        }
    }
}
