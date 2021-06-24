using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Client_Nutrition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Client_Nutrition",
                columns: table => new
                {
                    NutritionId = table.Column<int>(nullable:false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    STAFFId = table.Column<int>(nullable: false),
                    DATEFROM = table.Column<DateTime>(nullable: false),
                    DATETO = table.Column<DateTime>(nullable: false),
                    MealSpecialNote = table.Column<string>(maxLength: 255, nullable: false),
                    ShoppingSpecialNote = table.Column<string>(maxLength: 255, nullable: false),
                    CleaningSpecialNote = table.Column<string>(maxLength: 255, nullable: false)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_Nutrition", x => x.NutritionId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Nutrition_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Nutrition_tbl_Staff_STAFFId",
                        column: x => x.STAFFId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_Client_Nutrition_NutritionId",
                    table: "tbl_Client_Nutrition",
                    column: "NutritionId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Client_Nutrition");
        }
    }
}
