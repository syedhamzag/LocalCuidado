using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Client_MealType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Client_MealType",
                columns: table => new
                {
                    Deleted = table.Column<bool>(nullable: false),
                    ClientMealTypeId = table.Column<int>(nullable:false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MealType = table.Column<string>(maxLength: 15, nullable: false)                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_MealType", x => x.ClientMealTypeId);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_Client_MealType_ClientMealTypeId",
                    table: "tbl_Client_MealType",
                    column: "ClientMealTypeId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Client_MealType");
        }
    }
}
