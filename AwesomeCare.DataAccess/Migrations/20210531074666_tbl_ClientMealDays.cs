using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientMealDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientMealDay",
                columns: table => new
                {
                    NutritionId = table.Column<int>(nullable: false),
                    ClientMealTypeId = table.Column<int>(nullable: false),
                    MealDayofWeekId = table.Column<int>(nullable: false),
                    TYPEId = table.Column<int>(nullable: false),
                    MEALDETAILS = table.Column<string>(maxLength: 255, nullable: false),
                    HOWTOPREPARE = table.Column<string>(maxLength: 255, nullable: false),
                    SEEVIDEO = table.Column<string>(maxLength: 255, nullable: false),
                    PICTURE = table.Column<string>(nullable: false),
                    ClientMealId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientMealDay", x => x.ClientMealId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMealDay_tbl_ClientNutrition_NutritionId",
                        column: x => x.NutritionId,
                        principalTable: "tbl_ClientNutrition",
                        principalColumn: "NutritionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMealDay_tbl_ClientMealType_ClientMealTypeId",
                        column: x => x.ClientMealTypeId,
                        principalTable: "tbl_ClientMealType",
                        principalColumn: "ClientMealTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMealDay_tbl_RotaDayofWeek_MealDayofWeekId",
                        column: x => x.MealDayofWeekId,
                        principalTable: "tbl_RotaDayofWeek",
                        principalColumn: "RotaDayofWeekId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientMealDay_ClientMealId",
                    table: "tbl_ClientMealDay",
                    column: "ClientMealId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientMealDay");
        }
    }
}
