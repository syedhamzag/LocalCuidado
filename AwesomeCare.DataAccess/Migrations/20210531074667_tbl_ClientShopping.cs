using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientShopping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientShopping",
                columns: table => new
                {
                    ShoppingId = table.Column<int>(nullable:false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NutritionId = table.Column<int>(nullable: false),
                    MeansOfPurchase = table.Column<string>(maxLength: 100, nullable: false),
                    LocationOfPurchase = table.Column<string>(maxLength: 255, nullable: false),
                    Item = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    DAYOFSHOPPING = table.Column<string>(maxLength: 50, nullable: false),
                    DATEFROM = table.Column<DateTime>(nullable: false),
                    DATETO = table.Column<DateTime>(nullable: false),
                    STAFFId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientShopping", x => x.ShoppingId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientShopping_tbl_ClientNutrition_NutritionId",
                        column: x => x.NutritionId,
                        principalTable: "tbl_ClientNutrition",
                        principalColumn: "NutritionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientShopping_tbl_Staff_STAFFId",
                        column: x => x.STAFFId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientShopping_ShoppingId",
                    table: "tbl_ClientShopping",
                    column: "ShoppingId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientShopping");
        }
    }
}
