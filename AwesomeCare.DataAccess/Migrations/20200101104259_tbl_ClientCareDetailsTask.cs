using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientCareDetailsTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientCareDetailsTask",
                columns: table => new
                {
                    ClientCareDetailsTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientCareDetailsHeadingId = table.Column<int>(nullable: false),
                    Task = table.Column<string>(maxLength: 225, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientCareDetailsTask", x => x.ClientCareDetailsTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientCareDetailsTask_tbl_ClientCareDetailsHeading_ClientCareDetailsHeadingId",
                        column: x => x.ClientCareDetailsHeadingId,
                        principalTable: "tbl_ClientCareDetailsHeading",
                        principalColumn: "ClientCareDetailsHeadingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientCareDetailsTask_ClientCareDetailsHeadingId",
                table: "tbl_ClientCareDetailsTask",
                column: "ClientCareDetailsHeadingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientCareDetailsTask");
        }
    }
}
