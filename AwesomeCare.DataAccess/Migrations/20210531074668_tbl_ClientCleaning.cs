using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientCleaning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientCleaning",
                columns: table => new
                {
                    CleaningId = table.Column<int>(nullable:false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NutritionId = table.Column<int>(nullable: false),
                    AreasAndItems = table.Column<int>(nullable: false),
                    Details = table.Column<string>(maxLength: 255, nullable: false),
                    SafetyHazard = table.Column<string>(maxLength: 50, nullable: false),
                    LocationOfItem = table.Column<string>(maxLength: 50, nullable: false),
                    DescOfItem = table.Column<string>(maxLength: 50, nullable: false),
                    MinuteAlloted = table.Column<DateTime>(nullable: false),
                    Disposal = table.Column<string>(maxLength: 50, nullable: false),
                    WhereToGet = table.Column<int>(nullable: false),
                    WhereToKeep = table.Column<string>(nullable: false),
                    SEEVIDEO = table.Column<string>(maxLength: 255, nullable: false),
                    Image = table.Column<string>(nullable: false),
                    DAYOFCLEANING = table.Column<string>(maxLength: 50, nullable: false),
                    DATEFROM = table.Column<DateTime>(nullable: false),
                    DATETO = table.Column<DateTime>(nullable: false),
                    STAFFId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientCleaning", x => x.CleaningId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientCleaning_tbl_ClientNutrition_NutritionId",
                        column: x => x.NutritionId,
                        principalTable: "tbl_ClientNutrition",
                        principalColumn: "NutritionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientCleaning_tbl_Staff_STAFFId",
                        column: x => x.STAFFId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientCleaning_CleaningId",
                    table: "tbl_ClientCleaning",
                    column: "CleaningId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientCleaning");
        }
    }
}
