using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class InterestAndObjective : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DesiredCleaning",
                table: "tbl_InfectionControl",
                newName: "VaccStatus");

            migrationBuilder.RenameColumn(
                name: "Cleaning",
                table: "tbl_InfectionControl",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "CleaningTools",
                table: "tbl_InfectionControl",
                newName: "TestDate");

            migrationBuilder.RenameColumn(
                name: "DryLaundry",
                table: "tbl_InfectionControl",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "DirtyLaundry",
                table: "tbl_InfectionControl",
                newName: "Remarks");

            migrationBuilder.RenameColumn(
                name: "CleaningFreq",
                table: "tbl_InfectionControl",
                newName: "Guideline");

            migrationBuilder.CreateTable(
                name: "tbl_InterestAndObjective",
                columns: table => new
                {
                    GoalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    CareGoal = table.Column<string>(nullable: false),
                    Brief = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_InterestAndObjective", x => x.GoalId);
                    table.ForeignKey(
                        name: "FK_tbl_InterestAndObjective_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Pets",
                columns: table => new
                {
                    PetsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Age = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    PetActivities = table.Column<string>(nullable: false),
                    MealStorage = table.Column<int>(nullable: false),
                    VetVisit = table.Column<int>(nullable: false),
                    PetInsurance = table.Column<int>(nullable: false),
                    PetCare = table.Column<string>(nullable: false),
                    MealPattern = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Pets", x => x.PetsId);
                    table.ForeignKey(
                        name: "FK_tbl_Pets_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Interest",
                columns: table => new
                {
                    InterestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalId = table.Column<int>(nullable: false),
                    LeisureActivity = table.Column<int>(nullable: false),
                    InformalActivity = table.Column<int>(nullable: false),
                    MaintainContact = table.Column<int>(nullable: false),
                    CommunityActivity = table.Column<int>(nullable: false),
                    EventAwarness = table.Column<int>(nullable: false),
                    GoalAndObjective = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Interest", x => x.InterestId);
                    table.ForeignKey(
                        name: "FK_tbl_Interest_tbl_InterestAndObjective_GoalId",
                        column: x => x.GoalId,
                        principalTable: "tbl_InterestAndObjective",
                        principalColumn: "GoalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PersonalityTest",
                columns: table => new
                {
                    TestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalId = table.Column<int>(nullable: false),
                    Question = table.Column<int>(nullable: false),
                    Answer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PersonalityTest", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_tbl_PersonalityTest_tbl_InterestAndObjective_GoalId",
                        column: x => x.GoalId,
                        principalTable: "tbl_InterestAndObjective",
                        principalColumn: "GoalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Interest_GoalId",
                table: "tbl_Interest",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_InterestAndObjective_ClientId",
                table: "tbl_InterestAndObjective",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PersonalityTest_GoalId",
                table: "tbl_PersonalityTest",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Pets_ClientId",
                table: "tbl_Pets",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Interest");

            migrationBuilder.DropTable(
                name: "tbl_PersonalityTest");

            migrationBuilder.DropTable(
                name: "tbl_Pets");

            migrationBuilder.DropTable(
                name: "tbl_InterestAndObjective");

            migrationBuilder.RenameColumn(
                name: "VaccStatus",
                table: "tbl_InfectionControl",
                newName: "DesiredCleaning");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "tbl_InfectionControl",
                newName: "Cleaning");

            migrationBuilder.RenameColumn(
                name: "TestDate",
                table: "tbl_InfectionControl",
                newName: "CleaningTools");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "tbl_InfectionControl",
                newName: "DryLaundry");

            migrationBuilder.RenameColumn(
                name: "Remarks",
                table: "tbl_InfectionControl",
                newName: "DirtyLaundry");

            migrationBuilder.RenameColumn(
                name: "Guideline",
                table: "tbl_InfectionControl",
                newName: "CleaningFreq");
        }
    }
}
