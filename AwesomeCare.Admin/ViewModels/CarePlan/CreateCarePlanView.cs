using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.ManagingTasks;
using AwesomeCare.DataTransferObject.DTOs.ClientCleaning;
using AwesomeCare.DataTransferObject.DTOs.ClientDailyTask;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.ClientMealDays;
using AwesomeCare.DataTransferObject.DTOs.ClientNutrition;
using AwesomeCare.DataTransferObject.DTOs.ClientProgram;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch;
using AwesomeCare.DataTransferObject.DTOs.ClientShopping;
using AwesomeCare.DataTransferObject.DTOs.Health.Balance;
using AwesomeCare.DataTransferObject.DTOs.Health.PhysicalAbility;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.Interest;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.PersonalityTest;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.CarePlan
{
    public class CreateCarePlanView
    {
        public CreateCarePlanView()
        {
            #region Care Plan
            Individuality = new List<SelectListItem>();
            RightsAndRespect = new List<SelectListItem>();
            Choice = new List<SelectListItem>();
            DignityAndPrivacy = new List<SelectListItem>();
            Partnership = new List<SelectListItem>();
            IndicatorList = new List<string>();
            FocusList = new Dictionary<string, List<SelectListItem>>();
            StaffList = new List<SelectListItem>();
            GetEquipment = new List<GetEquipment>();
            LandLogList = new List<string>();
            KeyLogList = new List<string>();
            InvolingList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            HeadingList = new List<SelectListItem>();
            GetHomeRiskAssessments = new List<GetHomeRiskAssessment>();
            baseRecordList = new List<GetBaseRecordItem>();
            GetInvolvingParty = new List<GetClientInvolvingParty>();
            GetServiceWatch = new List<GetClientServiceWatch>();
            GetProgram = new List<GetClientProgram>();
            GetBalance = new List<GetBalance>();
            WeekDays = new List<GetRotaDayofWeek>();
            RotaTypes = new List<GetClientRotaType>();
            Rotas = new List<SelectListItem>();
            RotaTasks = new List<SelectListItem>();
            ClientRotas = new List<GetClientRota>();
            GetManagingTasks = new List<GetManagingTasks>();
            GetInterest = new List<GetInterest>();
            GetPersonalityTest = new List<GetPersonalityTest>();
            ClientMealDays = new List<GetClientMealDays>();
            ClientShopping = new List<GetClientShopping>();
            ClientCleaning = new List<GetClientCleaning>();
            #endregion


        }

        [DataType(DataType.Upload)]
        
        public IFormFile HL_Attach { get; set; }

        [DataType(DataType.Upload)]
        
        public IFormFile Equipment_Attach { get; set; }

        public byte[] QRCode { get; set; }
        public string PassportFilePath { get; set; }

        public string IdNumber { get; set; }

        public List<GetClientInvolvingParty> GetInvolvingParty { get; set; }
        #region PersonalDetail
        public List<GetEquipment> GetEquipment { get; set; }
        public List<GetPersonCentred> GetPersonCentred { get; set; }
        public List<GetPersonCentredFocus> GetPersonCentredFocus { get; set; }
        public List<int> Focus1 { get; set; }
        public List<int> Focus2 { get; set; }
        public List<int> Focus3 { get; set; }
        public List<int> Focus4 { get; set; }
        public List<int> Focus5 { get; set; }
        public List<SelectListItem> Individuality { get; set; }
        public List<SelectListItem> RightsAndRespect { get; set; }
        public List<SelectListItem> Choice { get; set; }
        public List<SelectListItem> DignityAndPrivacy { get; set; }
        public List<SelectListItem> Partnership { get; set; }
        public List<SelectListItem> StaffList { get; set; }
        public Dictionary<string, List<SelectListItem>> FocusList { get; set; }
        public List<string> IndicatorList { get; set; }
        public List<string> LandLogList { get; set; }
        public List<string> KeyLogList { get; set; }
        public List<SelectListItem> InvolingList { get; set; }
        public List<SelectListItem> ClassList { get; set; }

        public string ClientName { get; set; }
        public int EquipmentCount { get; set; } = 0;
        public int PersonCentreCount { get; set; } = 5;

        #region Personal Detail
        public int PersonalDetailId { get; set; }
        public int ClientId { get; set; }
        #endregion

        #region Capacity
        [Required]
        public int CapacityId { get; set; }
        public List<int> Indicator { get; set; }
        [Required]
        public int Pointer { get; set; }
        [Required]
        public string Implications { get; set; }
        #endregion

        #region ConsentCare
        [Required]
        public int CareId { get; set; }
        [Required]
        public int CareSignature { get; set; }
        [Required]
        public DateTime CareDate { get; set; }
        public int CareName { get; set; }
        public string CareRelation { get; set; }
        #endregion

        #region ConsentData
        [Required]
        public int DataId { get; set; }
        [Required]
        public int DataSignature { get; set; }
        [Required]
        public DateTime DataDate { get; set; }
        public int DataName { get; set; }
        public string DataRelation { get; set; }
        #endregion

        #region ConsentLandLine
        [Required]
        public int LandLineId { get; set; }
        [Required]
        public int LandLineSignature { get; set; }
        [Required]
        public List<int> LandLineLogMethod { get; set; }
        [Required]
        public DateTime LandLineDate { get; set; }

        public int LandName { get; set; }
        public string LandRelation { get; set; }
        #endregion

        #region KeyIndicators
        public int KeyId { get; set; }
        [Required]
        public string AboutMe { get; set; }
        [Required]
        public string FamilyRole { get; set; }
        [Required]
        public int LivingStatus { get; set; }
        [Required]
        public int Debture { get; set; }
        [Required]
        public string ThingsILike { get; set; }
        [Required]
        public List<int> LogMethod { get; set; }
        #endregion

        #region Personal
        [Required]
        public int PersonalId { get; set; }
        [Required]
        public int Smoking { get; set; }
        [Required]
        public int DNR { get; set; }

        public string FullName { get; set; }
        public string PreferredName { get; set; }
        public int PreferredLanguage { get; set; }
        public int Gender { get; set; }
        public string Religion { get; set; }
        public int PreferredGender { get; set; }
        public string Telephone { get; set; }
        public string AccessCode { get; set; }
        public string PostCode { get; set; }
        public string Nationality { get; set; }
        public string DateofBirth { get; set; }
        public string Address { get; set; }

        public string KeyWorker { get; set; }
        public string TeamLeader { get; set; }
        #endregion

        #region Review
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public DateTime CP_PreDate { get; set; }
        [Required]
        public DateTime CP_ReviewDate { get; set; }
        [Required]
        public DateTime RA_PreDate { get; set; }
        [Required]
        public DateTime RA_ReviewDate { get; set; }
        #endregion

        #endregion

        #region CarePlan Nutrition
        public int NutritionId { get; set; }
        public int Nutrition_FoodStorage { get; set; }
        public string Nutrition_ServingMeal { get; set; }
        public string Nutrition_WhenRestock { get; set; }
        public int Nutrition_WhoRestock { get; set; }
        public string Nutrition_SpecialDiet { get; set; }
        public string Nutrition_DrinkType { get; set; }
        public string Nutrition_AvoidFood { get; set; }
        public int Nutrition_ThingsILike { get; set; }
        public List<string> ListThingsILike { get; set; } = new List<string>();
        public int Nutrition_FoodIntake { get; set; }
        public int Nutrition_MealPreparation { get; set; }
        public int Nutrition_EatingDifficulty { get; set; }
        public string Nutrition_RiskMitigations { get; set; }
        #endregion

        #region Hygiene

        #region Infection Control
        public int InfectionId { get; set; }
        public int Infection_Type { get; set; }
        public string Infection_Guideline { get; set; }
        public DateTime Infection_TestDate { get; set; }
        public int Infection_VaccStatus { get; set; }
        public string Infection_Remarks { get; set; }
        public int Infection_Status { get; set; }
        #endregion

        #region Managing Tasks
        public List<GetManagingTasks> GetManagingTasks { get; set; }
        public int TaskCount { get; set; } = 0;
        #endregion

        #region Personal Hygiene
        public int HygieneId { get; set; }
        public int Hygiene_Cleaning { get; set; }
        public int Hygiene_CleaningTools { get; set; }
        public int Hygiene_WhoClean { get; set; }
        public int Hygiene_DesiredCleaning { get; set; }
        public int Hygiene_CleaningFreq { get; set; }
        public int Hygiene_GeneralAppliance { get; set; }
        public int Hygiene_DirtyLaundry { get; set; }
        public int Hygiene_DryLaundry { get; set; }
        public int Hygiene_WashingMachine { get; set; }
        public int Hygiene_Ironing { get; set; }
        public string Hygiene_LaundryGuide { get; set; }
        public string Hygiene_LaundrySupport { get; set; }
        #endregion

        #endregion

        #region Interest And Objective

        #region Interest
        public List<GetInterest> GetInterest { get; set; }
        public List<GetPersonalityTest> GetPersonalityTest { get; set; }

        public int GoalId { get; set; }
        public string Interest_CareGoal { get; set; }
        public string Interest_Brief { get; set; }

        public int InterestCount { get; set; }
        public int PersonalityCount { get; set; }
        #endregion

        #region Pets
        public int PetsId { get; set; }
        public string Pet_Name { get; set; }
        public int Pet_Type { get; set; }
        public string Pet_Age { get; set; }
        public int Pet_Gender { get; set; }
        public string PetActivities { get; set; }
        public int Pet_MealStorage { get; set; }
        public int Pet_VetVisit { get; set; }
        public int PetInsurance { get; set; }
        public string PetCare { get; set; }
        public string Pet_MealPattern { get; set; }
        #endregion

        #endregion

        #region Health

        #region Balance
        public List<GetBalance> GetBalance { get; set; }
        #endregion

        #region HealthAndLiving
        public int HLId { get; set; }
        public string HL_BriefHealth { get; set; }
        public string HL_ObserveHealth { get; set; }
        public string HL_WakeUp { get; set; }
        public string HL_CareSupport { get; set; }
        public string HL_MovingAndHandling { get; set; }
        public string HL_SupportToBed { get; set; }
        public int HL_DehydrationRisk { get; set; }
        public int HL_LifeStyle { get; set; }
        public int HL_PressureSore { get; set; }
        public int HL_ContinenceIssue { get; set; }
        public string HL_ContinenceNeeds { get; set; }
        public string HL_ContinenceSource { get; set; }
        public int HL_ConstraintRequired { get; set; }
        public string HL_ConstraintDetails { get; set; }
        public string HL_ConstraintAttachment { get; set; }
        public int HL_MeansOfComm { get; set; }
        public int HL_Smoking { get; set; }
        public int HL_AbilityToRead { get; set; }
        public string HL_AbilityToReadName { get; set; }
        public int HL_TextFontSize { get; set; }
        public int HL_Email { get; set; }
        public int HL_FinanceManagement { get; set; }
        public int HL_PostalService { get; set; }
        public int HL_LetterOpening { get; set; }
        public int HL_ShoppingRequired { get; set; }
        public int HL_SpecialCleaning { get; set; }
        public int HL_LaundaryRequired { get; set; }
        public int HL_VideoCallRequired { get; set; }
        public int HL_EatingWithStaff { get; set; }
        public int HL_AllowChats { get; set; }
        public int HL_TeaChocolateCoffee { get; set; }
        public int HL_NeighbourInvolment { get; set; }
        public int HL_FamilyUpdate { get; set; }
        public int HL_AlcoholicDrink { get; set; }
        public int HL_TVandMusic { get; set; }
        public string HL_SpecialCaution { get; set; }
        #endregion

        #region HistoryOfFall
        public int HistoryId { get; set; }
        public string History_Details { get; set; }
        public DateTime History_Date { get; set; }
        public string History_Cause { get; set; }
        public string History_Prevention { get; set; }
        #endregion

        #region PhysicalAbility
        public int PhysicalId { get; set; }
        public string Physical_Name { get; set; }
        public string Physical_Description { get; set; }
        public int Physical_Mobility { get; set; }
        public int Physical_Status { get; set; }
        #endregion

        #region SpecialHealthAndMed
        public int SHMId { get; set; }
        public int SHAM_AdminLvl { get; set; }
        public int SHAM_MedicationAllergy { get; set; }
        public int SHAM_FamilyMeds { get; set; }
        public int SHAM_LeftoutMedicine { get; set; }
        public int SHAM_NameFormMedicaiton { get; set; }
        public int SHAM_WhoAdminister { get; set; }
        public int SHAM_MedsGPOrder { get; set; }
        public int SHAM_PharmaMARChart { get; set; }
        public int SHAM_TempMARChart { get; set; }
        public string SHAM_MedKeyCode { get; set; }
        public int SHAM_FamilyReturnMed { get; set; }
        public int SHAM_AccessMedication { get; set; }
        public int SHAM_NoMedAccess { get; set; }
        public int SHAM_MedAccessDenial { get; set; }
        public int SHAM_PNRMedReq { get; set; }
        public int SHAM_PNRDoses { get; set; }
        public int SHAM_PNRMedsAdmin { get; set; }
        public int SHAM_OverdoseContact { get; set; }
        public string SHAM_PNRMedsMissing { get; set; }
        public string SHAM_MedicationStorage { get; set; }
        public int SHAM_SpecialStorage { get; set; }
        public string SHAM_PNRMedList { get; set; }
        public int SHAM_Consent { get; set; }
        public int SHAM_Type { get; set; }
        public string SHAM_By { get; set; }
        public DateTime SHAM_Date { get; set; }
        #endregion

        #region SpecialHealthCond
        public int HealthCondId { get; set; }
        public string SHC_ConditionName { get; set; }
        public string SHC_SourceInformation { get; set; }
        public string SHC_FeelingBeforeIncident { get; set; }
        public string SHC_FeelingAfterIncident { get; set; }
        public string SHC_Frequency { get; set; }
        public string SHC_LivingActivities { get; set; }
        public string SHC_Trigger { get; set; }
        public string SHC_ClientAction { get; set; }
        public string SHC_ClinicRecommendation { get; set; }
        public string SHC_LifestyleSupport { get; set; }
        public string SHC_PlanningHealthCondition { get; set; }
        #endregion


        #endregion

        #region Home Risk Assessment
        public List<SelectListItem> HeadingList { get; set; }
        public List<GetHomeRiskAssessment> GetHomeRiskAssessments { get; set; }
        public int TaskCountHRA { get; set; }

        public List<GetBaseRecordItem> baseRecordList { get; set; }
        public List<GetBaseRecordItem> careIssuebaseRecordList { get; set; } = new List<GetBaseRecordItem>();
        public List<GetBaseRecordWithItems> baseRecordBestInterestList { get; set; } = new List<GetBaseRecordWithItems> { };
        #endregion

        #region Best Interest

        public GetBestInterestAssessment getBestInterestAssessment { get; set; }
        public int BestId { get; set; }
        public List<SelectListItem> ClientList { get; set; }
        public List<GetBaseRecordItem> BestHeadingList { get; set; }
        public List<GetBaseRecordItem> Heading2List { get; set; }
        public List<GetBaseRecordItem> TitleList { get; set; }
        public List<GetBaseRecordItem> Title2List { get; set; }
        public List<GetBaseRecordItem> AnswerList { get; set; }
        public List<SelectListItem> AnswerSelectList { get; set; }
        public List<GetBaseRecordWithItems> BaseRecordList { get; set; }
        public DateTime Date { get; set; }
        public List<GetCareIssuesTask> GetCareIssuesTask { get; set; }
        public List<GetHealthTask> GetHealthTask { get; set; }
        public List<GetBelieveTask> GetBelieveTask { get; set; }
        public List<GetHealthTask2> GetHealthTask2 { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Signature { get; set; }

        public int BelieveTaskCount { get; set; }
        public int CareIssuesTaskCount { get; set; }
        public int HealthTaskCount { get; set; }
        public int HealthTask2Count { get; set; }
        #endregion
        
        public List<GetClientDailyTask> GetClientDailyTask { get; set; }
        public List<GetClientServiceWatch> GetServiceWatch { get; set; }
        public List<GetClientProgram> GetProgram { get; set; }

        public List<GetRotaDayofWeek> WeekDays { get; set; }
        public List<GetClientRotaType> RotaTypes { get; set; }
        public List<SelectListItem> RotaTasks { get; set; }

        public List<SelectListItem> Rotas { get; set; }

        public List<GetClientRota> ClientRotas { get; set; }


        public List<GetClientMealDays> ClientMealDays { get; set; }
        public List<GetClientShopping> ClientShopping { get; set; }
        public List<GetClientCleaning> ClientCleaning { get; set; }

        public List<GetPhysicalAbility> PhysicalAbility { get; set; } = new List<GetPhysicalAbility> { };
    }
}
