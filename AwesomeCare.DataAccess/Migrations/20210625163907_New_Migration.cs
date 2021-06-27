using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class New_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Client_BloodCoagulationRecord",
                columns: table => new
                {
                    BloodRecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Indication = table.Column<int>(nullable: false),
                    TargetINR = table.Column<int>(nullable: false),
                    TargetINRAttach = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CurrentDose = table.Column<int>(nullable: false),
                    INR = table.Column<int>(nullable: false),
                    NewDose = table.Column<int>(nullable: false),
                    NewINR = table.Column<int>(nullable: false),
                    BloodStatus = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    PhysicianResponce = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_BloodCoagulationRecord", x => x.BloodRecordId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_BloodCoagulationRecord_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_BloodPressure",
                columns: table => new
                {
                    BloodPressureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    GoalSystolic = table.Column<int>(nullable: false),
                    GoalDiastolic = table.Column<int>(nullable: false),
                    ReadingSystolic = table.Column<int>(nullable: false),
                    ReadingDiastolic = table.Column<int>(nullable: false),
                    StatusImage = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_BloodPressure", x => x.BloodPressureId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_BloodPressure_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_BMIChart",
                columns: table => new
                {
                    BMIChartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Weight = table.Column<string>(nullable: false),
                    NumberRange = table.Column<int>(nullable: false),
                    SeeChart = table.Column<int>(nullable: false),
                    SeeChartAttach = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: false),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_BMIChart", x => x.BMIChartId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_BMIChart_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_BodyTemp",
                columns: table => new
                {
                    BodyTempId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    TargetTemp = table.Column<int>(nullable: false),
                    TargetTempAttach = table.Column<string>(nullable: true),
                    CurrentReading = table.Column<string>(nullable: false),
                    SeeChart = table.Column<int>(nullable: false),
                    SeeChartAttach = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: false),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_BodyTemp", x => x.BodyTempId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_BodyTemp_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_BowelMovement",
                columns: table => new
                {
                    BowelMovementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TypeAttach = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    Color = table.Column<int>(nullable: false),
                    ColorAttach = table.Column<string>(nullable: true),
                    StatusImage = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: false),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_BowelMovement", x => x.BowelMovementId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_BowelMovement_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_ComplainRegister",
                columns: table => new
                {
                    ComplainId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    LINK = table.Column<string>(nullable: false),
                    IRFNUMBER = table.Column<string>(name: "IRFNUMBER ", nullable: false),
                    INCIDENTDATE = table.Column<DateTime>(nullable: false),
                    DATERECIEVED = table.Column<DateTime>(nullable: false),
                    DATEOFACKNOWLEDGEMENT = table.Column<DateTime>(nullable: false),
                    SOURCEOFCOMPLAINTS = table.Column<string>(nullable: false),
                    COMPLAINANTCONTACT = table.Column<string>(nullable: false),
                    CONCERNSRAISED = table.Column<string>(nullable: false),
                    DUEDATE = table.Column<DateTime>(nullable: false),
                    LETTERTOSTAFF = table.Column<string>(nullable: false),
                    INVESTIGATIONOUTCOME = table.Column<string>(nullable: false),
                    ACTIONTAKEN = table.Column<string>(nullable: false),
                    FINALRESPONSETOFAMILY = table.Column<string>(nullable: false),
                    ROOTCAUSE = table.Column<string>(nullable: false),
                    REMARK = table.Column<string>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    EvidenceFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_ComplainRegister", x => x.ComplainId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_ComplainRegister_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_EyeHealthMonitoring",
                columns: table => new
                {
                    EyeHealthId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    ToolUsed = table.Column<int>(nullable: false),
                    ToolUsedAttach = table.Column<string>(nullable: true),
                    MethodUsed = table.Column<int>(nullable: false),
                    MethodUsedAttach = table.Column<string>(nullable: true),
                    TargetSet = table.Column<int>(nullable: false),
                    CurrentScore = table.Column<int>(nullable: false),
                    PatientGlasses = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    StatusImage = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_EyeHealthMonitoring", x => x.EyeHealthId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_EyeHealthMonitoring_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_FoodIntake",
                columns: table => new
                {
                    FoodIntakeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Goal = table.Column<int>(nullable: false),
                    CurrentIntake = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    StatusImage = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_FoodIntake", x => x.FoodIntakeId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_FoodIntake_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_HeartRate",
                columns: table => new
                {
                    HeartRateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    TargetHR = table.Column<int>(nullable: false),
                    TargetHRAttach = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    GenderAttach = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    BeatsPerSeconds = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    SeeChart = table.Column<int>(nullable: false),
                    SeeChartAttach = table.Column<string>(nullable: true),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_HeartRate", x => x.HeartRateId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_HeartRate_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_LogAudit",
                columns: table => new
                {
                    LogAuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextDueDate = table.Column<DateTime>(nullable: false),
                    IsCareExpected = table.Column<string>(nullable: false),
                    IsCareDifference = table.Column<string>(nullable: false),
                    ProperDocumentation = table.Column<string>(nullable: false),
                    ImproperDocumentation = table.Column<string>(nullable: false),
                    Communication = table.Column<string>(nullable: false),
                    ThinkingServiceUsers = table.Column<string>(nullable: false),
                    ThinkingStaff = table.Column<string>(nullable: false),
                    ThinkingStaffStop = table.Column<string>(nullable: false),
                    Observations = table.Column<string>(nullable: false),
                    NameOfAuditor = table.Column<string>(nullable: false),
                    ActionRecommended = table.Column<string>(nullable: false),
                    ActionTaken = table.Column<string>(nullable: false),
                    EvidenceOfActionTaken = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    RepeatOfIncident = table.Column<int>(nullable: false),
                    RotCause = table.Column<string>(nullable: false),
                    LessonLearntAndShared = table.Column<string>(nullable: false),
                    LogURL = table.Column<string>(nullable: false),
                    EvidenceFilePath = table.Column<string>(nullable: true),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_LogAudit", x => x.LogAuditId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_LogAudit_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_LogAudit_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_MealType",
                columns: table => new
                {
                    ClientMealTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    MealType = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_MealType", x => x.ClientMealTypeId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_MedAudit",
                columns: table => new
                {
                    MedAuditId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextDueDate = table.Column<DateTime>(nullable: false),
                    GapsInAdmistration = table.Column<int>(nullable: false),
                    RightsOfMedication = table.Column<string>(nullable: false),
                    MarChartReview = table.Column<int>(nullable: false),
                    MedicationConcern = table.Column<int>(nullable: false),
                    HardCopyReview = table.Column<int>(nullable: false),
                    ThinkingServiceUsers = table.Column<string>(nullable: false),
                    MedicationSupplyEfficiency = table.Column<int>(nullable: false),
                    MedicationInfoUploadEefficiency = table.Column<int>(nullable: false),
                    Observations = table.Column<string>(nullable: false),
                    ActionRecommended = table.Column<string>(nullable: false),
                    ActionTaken = table.Column<string>(nullable: false),
                    EvidenceOfActionTaken = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    RepeatOfIncident = table.Column<int>(nullable: false),
                    RotCause = table.Column<string>(nullable: false),
                    LessonLearntAndShared = table.Column<string>(nullable: false),
                    LogURL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: true),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_MedAudit", x => x.MedAuditId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_MedAudit_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_MedAudit_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_MgtVisit",
                columns: table => new
                {
                    VisitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    RateServiceRecieving = table.Column<int>(nullable: false),
                    RateManagers = table.Column<int>(nullable: false),
                    HowToComplain = table.Column<int>(nullable: false),
                    ServiceRecommended = table.Column<int>(nullable: false),
                    ImprovementExpect = table.Column<string>(nullable: false),
                    Observation = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    ActionsTakenByMPCC = table.Column<string>(nullable: false),
                    EvidenceOfActionTaken = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    RotCause = table.Column<string>(nullable: false),
                    LessonLearntAndShared = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: true),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_MgtVisit", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_MgtVisit_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_MgtVisit_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_Nutrition",
                columns: table => new
                {
                    NutritionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    DATEFROM = table.Column<DateTime>(nullable: false),
                    DATETO = table.Column<DateTime>(nullable: false),
                    MealSpecialNote = table.Column<string>(nullable: false),
                    ShoppingSpecialNote = table.Column<string>(nullable: false),
                    CleaningSpecialNote = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_Nutrition", x => x.NutritionId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Nutrition_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Nutrition_tbl_StaffPersonalInfo_StaffId",
                        column: x => x.StaffId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_Oxygenlvl",
                columns: table => new
                {
                    OxygenLvlId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    TargetOxygen = table.Column<int>(nullable: false),
                    TargetOxygenAttach = table.Column<string>(nullable: true),
                    CurrentReading = table.Column<string>(nullable: false),
                    SeeChart = table.Column<int>(nullable: false),
                    SeeChartAttach = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: false),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_Oxygenlvl", x => x.OxygenLvlId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Oxygenlvl_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_PainChart",
                columns: table => new
                {
                    PainChartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TypeAttach = table.Column<string>(nullable: true),
                    Location = table.Column<int>(nullable: false),
                    LocationAttach = table.Column<string>(nullable: true),
                    PainLvl = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    StatusImage = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_PainChart", x => x.PainChartId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_PainChart_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_Program",
                columns: table => new
                {
                    ProgramId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    ProgramOfChoice = table.Column<int>(nullable: false),
                    DaysOfChoice = table.Column<int>(nullable: false),
                    PlaceLocationProgram = table.Column<int>(nullable: false),
                    DetailsOfProgram = table.Column<int>(nullable: false),
                    Observation = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: true),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_Program", x => x.ProgramId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Program_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Program_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_PulseRate",
                columns: table => new
                {
                    PulseRateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    TargetPulse = table.Column<int>(nullable: false),
                    TargetPulseAttach = table.Column<string>(nullable: true),
                    CurrentPulse = table.Column<string>(nullable: false),
                    Chart = table.Column<string>(nullable: false),
                    SeeChart = table.Column<int>(nullable: false),
                    SeeChartAttach = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: false),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_PulseRate", x => x.PulseRateId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_PulseRate_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_Seizure",
                columns: table => new
                {
                    SeizureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    SeizureType = table.Column<int>(nullable: false),
                    SeizureTypeAttach = table.Column<string>(nullable: true),
                    SeizureLength = table.Column<int>(nullable: false),
                    SeizureLengthAttach = table.Column<string>(nullable: true),
                    Often = table.Column<int>(nullable: false),
                    WhatHappened = table.Column<string>(nullable: false),
                    StatusImage = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_Seizure", x => x.SeizureId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Seizure_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_ServiceWatch",
                columns: table => new
                {
                    WatchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Incident = table.Column<int>(nullable: false),
                    Details = table.Column<int>(nullable: false),
                    Contact = table.Column<int>(nullable: false),
                    Observation = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: true),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_ServiceWatch", x => x.WatchId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_ServiceWatch_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_ServiceWatch_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_Voice",
                columns: table => new
                {
                    VoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    RateServiceRecieving = table.Column<int>(nullable: false),
                    RateStaffAttending = table.Column<int>(nullable: false),
                    OfficeStaffSupport = table.Column<int>(nullable: false),
                    AreasOfImprovements = table.Column<string>(nullable: false),
                    SomethingSpecial = table.Column<string>(nullable: false),
                    InterestedInPrograms = table.Column<int>(nullable: false),
                    HealthGoalShortTerm = table.Column<string>(nullable: false),
                    HealthGoalLongTerm = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    ActionsTakenByMPCC = table.Column<string>(nullable: false),
                    EvidenceOfActionTaken = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    RotCause = table.Column<string>(nullable: false),
                    LessonLearntAndShared = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: true),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_Voice", x => x.VoiceId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Voice_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Voice_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_WoundCare",
                columns: table => new
                {
                    WoundCareId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Goal = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TypeAttach = table.Column<string>(nullable: true),
                    UlcerStage = table.Column<int>(nullable: false),
                    UlcerStageAttach = table.Column<string>(nullable: true),
                    Measurment = table.Column<int>(nullable: false),
                    MeasurementAttach = table.Column<string>(nullable: true),
                    PainLvl = table.Column<int>(nullable: false),
                    Location = table.Column<int>(nullable: false),
                    LocationAttach = table.Column<string>(nullable: true),
                    WoundCause = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    StatusImage = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_WoundCare", x => x.WoundCareId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_WoundCare_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Enotice_",
                columns: table => new
                {
                    EnoticeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    PublishTo = table.Column<int>(nullable: false),
                    Heading = table.Column<string>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    PublishBy = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Video = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Enotice_", x => x.EnoticeId);
                    table.ForeignKey(
                        name: "FK_tbl_Enotice__tbl_Client_PublishTo",
                        column: x => x.PublishTo,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Incoming_Meds",
                columns: table => new
                {
                    IncomingMedsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<int>(nullable: false),
                    StaffName = table.Column<string>(nullable: false),
                    StartDate = table.Column<string>(nullable: false),
                    ChartImage = table.Column<string>(nullable: false),
                    MedsImage = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Incoming_Meds", x => x.IncomingMedsId);
                    table.ForeignKey(
                        name: "FK_tbl_Incoming_Meds_tbl_Client_UserName",
                        column: x => x.UserName,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Resources_",
                columns: table => new
                {
                    ResourcesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    PublishTo = table.Column<int>(nullable: false),
                    Heading = table.Column<string>(nullable: false),
                    Note = table.Column<string>(nullable: false),
                    PublishBy = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Video = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Resources_", x => x.ResourcesId);
                    table.ForeignKey(
                        name: "FK_tbl_Resources__tbl_Client_PublishTo",
                        column: x => x.PublishTo,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Staff_AdlObs",
                columns: table => new
                {
                    ObservationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(maxLength: 255, nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    UnderstandingofEquipment = table.Column<int>(nullable: false),
                    UnderstandingofService = table.Column<int>(nullable: false),
                    UnderstandingofControl = table.Column<int>(nullable: false),
                    FivePrinciples = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(maxLength: 255, nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Staff_AdlObs", x => x.ObservationID);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_AdlObs_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_AdlObs_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Staff_KeyWorkerVoice",
                columns: table => new
                {
                    KeyWorkerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    NotComfortableServices = table.Column<int>(nullable: false),
                    ServicesRequiresTime = table.Column<int>(nullable: false),
                    ServicesRequiresServices = table.Column<int>(nullable: false),
                    WellSupportedServices = table.Column<int>(nullable: false),
                    ChangesWeNeed = table.Column<string>(nullable: false),
                    NutritionalChanges = table.Column<string>(nullable: false),
                    HealthAndWellNessChanges = table.Column<string>(nullable: false),
                    MedicationChanges = table.Column<string>(nullable: false),
                    MovingAndHandling = table.Column<string>(nullable: false),
                    RiskAssessment = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Staff_KeyWorkerVoice", x => x.KeyWorkerId);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_KeyWorkerVoice_tbl_Client_ServicesRequiresServices",
                        column: x => x.ServicesRequiresServices,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_KeyWorkerVoice_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Staff_MedCompObs",
                columns: table => new
                {
                    MedCompId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    UnderstandingofMedication = table.Column<int>(nullable: false),
                    UnderstandingofRights = table.Column<int>(nullable: false),
                    ReadingMedicalPrescriptions = table.Column<int>(nullable: false),
                    CarePlan = table.Column<int>(nullable: false),
                    RateStaff = table.Column<int>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Staff_MedCompObs", x => x.MedCompId);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_MedCompObs_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_MedCompObs_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Staff_OneToOne",
                columns: table => new
                {
                    OneToOneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Purpose = table.Column<string>(nullable: false),
                    PreviousSupervision = table.Column<int>(nullable: false),
                    StaffImprovedInAreas = table.Column<string>(nullable: false),
                    CurrentEventArea = table.Column<string>(nullable: false),
                    StaffConclusion = table.Column<string>(nullable: false),
                    DecisionsReached = table.Column<string>(nullable: false),
                    ImprovementRecorded = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Staff_OneToOne", x => x.OneToOneId);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_OneToOne_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Staff_Reference",
                columns: table => new
                {
                    StaffReferenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ApplicantRole = table.Column<int>(nullable: false),
                    DateofEmployement = table.Column<int>(nullable: false),
                    DateofExit = table.Column<string>(nullable: false),
                    RehireStaff = table.Column<string>(nullable: false),
                    Relationship = table.Column<string>(nullable: false),
                    TeamWork = table.Column<int>(nullable: false),
                    Integrity = table.Column<int>(nullable: false),
                    Knowledgeable = table.Column<int>(nullable: false),
                    WorkUnderPressure = table.Column<int>(nullable: false),
                    Caring = table.Column<int>(nullable: false),
                    PreviousExperience = table.Column<int>(nullable: false),
                    RefreeName = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Contact = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    ConfirmedBy = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Staff_Reference", x => x.StaffReferenceId);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_Reference_tbl_Client_ApplicantRole",
                        column: x => x.ApplicantRole,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_Reference_tbl_StaffPersonalInfo_ConfirmedBy",
                        column: x => x.ConfirmedBy,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Staff_SpotCheck",
                columns: table => new
                {
                    SpotCheckId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    StaffArriveOnTime = table.Column<int>(nullable: false),
                    StaffDressCode = table.Column<int>(nullable: false),
                    AreaComments = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Staff_SpotCheck", x => x.SpotCheckId);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_SpotCheck_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_SpotCheck_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Staff_SupervisionAppraisal",
                columns: table => new
                {
                    StaffSupervisionAppraisalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    StaffRating = table.Column<int>(nullable: false),
                    ProfessionalDevelopment = table.Column<int>(nullable: false),
                    StaffComplaints = table.Column<int>(nullable: false),
                    FiveStarRating = table.Column<string>(nullable: false),
                    StaffDevelopment = table.Column<string>(nullable: false),
                    StaffAbility = table.Column<string>(nullable: false),
                    NoAbilityToSupport = table.Column<string>(nullable: false),
                    CondourAndWhistleBlowing = table.Column<string>(nullable: false),
                    NoCondourAndWhistleBlowing = table.Column<string>(nullable: false),
                    StaffSupportAreas = table.Column<int>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Staff_SupervisionAppraisal", x => x.StaffSupervisionAppraisalId);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_SupervisionAppraisal_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Staff_Survey",
                columns: table => new
                {
                    StaffSurveyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    AdequateTrainingReceived = table.Column<int>(nullable: false),
                    HealthCareServicesSatisfaction = table.Column<int>(nullable: false),
                    SupportFromCompany = table.Column<int>(nullable: false),
                    CompanyManagement = table.Column<int>(nullable: false),
                    AccessToPolicies = table.Column<int>(nullable: false),
                    WorkEnvironmentSuggestions = table.Column<string>(nullable: false),
                    AreaRequiringImprovements = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Staff_Survey", x => x.StaffSurveyId);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_Survey_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Whisttle_Blower",
                columns: table => new
                {
                    WhisttleBlowerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<int>(nullable: false),
                    StaffName = table.Column<string>(nullable: false),
                    IncidentDate = table.Column<string>(nullable: false),
                    Happening = table.Column<string>(nullable: false),
                    Evidence = table.Column<string>(nullable: false),
                    Witness = table.Column<int>(nullable: false),
                    LikeCalling = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Whisttle_Blower", x => x.WhisttleBlowerId);
                    table.ForeignKey(
                        name: "FK_tbl_Whisttle_Blower_tbl_Client_UserName",
                        column: x => x.UserName,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodCoag_OfficerToAct",
                columns: table => new
                {
                    BloodCoagOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodCoag_OfficerToAct", x => x.BloodCoagOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_OfficerToAct_tbl_Client_BloodCoagulationRecord_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_Client_BloodCoagulationRecord",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodCoag_Physician",
                columns: table => new
                {
                    BloodCoagPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodCoag_Physician", x => x.BloodCoagPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_Physician_tbl_Client_BloodCoagulationRecord_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_Client_BloodCoagulationRecord",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodCoag_StaffName",
                columns: table => new
                {
                    BloodCoagStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodCoag_StaffName", x => x.BloodCoagStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_StaffName_tbl_Client_BloodCoagulationRecord_BloodRecordId",
                        column: x => x.BloodRecordId,
                        principalTable: "tbl_Client_BloodCoagulationRecord",
                        principalColumn: "BloodRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodCoag_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodPressure_OfficerToAct",
                columns: table => new
                {
                    BloodPressureOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodPressureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodPressure_OfficerToAct", x => x.BloodPressureOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_OfficerToAct_tbl_Client_BloodPressure_BloodPressureId",
                        column: x => x.BloodPressureId,
                        principalTable: "tbl_Client_BloodPressure",
                        principalColumn: "BloodPressureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodPressure_Physician",
                columns: table => new
                {
                    BloodPressurePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodPressureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodPressure_Physician", x => x.BloodPressurePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_Physician_tbl_Client_BloodPressure_BloodPressureId",
                        column: x => x.BloodPressureId,
                        principalTable: "tbl_Client_BloodPressure",
                        principalColumn: "BloodPressureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BloodPressure_StaffName",
                columns: table => new
                {
                    BloodPressureStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BloodPressureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BloodPressure_StaffName", x => x.BloodPressureStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_StaffName_tbl_Client_BloodPressure_BloodPressureId",
                        column: x => x.BloodPressureId,
                        principalTable: "tbl_Client_BloodPressure",
                        principalColumn: "BloodPressureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BloodPressure_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BMIChart_OfficerToAct",
                columns: table => new
                {
                    BMIChartOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BMIChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BMIChart_OfficerToAct", x => x.BMIChartOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_OfficerToAct_tbl_Client_BMIChart_BMIChartId",
                        column: x => x.BMIChartId,
                        principalTable: "tbl_Client_BMIChart",
                        principalColumn: "BMIChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BMIChart_Physician",
                columns: table => new
                {
                    BMIChartPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BMIChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BMIChart_Physician", x => x.BMIChartPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_Physician_tbl_Client_BMIChart_BMIChartId",
                        column: x => x.BMIChartId,
                        principalTable: "tbl_Client_BMIChart",
                        principalColumn: "BMIChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BMIChart_StaffName",
                columns: table => new
                {
                    BMIChartStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BMIChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BMIChart_StaffName", x => x.BMIChartStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_StaffName_tbl_Client_BMIChart_BMIChartId",
                        column: x => x.BMIChartId,
                        principalTable: "tbl_Client_BMIChart",
                        principalColumn: "BMIChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BMIChart_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BodyTemp_OfficerToAct",
                columns: table => new
                {
                    BodyTempOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BodyTempId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BodyTemp_OfficerToAct", x => x.BodyTempOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_OfficerToAct_tbl_Client_BodyTemp_BodyTempId",
                        column: x => x.BodyTempId,
                        principalTable: "tbl_Client_BodyTemp",
                        principalColumn: "BodyTempId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BodyTemp_Physician",
                columns: table => new
                {
                    BodyTempPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BodyTempId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BodyTemp_Physician", x => x.BodyTempPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_Physician_tbl_Client_BodyTemp_BodyTempId",
                        column: x => x.BodyTempId,
                        principalTable: "tbl_Client_BodyTemp",
                        principalColumn: "BodyTempId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BodyTemp_StaffName",
                columns: table => new
                {
                    BodyTempStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BodyTempId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BodyTemp_StaffName", x => x.BodyTempStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_StaffName_tbl_Client_BodyTemp_BodyTempId",
                        column: x => x.BodyTempId,
                        principalTable: "tbl_Client_BodyTemp",
                        principalColumn: "BodyTempId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BodyTemp_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BowelMovement_OfficerToAct",
                columns: table => new
                {
                    BowelMovementOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BowelMovementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BowelMovement_OfficerToAct", x => x.BowelMovementOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_OfficerToAct_tbl_Client_BowelMovement_BowelMovementId",
                        column: x => x.BowelMovementId,
                        principalTable: "tbl_Client_BowelMovement",
                        principalColumn: "BowelMovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BowelMovement_Physician",
                columns: table => new
                {
                    BowelMovementPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BowelMovementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BowelMovement_Physician", x => x.BowelMovementPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_Physician_tbl_Client_BowelMovement_BowelMovementId",
                        column: x => x.BowelMovementId,
                        principalTable: "tbl_Client_BowelMovement",
                        principalColumn: "BowelMovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BowelMovement_StaffName",
                columns: table => new
                {
                    BowelMovementStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BowelMovementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BowelMovement_StaffName", x => x.BowelMovementStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_StaffName_tbl_Client_BowelMovement_BowelMovementId",
                        column: x => x.BowelMovementId,
                        principalTable: "tbl_Client_BowelMovement",
                        principalColumn: "BowelMovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_BowelMovement_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Complain_OfficerToAct",
                columns: table => new
                {
                    ComplainOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ComplainId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Complain_OfficerToAct", x => x.ComplainOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Complain_OfficerToAct_tbl_Client_ComplainRegister_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "tbl_Client_ComplainRegister",
                        principalColumn: "ComplainId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Complain_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Complain_StaffName",
                columns: table => new
                {
                    ComplainStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ComplainId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Complain_StaffName", x => x.ComplainStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Complain_StaffName_tbl_Client_ComplainRegister_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "tbl_Client_ComplainRegister",
                        principalColumn: "ComplainId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Complain_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EyeHealth_OfficerToAct",
                columns: table => new
                {
                    EyeHealthOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    EyeHealthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EyeHealth_OfficerToAct", x => x.EyeHealthOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_OfficerToAct_tbl_Client_EyeHealthMonitoring_EyeHealthId",
                        column: x => x.EyeHealthId,
                        principalTable: "tbl_Client_EyeHealthMonitoring",
                        principalColumn: "EyeHealthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EyeHealth_Physician",
                columns: table => new
                {
                    EyeHealthPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    EyeHealthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EyeHealth_Physician", x => x.EyeHealthPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_Physician_tbl_Client_EyeHealthMonitoring_EyeHealthId",
                        column: x => x.EyeHealthId,
                        principalTable: "tbl_Client_EyeHealthMonitoring",
                        principalColumn: "EyeHealthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EyeHealth_StaffName",
                columns: table => new
                {
                    EyeHealthStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    EyeHealthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EyeHealth_StaffName", x => x.EyeHealthStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_StaffName_tbl_Client_EyeHealthMonitoring_EyeHealthId",
                        column: x => x.EyeHealthId,
                        principalTable: "tbl_Client_EyeHealthMonitoring",
                        principalColumn: "EyeHealthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_EyeHealth_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_FoodIntake_OfficerToAct",
                columns: table => new
                {
                    FoodIntakeOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    FoodIntakeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FoodIntake_OfficerToAct", x => x.FoodIntakeOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_OfficerToAct_tbl_Client_FoodIntake_FoodIntakeId",
                        column: x => x.FoodIntakeId,
                        principalTable: "tbl_Client_FoodIntake",
                        principalColumn: "FoodIntakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_FoodIntake_Physician",
                columns: table => new
                {
                    FoodIntakePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    FoodIntakeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FoodIntake_Physician", x => x.FoodIntakePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_Physician_tbl_Client_FoodIntake_FoodIntakeId",
                        column: x => x.FoodIntakeId,
                        principalTable: "tbl_Client_FoodIntake",
                        principalColumn: "FoodIntakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_FoodIntake_StaffName",
                columns: table => new
                {
                    FoodIntakeStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    FoodIntakeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FoodIntake_StaffName", x => x.FoodIntakeStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_StaffName_tbl_Client_FoodIntake_FoodIntakeId",
                        column: x => x.FoodIntakeId,
                        principalTable: "tbl_Client_FoodIntake",
                        principalColumn: "FoodIntakeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_FoodIntake_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HeartRate_OfficerToAct",
                columns: table => new
                {
                    HeartRateOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    HeartRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HeartRate_OfficerToAct", x => x.HeartRateOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_OfficerToAct_tbl_Client_HeartRate_HeartRateId",
                        column: x => x.HeartRateId,
                        principalTable: "tbl_Client_HeartRate",
                        principalColumn: "HeartRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HeartRate_Physician",
                columns: table => new
                {
                    HeartRatePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    HeartRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HeartRate_Physician", x => x.HeartRatePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_Physician_tbl_Client_HeartRate_HeartRateId",
                        column: x => x.HeartRateId,
                        principalTable: "tbl_Client_HeartRate",
                        principalColumn: "HeartRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HeartRate_StaffName",
                columns: table => new
                {
                    HeartRateStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    HeartRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HeartRate_StaffName", x => x.HeartRateStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_StaffName_tbl_Client_HeartRate_HeartRateId",
                        column: x => x.HeartRateId,
                        principalTable: "tbl_Client_HeartRate",
                        principalColumn: "HeartRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_HeartRate_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_LogAudit_OfficerToAct",
                columns: table => new
                {
                    LogAuditOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    LogAuditId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_LogAudit_OfficerToAct", x => x.LogAuditOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_LogAudit_OfficerToAct_tbl_Client_LogAudit_LogAuditId",
                        column: x => x.LogAuditId,
                        principalTable: "tbl_Client_LogAudit",
                        principalColumn: "LogAuditId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_LogAudit_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_MedAudit_AuditorName",
                columns: table => new
                {
                    MedAuditStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    MedAuditId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MedAudit_AuditorName", x => x.MedAuditStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_MedAudit_AuditorName_tbl_Client_MedAudit_MedAuditId",
                        column: x => x.MedAuditId,
                        principalTable: "tbl_Client_MedAudit",
                        principalColumn: "MedAuditId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_MedAudit_AuditorName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_MedAudit_OfficerToAct",
                columns: table => new
                {
                    MedAuditOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    MedAuditId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MedAudit_OfficerToAct", x => x.MedAuditOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_MedAudit_OfficerToAct_tbl_Client_MedAudit_MedAuditId",
                        column: x => x.MedAuditId,
                        principalTable: "tbl_Client_MedAudit",
                        principalColumn: "MedAuditId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_MedAudit_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Visit_OfficerToAct",
                columns: table => new
                {
                    VisitOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VisitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Visit_OfficerToAct", x => x.VisitOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Visit_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Visit_OfficerToAct_tbl_Client_MgtVisit_VisitId",
                        column: x => x.VisitId,
                        principalTable: "tbl_Client_MgtVisit",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Visit_StaffName",
                columns: table => new
                {
                    VisitStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VisitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Visit_StaffName", x => x.VisitStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Visit_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Visit_StaffName_tbl_Client_MgtVisit_VisitId",
                        column: x => x.VisitId,
                        principalTable: "tbl_Client_MgtVisit",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_Cleaning",
                columns: table => new
                {
                    CleaningId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    STAFFId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_Cleaning", x => x.CleaningId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Cleaning_tbl_Client_Nutrition_NutritionId",
                        column: x => x.NutritionId,
                        principalTable: "tbl_Client_Nutrition",
                        principalColumn: "NutritionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Cleaning_tbl_StaffPersonalInfo_STAFFId",
                        column: x => x.STAFFId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_MealDay",
                columns: table => new
                {
                    ClientMealId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NutritionId = table.Column<int>(nullable: false),
                    MealDayofWeekId = table.Column<int>(nullable: false),
                    ClientMealTypeId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    MEALDETAILS = table.Column<string>(maxLength: 255, nullable: false),
                    HOWTOPREPARE = table.Column<string>(maxLength: 255, nullable: false),
                    SEEVIDEO = table.Column<string>(maxLength: 255, nullable: false),
                    PICTURE = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_MealDay", x => x.ClientMealId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_MealDay_tbl_Client_MealType_ClientMealTypeId",
                        column: x => x.ClientMealTypeId,
                        principalTable: "tbl_Client_MealType",
                        principalColumn: "ClientMealTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_MealDay_tbl_RotaDayofWeek_MealDayofWeekId",
                        column: x => x.MealDayofWeekId,
                        principalTable: "tbl_RotaDayofWeek",
                        principalColumn: "RotaDayofWeekId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_MealDay_tbl_Client_Nutrition_NutritionId",
                        column: x => x.NutritionId,
                        principalTable: "tbl_Client_Nutrition",
                        principalColumn: "NutritionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Client_Shopping",
                columns: table => new
                {
                    ShoppingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    STAFFId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_Shopping", x => x.ShoppingId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Shopping_tbl_Client_Nutrition_NutritionId",
                        column: x => x.NutritionId,
                        principalTable: "tbl_Client_Nutrition",
                        principalColumn: "NutritionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Client_Shopping_tbl_StaffPersonalInfo_STAFFId",
                        column: x => x.STAFFId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId");
                });

            migrationBuilder.CreateTable(
                name: "tbl_OxygenLvl_OfficerToAct",
                columns: table => new
                {
                    OxygenLvlOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    OxygenLvlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OxygenLvl_OfficerToAct", x => x.OxygenLvlOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_OfficerToAct_tbl_Client_Oxygenlvl_OxygenLvlId",
                        column: x => x.OxygenLvlId,
                        principalTable: "tbl_Client_Oxygenlvl",
                        principalColumn: "OxygenLvlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OxygenLvl_Physician",
                columns: table => new
                {
                    OxygenLvlPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    OxygenLvlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OxygenLvl_Physician", x => x.OxygenLvlPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_Physician_tbl_Client_Oxygenlvl_OxygenLvlId",
                        column: x => x.OxygenLvlId,
                        principalTable: "tbl_Client_Oxygenlvl",
                        principalColumn: "OxygenLvlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OxygenLvl_StaffName",
                columns: table => new
                {
                    OxygenLvlStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    OxygenLvlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OxygenLvl_StaffName", x => x.OxygenLvlStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_StaffName_tbl_Client_Oxygenlvl_OxygenLvlId",
                        column: x => x.OxygenLvlId,
                        principalTable: "tbl_Client_Oxygenlvl",
                        principalColumn: "OxygenLvlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OxygenLvl_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PainChart_OfficerToAct",
                columns: table => new
                {
                    PainChartOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PainChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PainChart_OfficerToAct", x => x.PainChartOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_OfficerToAct_tbl_Client_PainChart_PainChartId",
                        column: x => x.PainChartId,
                        principalTable: "tbl_Client_PainChart",
                        principalColumn: "PainChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PainChart_Physician",
                columns: table => new
                {
                    PainChartPhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PainChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PainChart_Physician", x => x.PainChartPhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_Physician_tbl_Client_PainChart_PainChartId",
                        column: x => x.PainChartId,
                        principalTable: "tbl_Client_PainChart",
                        principalColumn: "PainChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PainChart_StaffName",
                columns: table => new
                {
                    PainChartStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PainChartId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PainChart_StaffName", x => x.PainChartStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_StaffName_tbl_Client_PainChart_PainChartId",
                        column: x => x.PainChartId,
                        principalTable: "tbl_Client_PainChart",
                        principalColumn: "PainChartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PainChart_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Program_OfficerToAct",
                columns: table => new
                {
                    ProgramOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ProgramId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Program_OfficerToAct", x => x.ProgramOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Program_OfficerToAct_tbl_Client_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "tbl_Client_Program",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Program_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PulseRate_OfficerToAct",
                columns: table => new
                {
                    PulseRateOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PulseRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PulseRate_OfficerToAct", x => x.PulseRateOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_OfficerToAct_tbl_Client_PulseRate_PulseRateId",
                        column: x => x.PulseRateId,
                        principalTable: "tbl_Client_PulseRate",
                        principalColumn: "PulseRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PulseRate_Physician",
                columns: table => new
                {
                    PulseRatePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PulseRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PulseRate_Physician", x => x.PulseRatePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_Physician_tbl_Client_PulseRate_PulseRateId",
                        column: x => x.PulseRateId,
                        principalTable: "tbl_Client_PulseRate",
                        principalColumn: "PulseRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PulseRate_StaffName",
                columns: table => new
                {
                    PulseRateStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    PulseRateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PulseRate_StaffName", x => x.PulseRateStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_StaffName_tbl_Client_PulseRate_PulseRateId",
                        column: x => x.PulseRateId,
                        principalTable: "tbl_Client_PulseRate",
                        principalColumn: "PulseRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_PulseRate_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Seizure_OfficerToAct",
                columns: table => new
                {
                    SeizureOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SeizureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Seizure_OfficerToAct", x => x.SeizureOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_OfficerToAct_tbl_Client_Seizure_SeizureId",
                        column: x => x.SeizureId,
                        principalTable: "tbl_Client_Seizure",
                        principalColumn: "SeizureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Seizure_Physician",
                columns: table => new
                {
                    SeizurePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SeizureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Seizure_Physician", x => x.SeizurePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_Physician_tbl_Client_Seizure_SeizureId",
                        column: x => x.SeizureId,
                        principalTable: "tbl_Client_Seizure",
                        principalColumn: "SeizureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Seizure_StaffName",
                columns: table => new
                {
                    SeizureStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SeizureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Seizure_StaffName", x => x.SeizureStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_StaffName_tbl_Client_Seizure_SeizureId",
                        column: x => x.SeizureId,
                        principalTable: "tbl_Client_Seizure",
                        principalColumn: "SeizureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Seizure_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Service_OfficerToAct",
                columns: table => new
                {
                    ServiceOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Service_OfficerToAct", x => x.ServiceOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Service_OfficerToAct_tbl_Client_ServiceWatch_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "tbl_Client_ServiceWatch",
                        principalColumn: "WatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Service_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Service_StaffName",
                columns: table => new
                {
                    ServiceStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Service_StaffName", x => x.ServiceStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Service_StaffName_tbl_Client_ServiceWatch_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "tbl_Client_ServiceWatch",
                        principalColumn: "WatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Service_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Voice_CallerName",
                columns: table => new
                {
                    VoiceCallerNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Voice_CallerName", x => x.VoiceCallerNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_CallerName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_CallerName_tbl_Client_Voice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_Client_Voice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Voice_GoodStaff",
                columns: table => new
                {
                    VoiceGoodStaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Voice_GoodStaff", x => x.VoiceGoodStaffId);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_GoodStaff_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_GoodStaff_tbl_Client_Voice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_Client_Voice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Voice_OfficerToAct",
                columns: table => new
                {
                    VoiceOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Voice_OfficerToAct", x => x.VoiceOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_OfficerToAct_tbl_Client_Voice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_Client_Voice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Voice_PoorStaff",
                columns: table => new
                {
                    VoicePoorStaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    VoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Voice_PoorStaff", x => x.VoicePoorStaffId);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_PoorStaff_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Voice_PoorStaff_tbl_Client_Voice_VoiceId",
                        column: x => x.VoiceId,
                        principalTable: "tbl_Client_Voice",
                        principalColumn: "VoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WoundCare_OfficerToAct",
                columns: table => new
                {
                    WoundCareOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WoundCareId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WoundCare_OfficerToAct", x => x.WoundCareOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_OfficerToAct_tbl_Client_WoundCare_WoundCareId",
                        column: x => x.WoundCareId,
                        principalTable: "tbl_Client_WoundCare",
                        principalColumn: "WoundCareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WoundCare_Physician",
                columns: table => new
                {
                    WoundCarePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WoundCareId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WoundCare_Physician", x => x.WoundCarePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_Physician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_Physician_tbl_Client_WoundCare_WoundCareId",
                        column: x => x.WoundCareId,
                        principalTable: "tbl_Client_WoundCare",
                        principalColumn: "WoundCareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WoundCare_StaffName",
                columns: table => new
                {
                    WoundCareStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WoundCareId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WoundCare_StaffName", x => x.WoundCareStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_WoundCare_StaffName_tbl_Client_WoundCare_WoundCareId",
                        column: x => x.WoundCareId,
                        principalTable: "tbl_Client_WoundCare",
                        principalColumn: "WoundCareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AdlObs_OfficerToAct",
                columns: table => new
                {
                    AdlObsOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ObservationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AdlObs_OfficerToAct", x => x.AdlObsOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_AdlObs_OfficerToAct_tbl_Staff_AdlObs_ObservationId",
                        column: x => x.ObservationId,
                        principalTable: "tbl_Staff_AdlObs",
                        principalColumn: "ObservationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_AdlObs_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_KeyWorker_OfficerToAct",
                columns: table => new
                {
                    KeyWorkerOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    KeyWorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_KeyWorker_OfficerToAct", x => x.KeyWorkerOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorker_OfficerToAct_tbl_Staff_KeyWorkerVoice_KeyWorkerId",
                        column: x => x.KeyWorkerId,
                        principalTable: "tbl_Staff_KeyWorkerVoice",
                        principalColumn: "KeyWorkerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorker_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_KeyWorker_StaffName",
                columns: table => new
                {
                    KeyWorkerWorkteamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    KeyWorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_KeyWorker_StaffName", x => x.KeyWorkerWorkteamId);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorker_StaffName_tbl_Staff_KeyWorkerVoice_KeyWorkerId",
                        column: x => x.KeyWorkerId,
                        principalTable: "tbl_Staff_KeyWorkerVoice",
                        principalColumn: "KeyWorkerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_KeyWorker_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_MedComp_OfficerToAct",
                columns: table => new
                {
                    MedCompOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    MedCompId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MedComp_OfficerToAct", x => x.MedCompOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_MedComp_OfficerToAct_tbl_Staff_MedCompObs_MedCompId",
                        column: x => x.MedCompId,
                        principalTable: "tbl_Staff_MedCompObs",
                        principalColumn: "MedCompId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_MedComp_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OneToOne_OfficerToAct",
                columns: table => new
                {
                    OneToOneOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    OneToOneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OneToOne_OfficerToAct", x => x.OneToOneOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_OneToOne_OfficerToAct_tbl_Staff_OneToOne_OneToOneId",
                        column: x => x.OneToOneId,
                        principalTable: "tbl_Staff_OneToOne",
                        principalColumn: "OneToOneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OneToOne_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SpotCheck_OfficerToAct",
                columns: table => new
                {
                    SpotCheckOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SpotCheckId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SpotCheck_OfficerToAct", x => x.SpotCheckOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_SpotCheck_OfficerToAct_tbl_Staff_SpotCheck_SpotCheckId",
                        column: x => x.SpotCheckId,
                        principalTable: "tbl_Staff_SpotCheck",
                        principalColumn: "SpotCheckId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_SpotCheck_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Supervision_OfficerToAct",
                columns: table => new
                {
                    SupervisionOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    StaffSupervisionAppraisalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Supervision_OfficerToAct", x => x.SupervisionOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Supervision_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Supervision_OfficerToAct_tbl_Staff_SupervisionAppraisal_StaffSupervisionAppraisalId",
                        column: x => x.StaffSupervisionAppraisalId,
                        principalTable: "tbl_Staff_SupervisionAppraisal",
                        principalColumn: "StaffSupervisionAppraisalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Supervision_StaffName",
                columns: table => new
                {
                    SupervisionWorkteamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    StaffSupervisionAppraisalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Supervision_StaffName", x => x.SupervisionWorkteamId);
                    table.ForeignKey(
                        name: "FK_tbl_Supervision_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Supervision_StaffName_tbl_Staff_SupervisionAppraisal_StaffSupervisionAppraisalId",
                        column: x => x.StaffSupervisionAppraisalId,
                        principalTable: "tbl_Staff_SupervisionAppraisal",
                        principalColumn: "StaffSupervisionAppraisalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Survey_OfficerToAct",
                columns: table => new
                {
                    SurveyOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    StaffSurveyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Survey_OfficerToAct", x => x.SurveyOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Survey_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Survey_OfficerToAct_tbl_Staff_Survey_StaffSurveyId",
                        column: x => x.StaffSurveyId,
                        principalTable: "tbl_Staff_Survey",
                        principalColumn: "StaffSurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Survey_StaffName",
                columns: table => new
                {
                    SurveyWorkteamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    StaffSurveyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Survey_StaffName", x => x.SurveyWorkteamId);
                    table.ForeignKey(
                        name: "FK_tbl_Survey_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Survey_StaffName_tbl_Staff_Survey_StaffSurveyId",
                        column: x => x.StaffSurveyId,
                        principalTable: "tbl_Staff_Survey",
                        principalColumn: "StaffSurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AdlObs_OfficerToAct_ObservationId",
                table: "tbl_AdlObs_OfficerToAct",
                column: "ObservationId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AdlObs_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_AdlObs_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoag_OfficerToAct_BloodRecordId",
                table: "tbl_BloodCoag_OfficerToAct",
                column: "BloodRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoag_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_BloodCoag_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoag_Physician_BloodRecordId",
                table: "tbl_BloodCoag_Physician",
                column: "BloodRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoag_Physician_StaffPersonalInfoId",
                table: "tbl_BloodCoag_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoag_StaffName_BloodRecordId",
                table: "tbl_BloodCoag_StaffName",
                column: "BloodRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodCoag_StaffName_StaffPersonalInfoId",
                table: "tbl_BloodCoag_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressure_OfficerToAct_BloodPressureId",
                table: "tbl_BloodPressure_OfficerToAct",
                column: "BloodPressureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressure_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_BloodPressure_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressure_Physician_BloodPressureId",
                table: "tbl_BloodPressure_Physician",
                column: "BloodPressureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressure_Physician_StaffPersonalInfoId",
                table: "tbl_BloodPressure_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressure_StaffName_BloodPressureId",
                table: "tbl_BloodPressure_StaffName",
                column: "BloodPressureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BloodPressure_StaffName_StaffPersonalInfoId",
                table: "tbl_BloodPressure_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChart_OfficerToAct_BMIChartId",
                table: "tbl_BMIChart_OfficerToAct",
                column: "BMIChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChart_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_BMIChart_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChart_Physician_BMIChartId",
                table: "tbl_BMIChart_Physician",
                column: "BMIChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChart_Physician_StaffPersonalInfoId",
                table: "tbl_BMIChart_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChart_StaffName_BMIChartId",
                table: "tbl_BMIChart_StaffName",
                column: "BMIChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BMIChart_StaffName_StaffPersonalInfoId",
                table: "tbl_BMIChart_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTemp_OfficerToAct_BodyTempId",
                table: "tbl_BodyTemp_OfficerToAct",
                column: "BodyTempId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTemp_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_BodyTemp_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTemp_Physician_BodyTempId",
                table: "tbl_BodyTemp_Physician",
                column: "BodyTempId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTemp_Physician_StaffPersonalInfoId",
                table: "tbl_BodyTemp_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTemp_StaffName_BodyTempId",
                table: "tbl_BodyTemp_StaffName",
                column: "BodyTempId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BodyTemp_StaffName_StaffPersonalInfoId",
                table: "tbl_BodyTemp_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovement_OfficerToAct_BowelMovementId",
                table: "tbl_BowelMovement_OfficerToAct",
                column: "BowelMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovement_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_BowelMovement_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovement_Physician_BowelMovementId",
                table: "tbl_BowelMovement_Physician",
                column: "BowelMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovement_Physician_StaffPersonalInfoId",
                table: "tbl_BowelMovement_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovement_StaffName_BowelMovementId",
                table: "tbl_BowelMovement_StaffName",
                column: "BowelMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BowelMovement_StaffName_StaffPersonalInfoId",
                table: "tbl_BowelMovement_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_BloodCoagulationRecord_ClientId",
                table: "tbl_Client_BloodCoagulationRecord",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_BloodPressure_ClientId",
                table: "tbl_Client_BloodPressure",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_BMIChart_ClientId",
                table: "tbl_Client_BMIChart",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_BodyTemp_ClientId",
                table: "tbl_Client_BodyTemp",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_BowelMovement_ClientId",
                table: "tbl_Client_BowelMovement",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Cleaning_NutritionId",
                table: "tbl_Client_Cleaning",
                column: "NutritionId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Cleaning_STAFFId",
                table: "tbl_Client_Cleaning",
                column: "STAFFId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_ComplainRegister_ClientId",
                table: "tbl_Client_ComplainRegister",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_EyeHealthMonitoring_ClientId",
                table: "tbl_Client_EyeHealthMonitoring",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_FoodIntake_ClientId",
                table: "tbl_Client_FoodIntake",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_HeartRate_ClientId",
                table: "tbl_Client_HeartRate",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_LogAudit_ClientId",
                table: "tbl_Client_LogAudit",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_LogAudit_StaffPersonalInfoId",
                table: "tbl_Client_LogAudit",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_MealDay_ClientMealTypeId",
                table: "tbl_Client_MealDay",
                column: "ClientMealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_MealDay_MealDayofWeekId",
                table: "tbl_Client_MealDay",
                column: "MealDayofWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_MealDay_NutritionId",
                table: "tbl_Client_MealDay",
                column: "NutritionId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_MealType_MealType",
                table: "tbl_Client_MealType",
                column: "MealType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_MedAudit_ClientId",
                table: "tbl_Client_MedAudit",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_MedAudit_StaffPersonalInfoId",
                table: "tbl_Client_MedAudit",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_MgtVisit_ClientId",
                table: "tbl_Client_MgtVisit",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_MgtVisit_StaffPersonalInfoId",
                table: "tbl_Client_MgtVisit",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Nutrition_ClientId",
                table: "tbl_Client_Nutrition",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Nutrition_StaffId",
                table: "tbl_Client_Nutrition",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Oxygenlvl_ClientId",
                table: "tbl_Client_Oxygenlvl",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_PainChart_ClientId",
                table: "tbl_Client_PainChart",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Program_ClientId",
                table: "tbl_Client_Program",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Program_StaffPersonalInfoId",
                table: "tbl_Client_Program",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_PulseRate_ClientId",
                table: "tbl_Client_PulseRate",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Seizure_ClientId",
                table: "tbl_Client_Seizure",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_ServiceWatch_ClientId",
                table: "tbl_Client_ServiceWatch",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_ServiceWatch_StaffPersonalInfoId",
                table: "tbl_Client_ServiceWatch",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Shopping_NutritionId",
                table: "tbl_Client_Shopping",
                column: "NutritionId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Shopping_STAFFId",
                table: "tbl_Client_Shopping",
                column: "STAFFId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Voice_ClientId",
                table: "tbl_Client_Voice",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_Voice_StaffPersonalInfoId",
                table: "tbl_Client_Voice",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_WoundCare_ClientId",
                table: "tbl_Client_WoundCare",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Complain_OfficerToAct_ComplainId",
                table: "tbl_Complain_OfficerToAct",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Complain_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_Complain_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Complain_StaffName_ComplainId",
                table: "tbl_Complain_StaffName",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Complain_StaffName_StaffPersonalInfoId",
                table: "tbl_Complain_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Enotice__PublishTo",
                table: "tbl_Enotice_",
                column: "PublishTo");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealth_OfficerToAct_EyeHealthId",
                table: "tbl_EyeHealth_OfficerToAct",
                column: "EyeHealthId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealth_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_EyeHealth_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealth_Physician_EyeHealthId",
                table: "tbl_EyeHealth_Physician",
                column: "EyeHealthId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealth_Physician_StaffPersonalInfoId",
                table: "tbl_EyeHealth_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealth_StaffName_EyeHealthId",
                table: "tbl_EyeHealth_StaffName",
                column: "EyeHealthId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EyeHealth_StaffName_StaffPersonalInfoId",
                table: "tbl_EyeHealth_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntake_OfficerToAct_FoodIntakeId",
                table: "tbl_FoodIntake_OfficerToAct",
                column: "FoodIntakeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntake_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_FoodIntake_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntake_Physician_FoodIntakeId",
                table: "tbl_FoodIntake_Physician",
                column: "FoodIntakeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntake_Physician_StaffPersonalInfoId",
                table: "tbl_FoodIntake_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntake_StaffName_FoodIntakeId",
                table: "tbl_FoodIntake_StaffName",
                column: "FoodIntakeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FoodIntake_StaffName_StaffPersonalInfoId",
                table: "tbl_FoodIntake_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRate_OfficerToAct_HeartRateId",
                table: "tbl_HeartRate_OfficerToAct",
                column: "HeartRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRate_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_HeartRate_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRate_Physician_HeartRateId",
                table: "tbl_HeartRate_Physician",
                column: "HeartRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRate_Physician_StaffPersonalInfoId",
                table: "tbl_HeartRate_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRate_StaffName_HeartRateId",
                table: "tbl_HeartRate_StaffName",
                column: "HeartRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HeartRate_StaffName_StaffPersonalInfoId",
                table: "tbl_HeartRate_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Incoming_Meds_UserName",
                table: "tbl_Incoming_Meds",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_KeyWorker_OfficerToAct_KeyWorkerId",
                table: "tbl_KeyWorker_OfficerToAct",
                column: "KeyWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_KeyWorker_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_KeyWorker_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_KeyWorker_StaffName_KeyWorkerId",
                table: "tbl_KeyWorker_StaffName",
                column: "KeyWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_KeyWorker_StaffName_StaffPersonalInfoId",
                table: "tbl_KeyWorker_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_LogAudit_OfficerToAct_LogAuditId",
                table: "tbl_LogAudit_OfficerToAct",
                column: "LogAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_LogAudit_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_LogAudit_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedAudit_AuditorName_MedAuditId",
                table: "tbl_MedAudit_AuditorName",
                column: "MedAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedAudit_AuditorName_StaffPersonalInfoId",
                table: "tbl_MedAudit_AuditorName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedAudit_OfficerToAct_MedAuditId",
                table: "tbl_MedAudit_OfficerToAct",
                column: "MedAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedAudit_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_MedAudit_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedComp_OfficerToAct_MedCompId",
                table: "tbl_MedComp_OfficerToAct",
                column: "MedCompId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedComp_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_MedComp_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OneToOne_OfficerToAct_OneToOneId",
                table: "tbl_OneToOne_OfficerToAct",
                column: "OneToOneId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OneToOne_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_OneToOne_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvl_OfficerToAct_OxygenLvlId",
                table: "tbl_OxygenLvl_OfficerToAct",
                column: "OxygenLvlId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvl_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_OxygenLvl_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvl_Physician_OxygenLvlId",
                table: "tbl_OxygenLvl_Physician",
                column: "OxygenLvlId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvl_Physician_StaffPersonalInfoId",
                table: "tbl_OxygenLvl_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvl_StaffName_OxygenLvlId",
                table: "tbl_OxygenLvl_StaffName",
                column: "OxygenLvlId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OxygenLvl_StaffName_StaffPersonalInfoId",
                table: "tbl_OxygenLvl_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChart_OfficerToAct_PainChartId",
                table: "tbl_PainChart_OfficerToAct",
                column: "PainChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChart_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_PainChart_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChart_Physician_PainChartId",
                table: "tbl_PainChart_Physician",
                column: "PainChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChart_Physician_StaffPersonalInfoId",
                table: "tbl_PainChart_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChart_StaffName_PainChartId",
                table: "tbl_PainChart_StaffName",
                column: "PainChartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PainChart_StaffName_StaffPersonalInfoId",
                table: "tbl_PainChart_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Program_OfficerToAct_ProgramId",
                table: "tbl_Program_OfficerToAct",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Program_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_Program_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRate_OfficerToAct_PulseRateId",
                table: "tbl_PulseRate_OfficerToAct",
                column: "PulseRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRate_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_PulseRate_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRate_Physician_PulseRateId",
                table: "tbl_PulseRate_Physician",
                column: "PulseRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRate_Physician_StaffPersonalInfoId",
                table: "tbl_PulseRate_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRate_StaffName_PulseRateId",
                table: "tbl_PulseRate_StaffName",
                column: "PulseRateId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PulseRate_StaffName_StaffPersonalInfoId",
                table: "tbl_PulseRate_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Resources__PublishTo",
                table: "tbl_Resources_",
                column: "PublishTo");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Seizure_OfficerToAct_SeizureId",
                table: "tbl_Seizure_OfficerToAct",
                column: "SeizureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Seizure_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_Seizure_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Seizure_Physician_SeizureId",
                table: "tbl_Seizure_Physician",
                column: "SeizureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Seizure_Physician_StaffPersonalInfoId",
                table: "tbl_Seizure_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Seizure_StaffName_SeizureId",
                table: "tbl_Seizure_StaffName",
                column: "SeizureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Seizure_StaffName_StaffPersonalInfoId",
                table: "tbl_Seizure_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Service_OfficerToAct_ServiceId",
                table: "tbl_Service_OfficerToAct",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Service_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_Service_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Service_StaffName_ServiceId",
                table: "tbl_Service_StaffName",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Service_StaffName_StaffPersonalInfoId",
                table: "tbl_Service_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SpotCheck_OfficerToAct_SpotCheckId",
                table: "tbl_SpotCheck_OfficerToAct",
                column: "SpotCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SpotCheck_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_SpotCheck_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_AdlObs_ClientId",
                table: "tbl_Staff_AdlObs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_AdlObs_StaffPersonalInfoId",
                table: "tbl_Staff_AdlObs",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_KeyWorkerVoice_ServicesRequiresServices",
                table: "tbl_Staff_KeyWorkerVoice",
                column: "ServicesRequiresServices");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_KeyWorkerVoice_StaffPersonalInfoId",
                table: "tbl_Staff_KeyWorkerVoice",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_MedCompObs_ClientId",
                table: "tbl_Staff_MedCompObs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_MedCompObs_StaffPersonalInfoId",
                table: "tbl_Staff_MedCompObs",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_OneToOne_StaffPersonalInfoId",
                table: "tbl_Staff_OneToOne",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_Reference_ApplicantRole",
                table: "tbl_Staff_Reference",
                column: "ApplicantRole");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_Reference_ConfirmedBy",
                table: "tbl_Staff_Reference",
                column: "ConfirmedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_SpotCheck_ClientId",
                table: "tbl_Staff_SpotCheck",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_SpotCheck_StaffPersonalInfoId",
                table: "tbl_Staff_SpotCheck",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_SupervisionAppraisal_StaffPersonalInfoId",
                table: "tbl_Staff_SupervisionAppraisal",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_Survey_StaffPersonalInfoId",
                table: "tbl_Staff_Survey",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Supervision_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_Supervision_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Supervision_OfficerToAct_StaffSupervisionAppraisalId",
                table: "tbl_Supervision_OfficerToAct",
                column: "StaffSupervisionAppraisalId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Supervision_StaffName_StaffPersonalInfoId",
                table: "tbl_Supervision_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Supervision_StaffName_StaffSupervisionAppraisalId",
                table: "tbl_Supervision_StaffName",
                column: "StaffSupervisionAppraisalId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Survey_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_Survey_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Survey_OfficerToAct_StaffSurveyId",
                table: "tbl_Survey_OfficerToAct",
                column: "StaffSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Survey_StaffName_StaffPersonalInfoId",
                table: "tbl_Survey_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Survey_StaffName_StaffSurveyId",
                table: "tbl_Survey_StaffName",
                column: "StaffSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Visit_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_Visit_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Visit_OfficerToAct_VisitId",
                table: "tbl_Visit_OfficerToAct",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Visit_StaffName_StaffPersonalInfoId",
                table: "tbl_Visit_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Visit_StaffName_VisitId",
                table: "tbl_Visit_StaffName",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_CallerName_StaffPersonalInfoId",
                table: "tbl_Voice_CallerName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_CallerName_VoiceId",
                table: "tbl_Voice_CallerName",
                column: "VoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_GoodStaff_StaffPersonalInfoId",
                table: "tbl_Voice_GoodStaff",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_GoodStaff_VoiceId",
                table: "tbl_Voice_GoodStaff",
                column: "VoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_Voice_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_OfficerToAct_VoiceId",
                table: "tbl_Voice_OfficerToAct",
                column: "VoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_PoorStaff_StaffPersonalInfoId",
                table: "tbl_Voice_PoorStaff",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Voice_PoorStaff_VoiceId",
                table: "tbl_Voice_PoorStaff",
                column: "VoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Whisttle_Blower_UserName",
                table: "tbl_Whisttle_Blower",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCare_OfficerToAct_StaffPersonalInfoId",
                table: "tbl_WoundCare_OfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCare_OfficerToAct_WoundCareId",
                table: "tbl_WoundCare_OfficerToAct",
                column: "WoundCareId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCare_Physician_StaffPersonalInfoId",
                table: "tbl_WoundCare_Physician",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCare_Physician_WoundCareId",
                table: "tbl_WoundCare_Physician",
                column: "WoundCareId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCare_StaffName_StaffPersonalInfoId",
                table: "tbl_WoundCare_StaffName",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_WoundCare_StaffName_WoundCareId",
                table: "tbl_WoundCare_StaffName",
                column: "WoundCareId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_AdlObs_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_BloodCoag_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_BloodCoag_Physician");

            migrationBuilder.DropTable(
                name: "tbl_BloodCoag_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_BloodPressure_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_BloodPressure_Physician");

            migrationBuilder.DropTable(
                name: "tbl_BloodPressure_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_BMIChart_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_BMIChart_Physician");

            migrationBuilder.DropTable(
                name: "tbl_BMIChart_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_BodyTemp_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_BodyTemp_Physician");

            migrationBuilder.DropTable(
                name: "tbl_BodyTemp_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_BowelMovement_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_BowelMovement_Physician");

            migrationBuilder.DropTable(
                name: "tbl_BowelMovement_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_Client_Cleaning");

            migrationBuilder.DropTable(
                name: "tbl_Client_MealDay");

            migrationBuilder.DropTable(
                name: "tbl_Client_Shopping");

            migrationBuilder.DropTable(
                name: "tbl_Complain_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_Complain_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_Enotice_");

            migrationBuilder.DropTable(
                name: "tbl_EyeHealth_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_EyeHealth_Physician");

            migrationBuilder.DropTable(
                name: "tbl_EyeHealth_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_FoodIntake_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_FoodIntake_Physician");

            migrationBuilder.DropTable(
                name: "tbl_FoodIntake_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_HeartRate_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_HeartRate_Physician");

            migrationBuilder.DropTable(
                name: "tbl_HeartRate_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_Incoming_Meds");

            migrationBuilder.DropTable(
                name: "tbl_KeyWorker_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_KeyWorker_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_LogAudit_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_MedAudit_AuditorName");

            migrationBuilder.DropTable(
                name: "tbl_MedAudit_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_MedComp_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_OneToOne_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_OxygenLvl_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_OxygenLvl_Physician");

            migrationBuilder.DropTable(
                name: "tbl_OxygenLvl_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_PainChart_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_PainChart_Physician");

            migrationBuilder.DropTable(
                name: "tbl_PainChart_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_Program_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_PulseRate_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_PulseRate_Physician");

            migrationBuilder.DropTable(
                name: "tbl_PulseRate_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_Resources_");

            migrationBuilder.DropTable(
                name: "tbl_Seizure_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_Seizure_Physician");

            migrationBuilder.DropTable(
                name: "tbl_Seizure_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_Service_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_Service_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_SpotCheck_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_Staff_Reference");

            migrationBuilder.DropTable(
                name: "tbl_Supervision_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_Supervision_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_Survey_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_Survey_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_Visit_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_Visit_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_Voice_CallerName");

            migrationBuilder.DropTable(
                name: "tbl_Voice_GoodStaff");

            migrationBuilder.DropTable(
                name: "tbl_Voice_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_Voice_PoorStaff");

            migrationBuilder.DropTable(
                name: "tbl_Whisttle_Blower");

            migrationBuilder.DropTable(
                name: "tbl_WoundCare_OfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_WoundCare_Physician");

            migrationBuilder.DropTable(
                name: "tbl_WoundCare_StaffName");

            migrationBuilder.DropTable(
                name: "tbl_Staff_AdlObs");

            migrationBuilder.DropTable(
                name: "tbl_Client_BloodCoagulationRecord");

            migrationBuilder.DropTable(
                name: "tbl_Client_BloodPressure");

            migrationBuilder.DropTable(
                name: "tbl_Client_BMIChart");

            migrationBuilder.DropTable(
                name: "tbl_Client_BodyTemp");

            migrationBuilder.DropTable(
                name: "tbl_Client_BowelMovement");

            migrationBuilder.DropTable(
                name: "tbl_Client_MealType");

            migrationBuilder.DropTable(
                name: "tbl_Client_Nutrition");

            migrationBuilder.DropTable(
                name: "tbl_Client_ComplainRegister");

            migrationBuilder.DropTable(
                name: "tbl_Client_EyeHealthMonitoring");

            migrationBuilder.DropTable(
                name: "tbl_Client_FoodIntake");

            migrationBuilder.DropTable(
                name: "tbl_Client_HeartRate");

            migrationBuilder.DropTable(
                name: "tbl_Staff_KeyWorkerVoice");

            migrationBuilder.DropTable(
                name: "tbl_Client_LogAudit");

            migrationBuilder.DropTable(
                name: "tbl_Client_MedAudit");

            migrationBuilder.DropTable(
                name: "tbl_Staff_MedCompObs");

            migrationBuilder.DropTable(
                name: "tbl_Staff_OneToOne");

            migrationBuilder.DropTable(
                name: "tbl_Client_Oxygenlvl");

            migrationBuilder.DropTable(
                name: "tbl_Client_PainChart");

            migrationBuilder.DropTable(
                name: "tbl_Client_Program");

            migrationBuilder.DropTable(
                name: "tbl_Client_PulseRate");

            migrationBuilder.DropTable(
                name: "tbl_Client_Seizure");

            migrationBuilder.DropTable(
                name: "tbl_Client_ServiceWatch");

            migrationBuilder.DropTable(
                name: "tbl_Staff_SpotCheck");

            migrationBuilder.DropTable(
                name: "tbl_Staff_SupervisionAppraisal");

            migrationBuilder.DropTable(
                name: "tbl_Staff_Survey");

            migrationBuilder.DropTable(
                name: "tbl_Client_MgtVisit");

            migrationBuilder.DropTable(
                name: "tbl_Client_Voice");

            migrationBuilder.DropTable(
                name: "tbl_Client_WoundCare");
        }
    }
}
