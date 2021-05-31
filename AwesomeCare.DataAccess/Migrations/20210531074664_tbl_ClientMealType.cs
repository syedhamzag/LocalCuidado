using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientMealType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientMealType",
                columns: table => new
                {
                    Deleted = table.Column<bool>(nullable: false),
                    ClientMealTypeId = table.Column<int>(nullable:false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MealType = table.Column<string>(maxLength: 15, nullable: false)                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientVoice", x => x.ClientMealTypeId);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientMealType_ClientMealTypeId",
                    table: "tbl_ClientMealType",
                    column: "ClientMealTypeId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientMealType");
        }
    }
}
