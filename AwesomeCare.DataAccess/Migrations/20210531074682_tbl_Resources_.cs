using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Resources_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Resources_",
                columns: table => new
                {
                    ResourcesId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    PublishTo = table.Column<int>(nullable: false),
                    Heading = table.Column<string>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    PublishBy = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Video = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Resources_Id", x => x.ResourcesId);
                    table.ForeignKey(
                        name: "FK_tbl_Resources__tbl_Client_PublishTo",
                        column: x => x.PublishTo,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                }); ;
            migrationBuilder.CreateIndex(
                name: "IX_tbl_Resources__ResourcesId",
                table: "tbl_Resources_",
                column: "ResourcesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Resources_");
        }
    }
}
