using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientCareDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientCareDetails",
                columns: table => new
                {
                    ClientCareDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientCareDetailsTaskId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Risk = table.Column<string>(maxLength: 250, nullable: false),
                    Mitigation = table.Column<string>(maxLength: 250, nullable: true),
                    Location = table.Column<string>(maxLength: 250, nullable: true),
                    Remark = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientCareDetails", x => x.ClientCareDetailsId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientCareDetails_tbl_ClientCareDetailsTask_ClientCareDetailsTaskId",
                        column: x => x.ClientCareDetailsTaskId,
                        principalTable: "tbl_ClientCareDetailsTask",
                        principalColumn: "ClientCareDetailsTaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientCareDetails_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientCareDetails_ClientCareDetailsTaskId",
                table: "tbl_ClientCareDetails",
                column: "ClientCareDetailsTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientCareDetails_ClientId",
                table: "tbl_ClientCareDetails",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientCareDetails");
        }
    }
}
