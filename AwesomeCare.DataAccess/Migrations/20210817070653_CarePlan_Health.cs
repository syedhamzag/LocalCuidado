using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class CarePlan_Health : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Balance",
                columns: table => new
                {
                    BalanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Mobility = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Balance", x => x.BalanceId);
                    table.ForeignKey(
                        name: "FK_tbl_Balance_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CarePlanNutrition",
                columns: table => new
                {
                    NutritionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    FoodStorage = table.Column<int>(nullable: false),
                    ServingMeal = table.Column<string>(nullable: false),
                    WhenRestock = table.Column<string>(nullable: false),
                    WhoRestock = table.Column<int>(nullable: false),
                    SpecialDiet = table.Column<string>(nullable: false),
                    DrinkType = table.Column<string>(nullable: false),
                    AvoidFood = table.Column<string>(nullable: false),
                    ThingsILike = table.Column<int>(nullable: false),
                    FoodIntake = table.Column<int>(nullable: false),
                    MealPreparation = table.Column<int>(nullable: false),
                    EatingDifficulty = table.Column<int>(nullable: false),
                    RiskMitigations = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CarePlanNutrition", x => x.NutritionId);
                    table.ForeignKey(
                        name: "FK_tbl_CarePlanNutrition_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HealthAndLiving",
                columns: table => new
                {
                    HLId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    BriefHealth = table.Column<string>(nullable: false),
                    ObserveHealth = table.Column<string>(nullable: false),
                    WakeUp = table.Column<string>(nullable: false),
                    CareSupport = table.Column<string>(nullable: false),
                    MovingAndHandling = table.Column<string>(nullable: false),
                    SupportToBed = table.Column<string>(nullable: false),
                    DehydrationRisk = table.Column<int>(nullable: false),
                    LifeStyle = table.Column<int>(nullable: false),
                    PressureSore = table.Column<int>(nullable: false),
                    ContinenceIssue = table.Column<int>(nullable: false),
                    ContinenceNeeds = table.Column<string>(nullable: false),
                    ContinenceSource = table.Column<string>(nullable: false),
                    ConstraintRequired = table.Column<int>(nullable: false),
                    ConstraintDetails = table.Column<string>(nullable: false),
                    ConstraintAttachment = table.Column<string>(nullable: false),
                    MeansOfComm = table.Column<int>(nullable: false),
                    Smoking = table.Column<int>(nullable: false),
                    AbilityToRead = table.Column<int>(nullable: false),
                    TextFontSize = table.Column<int>(nullable: false),
                    Email = table.Column<int>(nullable: false),
                    FinanceManagement = table.Column<int>(nullable: false),
                    PostalService = table.Column<int>(nullable: false),
                    LetterOpening = table.Column<int>(nullable: false),
                    ShoppingRequired = table.Column<int>(nullable: false),
                    SpecialCleaning = table.Column<int>(nullable: false),
                    LaundaryRequired = table.Column<int>(nullable: false),
                    VideoCallRequired = table.Column<int>(nullable: false),
                    EatingWithStaff = table.Column<int>(nullable: false),
                    AllowChats = table.Column<int>(nullable: false),
                    TeaChocolateCoffee = table.Column<int>(nullable: false),
                    NeighbourInvolment = table.Column<int>(nullable: false),
                    FamilyUpdate = table.Column<int>(nullable: false),
                    AlcoholicDrink = table.Column<int>(nullable: false),
                    TVandMusic = table.Column<int>(nullable: false),
                    SpecialCaution = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HealthAndLiving", x => x.HLId);
                    table.ForeignKey(
                        name: "FK_tbl_HealthAndLiving_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HistoryOfFall",
                columns: table => new
                {
                    HistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Cause = table.Column<string>(nullable: false),
                    Prevention = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HistoryOfFall", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_tbl_HistoryOfFall_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PhysicalAbility",
                columns: table => new
                {
                    PhysicalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Mobility = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PhysicalAbility", x => x.PhysicalId);
                    table.ForeignKey(
                        name: "FK_tbl_PhysicalAbility_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SpecialHealthAndMedication",
                columns: table => new
                {
                    SHMId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    AdminLvl = table.Column<int>(nullable: false),
                    MedicationAllergy = table.Column<int>(nullable: false),
                    FamilyMeds = table.Column<int>(nullable: false),
                    LeftoutMedicine = table.Column<int>(nullable: false),
                    NameFormMedicaiton = table.Column<int>(nullable: false),
                    WhoAdminister = table.Column<int>(nullable: false),
                    MedsGPOrder = table.Column<int>(nullable: false),
                    PharmaMARChart = table.Column<int>(nullable: false),
                    TempMARChart = table.Column<int>(nullable: false),
                    MedKeyCode = table.Column<string>(nullable: false),
                    FamilyReturnMed = table.Column<int>(nullable: false),
                    AccessMedication = table.Column<int>(nullable: false),
                    NoMedAccess = table.Column<int>(nullable: false),
                    MedAccessDenial = table.Column<int>(nullable: false),
                    PNRMedReq = table.Column<int>(nullable: false),
                    PNRDoses = table.Column<int>(nullable: false),
                    PNRMedsAdmin = table.Column<int>(nullable: false),
                    OverdoseContact = table.Column<int>(nullable: false),
                    PNRMedsMissing = table.Column<string>(nullable: false),
                    MedicationStorage = table.Column<string>(nullable: false),
                    SpecialStorage = table.Column<int>(nullable: false),
                    PNRMedList = table.Column<string>(nullable: false),
                    Consent = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    By = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SpecialHealthAndMedication", x => x.SHMId);
                    table.ForeignKey(
                        name: "FK_tbl_SpecialHealthAndMedication_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SpecialHealthCondition",
                columns: table => new
                {
                    HealthCondId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    ConditionName = table.Column<string>(nullable: false),
                    SourceInformation = table.Column<string>(nullable: false),
                    FeelingBeforeIncident = table.Column<string>(nullable: false),
                    FeelingAfterIncident = table.Column<string>(nullable: false),
                    Frequency = table.Column<string>(nullable: false),
                    LivingActivities = table.Column<string>(nullable: false),
                    Trigger = table.Column<string>(nullable: false),
                    ClientAction = table.Column<string>(nullable: false),
                    ClinicRecommendation = table.Column<string>(nullable: false),
                    LifestyleSupport = table.Column<string>(nullable: false),
                    PlanningHealthCondition = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SpecialHealthCondition", x => x.HealthCondId);
                    table.ForeignKey(
                        name: "FK_tbl_SpecialHealthCondition_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Balance_ClientId",
                table: "tbl_Balance",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CarePlanNutrition_ClientId",
                table: "tbl_CarePlanNutrition",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HealthAndLiving_ClientId",
                table: "tbl_HealthAndLiving",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HistoryOfFall_ClientId",
                table: "tbl_HistoryOfFall",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PhysicalAbility_ClientId",
                table: "tbl_PhysicalAbility",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SpecialHealthAndMedication_ClientId",
                table: "tbl_SpecialHealthAndMedication",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SpecialHealthCondition_ClientId",
                table: "tbl_SpecialHealthCondition",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Balance");

            migrationBuilder.DropTable(
                name: "tbl_CarePlanNutrition");

            migrationBuilder.DropTable(
                name: "tbl_HealthAndLiving");

            migrationBuilder.DropTable(
                name: "tbl_HistoryOfFall");

            migrationBuilder.DropTable(
                name: "tbl_PhysicalAbility");

            migrationBuilder.DropTable(
                name: "tbl_SpecialHealthAndMedication");

            migrationBuilder.DropTable(
                name: "tbl_SpecialHealthCondition");
        }
    }
}
