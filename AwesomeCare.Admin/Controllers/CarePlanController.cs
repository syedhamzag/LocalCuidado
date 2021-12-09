using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Balance;
using AwesomeCare.Admin.Services.CarePlanNutrition;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientInvolvingParty;
using AwesomeCare.Admin.Services.HealthAndLiving;
using AwesomeCare.Admin.Services.HistoryOfFall;
using AwesomeCare.Admin.Services.InfectionControl;
using AwesomeCare.Admin.Services.InterestAndObjective;
using AwesomeCare.Admin.Services.ManagingTasks;
using AwesomeCare.Admin.Services.PersonalDetail;
using AwesomeCare.Admin.Services.PersonalHygiene;
using AwesomeCare.Admin.Services.Pets;
using AwesomeCare.Admin.Services.PhysicalAbility;
using AwesomeCare.Admin.Services.SpecialHealthAndMedication;
using AwesomeCare.Admin.Services.SpecialHealthCondition;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.CarePlan;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OfficeOpenXml;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class CarePlanController : BaseController
    {
        private IPersonalDetailService _pdetailService;
        private ICarePlanNutritionService _nutritionService;
        private IPersonalHygieneService _phygieneService;
        private IInfectionControlService _infectionService;
        private IManagingTasksService _mataskService;
        private IPetsService _petsService;
        private IInterestAndObjectiveService _interestService;
        private IBalanceService _balanceService;
        private IPhysicalAbilityService _physicalService;
        private IHistoryOfFallService _historyService;
        private IHealthAndLivingService _hlService;
        private ISpecialHealthAndMedicationService _shamService;
        private ISpecialHealthConditionService _shcService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;
        private IClientInvolvingParty _involvingparty;
        private IHomeRiskAssessmentService _clientHomeRiskAssessment;
        private readonly QRCodeGenerator _qRCodeGenerator;

        public CarePlanController(  IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IPersonalDetailService pdetailService,
                                    IBaseRecordService baseRecord, IClientInvolvingParty involvingparty, ICarePlanNutritionService nutritionService,
                                    IPersonalHygieneService phygieneService, IInfectionControlService infectionService, IManagingTasksService mataskService,
                                    IPetsService petsService, IInterestAndObjectiveService interestService, IBalanceService balanceService, IPhysicalAbilityService physicalService,
                                    IHistoryOfFallService historyService, IHealthAndLivingService hlService, ISpecialHealthAndMedicationService shamService,
                                    ISpecialHealthConditionService shcService, IHomeRiskAssessmentService clientHomeRiskAssessment, QRCodeGenerator qRCodeGenerator) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _pdetailService = pdetailService;
            _baseRecord = baseRecord;
            _involvingparty = involvingparty;
            _nutritionService = nutritionService;
            _phygieneService = phygieneService;
            _infectionService = infectionService;
            _mataskService = mataskService;
            _petsService = petsService;
            _interestService = interestService;
            _balanceService = balanceService;
            _physicalService = physicalService;
            _historyService = historyService;
            _hlService = hlService;
            _shamService = shamService;
            _shcService = shcService;
            _clientHomeRiskAssessment = clientHomeRiskAssessment;
            _qRCodeGenerator = qRCodeGenerator;
        }

        public async Task<IActionResult> CareView(int clientId)
        {
            //var bases = await _baseRecord.GetBaseRecord();
            //var baseClass = bases.Where(s => s.KeyName == "Class").FirstOrDefault().BaseRecordId;
            //var classItems = await _baseRecord.GetBaseRecordWithItems(baseClass);

            //var staff = await _staffService.GetStaffs();
            //var involve = await _clientService.GetClient(clientId);
            //var client = await _clientService.GetClient(clientId);
            //var details = await _clientService.GetClientDetail();
            //var party = _involvingparty.Get(clientId);
            //var Relation = "N/A";
            //if (party.Result != null)
            //{
            //    Relation = party.Result.Relationship;
            //}

            //var pdetail = await _pdetailService.Get(clientId);
            //var nutrition = await _nutritionService.Get();
            //var infection = await _infectionService.Get();
            //var mtask = await _mataskService.Get();
            //var hygiene = await _phygieneService.Get();
            //var obj = await _interestService.Get(clientId);
            //var pets = await _petsService.Get();
            //var balance = await _balanceService.Get();
            //var physical = await _physicalService.Get();
            //var hl = await _hlService.Get();
            //var history = await _historyService.Get();
            //var sham = await _shamService.Get();
            //var shc = await _shcService.Get();
            //List<GetHomeRiskAssessment> home = await _clientHomeRiskAssessment.GetByClient(clientId);
            //var model = new CreateCarePlanView();
            //model.ClientId = clientId;
            //var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            //var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Home_Risk_Assessment_Heading").Select(s => s.BaseRecordItems).FirstOrDefault();
            //model.baseRecordList = filterBaseRecord.ToList();

            //if (home.Count > 0)
            //{
            //    model.HomeRiskAssessmentId = home.FirstOrDefault().HomeRiskAssessmentId;
            //    model.HeadingList = home.Select(s => new SelectListItem(s.Heading, s.HomeRiskAssessmentId.ToString())).ToList();
            //}
            //#region PERSONAL DETAILS
            //if (pdetail != null) 
            //{
            //    model.ClientId = clientId;
            //    model.PersonalDetailId = pdetail.PersonalDetailId;
            //    model.CapacityId = pdetail.Capacity.FirstOrDefault().CapacityId;
            //    model.Implications = pdetail.Capacity.FirstOrDefault().Implications;
            //    model.Pointer = pdetail.Capacity.FirstOrDefault().Pointer;
            //    model.IndicatorList = pdetail.Capacity.FirstOrDefault().Indicator.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList();
            //    model.Indicator = pdetail.Capacity.FirstOrDefault().Indicator.Select(s => s.BaseRecordId).ToList();
            //    model.CareId = pdetail.ConsentCare.FirstOrDefault().CareId;
            //    model.CareSignature = pdetail.ConsentCare.FirstOrDefault().Signature;
            //    model.CareDate = pdetail.ConsentCare.FirstOrDefault().Date;
            //    model.CareName = pdetail.ConsentCare.FirstOrDefault().Name;
            //    model.CareRelation = Relation;
            //    model.DataId = pdetail.ConsentData.FirstOrDefault().DataId;
            //    model.DataSignature = pdetail.ConsentData.FirstOrDefault().Signature;
            //    model.DataDate = pdetail.ConsentData.FirstOrDefault().Date;
            //    model.DataName = pdetail.ConsentData.FirstOrDefault().Name;
            //    model.DataRelation = Relation;
            //    model.LandLineId = pdetail.ConsentLandline.FirstOrDefault().LandlineId;
            //    model.LandLineSignature = pdetail.ConsentLandline.FirstOrDefault().Signature;
            //    model.LandLineDate = pdetail.ConsentLandline.FirstOrDefault().Date;
            //    model.LandLogList = pdetail.ConsentLandline.FirstOrDefault().LogMethod.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList();
            //    model.LandLineLogMethod = pdetail.ConsentLandline.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList();
            //    model.LandName = pdetail.ConsentLandline.FirstOrDefault().Name;
            //    model.LandRelation = Relation;
            //    model.GetEquipment = pdetail.Equipment;
            //    model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            //    model.AboutMe = pdetail.KeyIndicators.FirstOrDefault().AboutMe;
            //    model.KeyId = pdetail.KeyIndicators.FirstOrDefault().KeyId;
            //    model.FamilyRole = pdetail.KeyIndicators.FirstOrDefault().FamilyRole;
            //    model.Debture = pdetail.KeyIndicators.FirstOrDefault().Debture;
            //    model.LivingStatus = pdetail.KeyIndicators.FirstOrDefault().LivingStatus;
            //    model.KeyLogList = pdetail.KeyIndicators.FirstOrDefault().LogMethod.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList();
            //    model.LogMethod = pdetail.KeyIndicators.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList();
            //    model.ThingsILike = pdetail.KeyIndicators.FirstOrDefault().ThingsILike;
            //    model.PersonalId = pdetail.Personal.FirstOrDefault().PersonalId;
            //    model.Smoking = pdetail.Personal.FirstOrDefault().Smoking;
            //    model.DNR = pdetail.Personal.FirstOrDefault().DNR;
            //    model.FullName = details.Where(s => s.ClientId == clientId).Select(s => s.FullName).FirstOrDefault();
            //    model.PreferredLanguage = client.LanguageId;
            //    model.Gender = client.GenderId;
            //    model.DateofBirth = client.DateOfBirth;
            //    model.Address = client.Address;
            //    model.PostCode = client.PostCode;
            //    model.Telephone = client.Telephone;
            //    model.PreferredName = client.Firstname;
            //    model.AccessCode = client.KeySafe;
            //    model.PreferredGender = client.ChoiceOfStaffId;
            //    model.KeyWorker = client.Keyworker;
            //    model.TeamLeader = client.TeamLeader;
            //    model.Religion = pdetail.Personal.FirstOrDefault().Religion;
            //    model.Nationality = pdetail.Personal.FirstOrDefault().Nationality;
            //    model.GetPersonCentred = pdetail.PersonCentred;
            //    model.ReviewId = pdetail.Review.FirstOrDefault().ReviewId;
            //    model.CP_PreDate = pdetail.Review.FirstOrDefault().CP_PreDate;
            //    model.CP_ReviewDate = pdetail.Review.FirstOrDefault().CP_ReviewDate;
            //    model.RA_PreDate = pdetail.Review.FirstOrDefault().RA_PreDate;
            //    model.RA_ReviewDate = pdetail.Review.FirstOrDefault().RA_ReviewDate;
            //    model.InvolingList = involve.InvolvingParties.Select(s => new SelectListItem(s.Name, s.ClientInvolvingPartyId.ToString())).ToList();
            //    model.EquipmentCount = pdetail.Equipment.Count;
            //    model.PersonCentreCount = pdetail.PersonCentred.Count;
            //}
            //#endregion

            //#region NUTRITION
            //if (nutrition.Count > 0)
            //{
            //    if (nutrition.Where(s => s.ClientId == clientId).Count() > 0)
            //    {
            //        model.Nutrition_AvoidFood = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().AvoidFood;
            //        model.Nutrition_DrinkType = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().DrinkType;
            //        model.Nutrition_EatingDifficulty = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().EatingDifficulty;
            //        model.Nutrition_FoodIntake = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().FoodIntake;
            //        model.Nutrition_FoodStorage = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().FoodStorage;
            //        model.Nutrition_MealPreparation = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().MealPreparation;
            //        model.NutritionId = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().NutritionId;
            //        model.Nutrition_RiskMitigations = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().RiskMitigations;
            //        model.Nutrition_ServingMeal = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().ServingMeal;
            //        model.Nutrition_SpecialDiet = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().SpecialDiet;
            //        model.Nutrition_ThingsILike = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().ThingsILike;
            //        model.Nutrition_WhenRestock = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().WhenRestock;
            //        model.Nutrition_WhoRestock = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().WhoRestock;

            //    }

            //}
            //#endregion

            //#region HYGIENE
            //if (infection.Count > 0)
            //{
            //    if(infection.Where(s => s.ClientId == clientId).Count() > 0)
            //    { 
            //        model.Infection_Guideline = infection.Where(s => s.ClientId == clientId).FirstOrDefault().Guideline;
            //        model.InfectionId = infection.Where(s => s.ClientId == clientId).FirstOrDefault().InfectionId;
            //        model.Infection_Remarks = infection.Where(s => s.ClientId == clientId).FirstOrDefault().Remarks;
            //        model.Infection_Status = infection.Where(s => s.ClientId == clientId).FirstOrDefault().Status;
            //        model.Infection_TestDate = infection.Where(s => s.ClientId == clientId).FirstOrDefault().TestDate;
            //        model.Infection_Type = infection.Where(s => s.ClientId == clientId).FirstOrDefault().Type;
            //        model.Infection_VaccStatus = infection.Where(s => s.ClientId == clientId).FirstOrDefault().VaccStatus;
            //    }
            //}
            //if (mtask.Count > 0)
            //    model.GetManagingTasks = mtask.Where(s=>s.ClientId==clientId).ToList();

            //if (hygiene.Count > 0)
            //{
            //    if(hygiene.Where(s => s.ClientId == clientId).Count() > 0)
            //    { 
            //        model.HygieneId = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().HygieneId;
            //        model.Hygiene_Cleaning = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().Cleaning;
            //        model.Hygiene_CleaningFreq = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().CleaningFreq;
            //        model.Hygiene_CleaningTools = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().CleaningTools;
            //        model.Hygiene_DesiredCleaning = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().DesiredCleaning;
            //        model.Hygiene_DirtyLaundry = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().DirtyLaundry;
            //        model.Hygiene_DryLaundry = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().DryLaundry;
            //        model.Hygiene_GeneralAppliance = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().GeneralAppliance;
            //        model.Hygiene_Ironing = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().Ironing;
            //        model.Hygiene_LaundryGuide = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().LaundryGuide;
            //        model.Hygiene_LaundrySupport = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().LaundrySupport;
            //        model.Hygiene_WashingMachine = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().WashingMachine;
            //        model.Hygiene_WhoClean = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().WhoClean;
            //    }
            //}

            //#endregion

            //#region INTEREST AND OBJECTIVE
            //if (obj != null)
            //{ 
            //    model.Interest_CareGoal = obj.CareGoal;
            //    model.Interest_Brief = obj.Brief;
            //    model.GetInterest = obj.Interest;
            //    model.GetPersonalityTest = obj.PersonalityTest;
            //}

            //if (pets.Count > 0)
            //{ 
            //    if(pets.Where(s => s.ClientId == clientId).Count() > 0)
            //    { 
            //        model.Pet_Age = pets.Where(s => s.ClientId == clientId).FirstOrDefault().Age;
            //        model.Pet_Type = pets.Where(s => s.ClientId == clientId).FirstOrDefault().Type;
            //        model.PetsId = pets.Where(s => s.ClientId == clientId).FirstOrDefault().PetsId;
            //        model.Pet_Name = pets.Where(s => s.ClientId == clientId).FirstOrDefault().Name;
            //        model.Pet_Gender = pets.Where(s => s.ClientId == clientId).FirstOrDefault().Gender;
            //        model.PetActivities = pets.Where(s => s.ClientId == clientId).FirstOrDefault().PetActivities;
            //        model.PetCare = pets.Where(s => s.ClientId == clientId).FirstOrDefault().PetCare;
            //        model.Pet_MealPattern = pets.Where(s => s.ClientId == clientId).FirstOrDefault().MealPattern;
            //        model.PetInsurance = pets.Where(s => s.ClientId == clientId).FirstOrDefault().PetInsurance;
            //        model.Pet_MealStorage = pets.Where(s => s.ClientId == clientId).FirstOrDefault().MealStorage;
            //        model.Pet_VetVisit = pets.Where(s => s.ClientId == clientId).FirstOrDefault().VetVisit;
            //    }
            //}
            //#endregion

            //#region HEALTH
            //if (balance.Count > 0) 
            //{ 
            //    if(balance.Where(s => s.ClientId == clientId).Count() > 0)
            //    { 
            //        model.Balance_Description = balance.Where(s => s.ClientId == clientId).FirstOrDefault().Description;
            //        model.Balance_Mobility = balance.Where(s => s.ClientId == clientId).FirstOrDefault().Mobility;
            //        model.BalanceId = balance.Where(s => s.ClientId == clientId).FirstOrDefault().BalanceId;
            //        model.Balance_Name = balance.Where(s => s.ClientId == clientId).FirstOrDefault().Name;
            //        model.Balance_Status = balance.Where(s => s.ClientId == clientId).FirstOrDefault().Status;
            //    }
            //}
            //if (physical.Count > 0) 
            //{ 
            //    if(physical.Where(s => s.ClientId == clientId).Count() > 0)
            //    { 
            //        model.Physical_Description = physical.Where(s => s.ClientId == clientId).FirstOrDefault().Description;
            //        model.Physical_Mobility = physical.Where(s => s.ClientId == clientId).FirstOrDefault().Mobility;
            //        model.PhysicalId = physical.Where(s => s.ClientId == clientId).FirstOrDefault().PhysicalId;
            //        model.Physical_Name = physical.Where(s => s.ClientId == clientId).FirstOrDefault().Name;
            //        model.Physical_Status = physical.Where(s => s.ClientId == clientId).FirstOrDefault().Status;
            //    }
            //}
            //if (hl.Count > 0) 
            //{ 
            //    if(hl.Where(s => s.ClientId == clientId).Count() > 0)
            //    { 
            //        model.HL_AbilityToRead = hl.Where(s => s.ClientId == clientId).FirstOrDefault().AbilityToRead;
            //        model.HL_AlcoholicDrink = hl.Where(s => s.ClientId == clientId).FirstOrDefault().AlcoholicDrink;
            //        model.HL_AllowChats = hl.Where(s => s.ClientId == clientId).FirstOrDefault().AllowChats;
            //        model.HL_BriefHealth = hl.Where(s => s.ClientId == clientId).FirstOrDefault().BriefHealth;
            //        model.HL_CareSupport = hl.Where(s => s.ClientId == clientId).FirstOrDefault().CareSupport;
            //        model.HL_ConstraintAttachment = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ConstraintAttachment;
            //        model.HL_ConstraintDetails = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ConstraintDetails;
            //        model.HL_ConstraintRequired = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ConstraintRequired;
            //        model.HL_ContinenceIssue = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ContinenceIssue;
            //        model.HL_ContinenceNeeds = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ContinenceNeeds;
            //        model.HL_ContinenceSource = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ContinenceSource;
            //        model.HL_DehydrationRisk = hl.Where(s => s.ClientId == clientId).FirstOrDefault().DehydrationRisk;
            //        model.HL_EatingWithStaff = hl.Where(s => s.ClientId == clientId).FirstOrDefault().EatingWithStaff;
            //        model.HL_Email = hl.Where(s => s.ClientId == clientId).FirstOrDefault().Email;
            //        model.HL_FamilyUpdate = hl.Where(s => s.ClientId == clientId).FirstOrDefault().FamilyUpdate;
            //        model.HL_FinanceManagement = hl.Where(s => s.ClientId == clientId).FirstOrDefault().FinanceManagement;
            //        model.HLId = hl.Where(s => s.ClientId == clientId).FirstOrDefault().HLId;
            //        model.HL_LaundaryRequired = hl.Where(s => s.ClientId == clientId).FirstOrDefault().LaundaryRequired;
            //        model.HL_LetterOpening = hl.Where(s => s.ClientId == clientId).FirstOrDefault().LetterOpening;
            //        model.HL_LifeStyle = hl.Where(s => s.ClientId == clientId).FirstOrDefault().LifeStyle;
            //        model.HL_MeansOfComm = hl.Where(s => s.ClientId == clientId).FirstOrDefault().MeansOfComm;
            //        model.HL_MovingAndHandling = hl.Where(s => s.ClientId == clientId).FirstOrDefault().MovingAndHandling;
            //        model.HL_NeighbourInvolment = hl.Where(s => s.ClientId == clientId).FirstOrDefault().NeighbourInvolment;
            //        model.HL_ObserveHealth = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ObserveHealth;
            //        model.HL_PostalService = hl.Where(s => s.ClientId == clientId).FirstOrDefault().PostalService;
            //        model.HL_PressureSore = hl.Where(s => s.ClientId == clientId).FirstOrDefault().PressureSore;
            //        model.HL_ShoppingRequired = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ShoppingRequired;
            //        model.HL_Smoking = hl.Where(s => s.ClientId == clientId).FirstOrDefault().Smoking;
            //        model.HL_SpecialCaution = hl.Where(s => s.ClientId == clientId).FirstOrDefault().SpecialCaution;
            //        model.HL_SpecialCleaning = hl.Where(s => s.ClientId == clientId).FirstOrDefault().SpecialCleaning;
            //        model.HL_SupportToBed = hl.Where(s => s.ClientId == clientId).FirstOrDefault().SupportToBed;
            //        model.HL_TeaChocolateCoffee = hl.Where(s => s.ClientId == clientId).FirstOrDefault().TeaChocolateCoffee;
            //        model.HL_TextFontSize = hl.Where(s => s.ClientId == clientId).FirstOrDefault().TextFontSize;
            //        model.HL_TVandMusic = hl.Where(s => s.ClientId == clientId).FirstOrDefault().TVandMusic;
            //        model.HL_VideoCallRequired = hl.Where(s => s.ClientId == clientId).FirstOrDefault().VideoCallRequired;
            //        model.HL_WakeUp = hl.Where(s => s.ClientId == clientId).FirstOrDefault().WakeUp;
            //    }
            //}
            //if (sham.Count > 0) 
            //{ 
            //    if(sham.Where(s => s.ClientId == clientId).Count() > 0)
            //    { 
            //        model.SHAM_AccessMedication = sham.Where(s => s.ClientId == clientId).FirstOrDefault().AccessMedication;
            //        model.SHAM_AdminLvl = sham.Where(s => s.ClientId == clientId).FirstOrDefault().AdminLvl;
            //        model.SHAM_By = sham.Where(s => s.ClientId == clientId).FirstOrDefault().By;
            //        model.SHAM_Consent = sham.Where(s => s.ClientId == clientId).FirstOrDefault().Consent;
            //        model.SHAM_Date = sham.Where(s => s.ClientId == clientId).FirstOrDefault().Date;
            //        model.SHAM_FamilyMeds = sham.Where(s => s.ClientId == clientId).FirstOrDefault().FamilyMeds;
            //        model.SHAM_MedKeyCode = sham.Where(s => s.ClientId == clientId).FirstOrDefault().MedKeyCode;
            //        model.SHAM_MedicationAllergy = sham.Where(s => s.ClientId == clientId).FirstOrDefault().MedicationAllergy;
            //        model.SHMId = sham.Where(s => s.ClientId == clientId).FirstOrDefault().SHMId;
            //        model.SHAM_LeftoutMedicine = sham.Where(s => s.ClientId == clientId).FirstOrDefault().LeftoutMedicine;
            //        model.SHAM_MedAccessDenial = sham.Where(s => s.ClientId == clientId).FirstOrDefault().MedAccessDenial;
            //        model.SHAM_MedicationStorage = sham.Where(s => s.ClientId == clientId).FirstOrDefault().MedicationStorage;
            //        model.SHAM_NameFormMedicaiton = sham.Where(s => s.ClientId == clientId).FirstOrDefault().NameFormMedicaiton;
            //        model.SHAM_PharmaMARChart = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PharmaMARChart;
            //        model.SHAM_PNRDoses = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PNRDoses;
            //        model.SHAM_PNRMedsMissing = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PNRMedsMissing;
            //        model.SHAM_SpecialStorage = sham.Where(s => s.ClientId == clientId).FirstOrDefault().SpecialStorage;
            //        model.SHAM_NoMedAccess = sham.Where(s => s.ClientId == clientId).FirstOrDefault().NoMedAccess;
            //        model.SHAM_MedsGPOrder = sham.Where(s => s.ClientId == clientId).FirstOrDefault().MedsGPOrder;
            //        model.SHAM_WhoAdminister = sham.Where(s => s.ClientId == clientId).FirstOrDefault().WhoAdminister;
            //        model.SHAM_PNRMedsAdmin = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PNRMedsAdmin;
            //        model.SHAM_PNRMedList = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PNRMedList;
            //        model.SHAM_OverdoseContact = sham.Where(s => s.ClientId == clientId).FirstOrDefault().OverdoseContact;
            //        model.SHAM_TempMARChart = sham.Where(s => s.ClientId == clientId).FirstOrDefault().TempMARChart;
            //        model.SHAM_FamilyReturnMed = sham.Where(s => s.ClientId == clientId).FirstOrDefault().FamilyReturnMed;
            //        model.SHAM_PNRMedReq = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PNRMedReq;
            //        model.SHAM_Type = sham.Where(s => s.ClientId == clientId).FirstOrDefault().Type;
            //    }
            //}
            //if (shc.Count > 0) 
            //{ 
            //    if(shc.Where(s => s.ClientId == clientId).Count() > 0)
            //    { 
            //        model.SHC_ClientAction = shc.Where(s => s.ClientId == clientId).FirstOrDefault().ClientAction;
            //        model.SHC_ClinicRecommendation = shc.Where(s => s.ClientId == clientId).FirstOrDefault().ClinicRecommendation;
            //        model.SHC_ConditionName = shc.Where(s => s.ClientId == clientId).FirstOrDefault().ConditionName;
            //        model.SHC_FeelingAfterIncident = shc.Where(s => s.ClientId == clientId).FirstOrDefault().FeelingAfterIncident;
            //        model.SHC_LivingActivities = shc.Where(s => s.ClientId == clientId).FirstOrDefault().LivingActivities;
            //        model.SHC_FeelingBeforeIncident = shc.Where(s => s.ClientId == clientId).FirstOrDefault().FeelingBeforeIncident;
            //        model.SHC_Frequency = shc.Where(s => s.ClientId == clientId).FirstOrDefault().Frequency;
            //        model.HealthCondId = shc.Where(s => s.ClientId == clientId).FirstOrDefault().HealthCondId;
            //        model.SHC_LifestyleSupport = shc.Where(s => s.ClientId == clientId).FirstOrDefault().LifestyleSupport;
            //        model.SHC_PlanningHealthCondition = shc.Where(s => s.ClientId == clientId).FirstOrDefault().PlanningHealthCondition;
            //        model.SHC_SourceInformation = shc.Where(s => s.ClientId == clientId).FirstOrDefault().SourceInformation;
            //        model.SHC_Trigger = shc.Where(s => s.ClientId == clientId).FirstOrDefault().Trigger;
            //    }
            //}
            //if (history.Count > 0) 
            //{
            //    if (history.Where(s => s.ClientId == clientId).Count() > 0)
            //    { 
            //        model.HistoryId = history.Where(s => s.ClientId == clientId).FirstOrDefault().HistoryId;
            //        model.History_Cause = history.Where(s => s.ClientId == clientId).FirstOrDefault().Cause;
            //        model.History_Date = history.Where(s => s.ClientId == clientId).FirstOrDefault().Date;
            //        model.History_Details = history.Where(s => s.ClientId == clientId).FirstOrDefault().Details;
            //        model.History_Prevention = history.Where(s => s.ClientId == clientId).FirstOrDefault().Prevention;
            //    }
            //}
            //#endregion

            //#region ClassList
            //model.ClassList = classItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            //foreach (var item in model.ClassList)
            //{
            //    var child = bases.Where(s => s.KeyName == item.Text).FirstOrDefault().BaseRecordId;
            //    var childItems = await _baseRecord.GetBaseRecordWithItems(child);

            //    if (item.Text.ToString() == "Individuality")
            //    {
            //        model.Individuality = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            //        model.FocusList.Add(item.Text, model.Individuality);
            //    }
            //    if (item.Text.ToString() == "RightsAndRespect")
            //    {
            //        model.RightsAndRespect = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            //        model.FocusList.Add(item.Text, model.RightsAndRespect);
            //    }
            //    if (item.Text.ToString() == "Choice")
            //    {
            //        model.Choice = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            //        model.FocusList.Add(item.Text, model.Choice);
            //    }
            //    if (item.Text.ToString() == "DignityAndPrivacy")
            //    {
            //        model.DignityAndPrivacy = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            //        model.FocusList.Add(item.Text, model.DignityAndPrivacy);
            //    }
            //    if (item.Text.ToString() == "Partnership")
            //    {
            //        model.Partnership = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            //        model.FocusList.Add(item.Text, model.Partnership);
            //    }
            //}
            //int i = 1;
            //if(model.GetPersonCentred != null)
            //{ 
            //    foreach (var item in model.GetPersonCentred)
            //{
            //    if (i == 1)
            //        model.Focus1 = item.Focus.Select(s => s.BaseRecordId).ToList();
            //    if (i == 2)
            //        model.Focus2 = item.Focus.Select(s => s.BaseRecordId).ToList();
            //    if (i == 3)
            //        model.Focus3 = item.Focus.Select(s => s.BaseRecordId).ToList();
            //    if (i == 4)
            //        model.Focus4 = item.Focus.Select(s => s.BaseRecordId).ToList();
            //    if (i == 5)
            //        model.Focus5 = item.Focus.Select(s => s.BaseRecordId).ToList();
            //    i++;
            //}
            //}
            //#endregion
            CreateCarePlanView model = await GetCarPlan(clientId);
            return View(model);
        }
        public async Task<CreateCarePlanView> GetCarPlan(int clientId)
        {
            var bases = await _baseRecord.GetBaseRecord();
            var baseClass = bases.Where(s => s.KeyName == "Class").FirstOrDefault().BaseRecordId;
            var classItems = await _baseRecord.GetBaseRecordWithItems(baseClass);

            var pdetail = await _pdetailService.Get(clientId);
            var nutrition = await _nutritionService.Get();
            var infection = await _infectionService.Get();
            var mtask = await _mataskService.Get();
            var hygiene = await _phygieneService.Get();
            var Intobj = await _interestService.Get(clientId);
            var pets = await _petsService.Get();
            var balance = await _balanceService.Get();
            var physical = await _physicalService.Get();
            var hl = await _hlService.Get();
            var history = await _historyService.Get();
            var sham = await _shamService.Get();
            var shc = await _shcService.Get();
            var home = await _clientHomeRiskAssessment.GetByClient(clientId);
            var party = await _involvingparty.GetAll();
            var InvolvingParty = party.Where(s => s.ClientId == clientId).Select(s=>s.Relationship);
            var Relation = "N/A";
            if (InvolvingParty != null)
            {
                Relation = InvolvingParty.FirstOrDefault();
            }
            var model = new CreateCarePlanView();
            var client = await _clientService.GetClient(clientId);
            QRCodeData qrCodeData = _qRCodeGenerator.CreateQrCode(client.UniqueId, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            model.QRCode = qrCodeImage.ToByteArray();
            model.PassportFilePath = client.PassportFilePath;
            model.ClientName = client.PreferredName;
            model.ClientId = clientId;
            model.IdNumber = client.IdNumber;
            model.GetInvolvingParty = party.Where(s => s.ClientId == clientId).ToList(); 
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Home_Risk_Assessment_Heading").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            
            #region Home Risk
            if (home.Count > 0)
            {
                model.TaskCountHRA = home.Count();
                model.GetHomeRiskAssessments = home.ToList();
                model.HeadingList = home.Select(s => new SelectListItem(s.Heading, s.HomeRiskAssessmentId.ToString())).ToList();
            }
            #endregion
            
            #region PERSONAL DETAILS
            if (pdetail != null)
            {
                model.ClientId = clientId;
                model.PersonalDetailId = pdetail.PersonalDetailId;
                model.CapacityId = pdetail.Capacity.FirstOrDefault().CapacityId;
                model.Implications = pdetail.Capacity.FirstOrDefault().Implications;
                model.Pointer = pdetail.Capacity.FirstOrDefault().Pointer;
                model.IndicatorList = pdetail.Capacity.FirstOrDefault().Indicator.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList();
                model.Indicator = pdetail.Capacity.FirstOrDefault().Indicator.Select(s => s.BaseRecordId).ToList();
                model.CareId = pdetail.ConsentCare.FirstOrDefault().CareId;
                model.CareSignature = pdetail.ConsentCare.FirstOrDefault().Signature;
                model.CareDate = pdetail.ConsentCare.FirstOrDefault().Date;
                model.CareName = pdetail.ConsentCare.FirstOrDefault().Name;
                model.CareRelation = Relation;
                model.DataId = pdetail.ConsentData.FirstOrDefault().DataId;
                model.DataSignature = pdetail.ConsentData.FirstOrDefault().Signature;
                model.DataDate = pdetail.ConsentData.FirstOrDefault().Date;
                model.DataName = pdetail.ConsentData.FirstOrDefault().Name;
                model.DataRelation = Relation;
                model.LandLineId = pdetail.ConsentLandline.FirstOrDefault().LandlineId;
                model.LandLineSignature = pdetail.ConsentLandline.FirstOrDefault().Signature;
                model.LandLineDate = pdetail.ConsentLandline.FirstOrDefault().Date;
                model.LandLogList = pdetail.ConsentLandline.FirstOrDefault().LogMethod.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList();
                model.LandLineLogMethod = pdetail.ConsentLandline.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList();
                model.LandName = pdetail.ConsentLandline.FirstOrDefault().Name;
                model.LandRelation = Relation;
                model.GetEquipment = pdetail.Equipment;
                model.AboutMe = pdetail.KeyIndicators.FirstOrDefault().AboutMe;
                model.KeyId = pdetail.KeyIndicators.FirstOrDefault().KeyId;
                model.FamilyRole = pdetail.KeyIndicators.FirstOrDefault().FamilyRole;
                model.Debture = pdetail.KeyIndicators.FirstOrDefault().Debture;
                model.LivingStatus = pdetail.KeyIndicators.FirstOrDefault().LivingStatus;
                model.KeyLogList = pdetail.KeyIndicators.FirstOrDefault().LogMethod.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList();
                model.LogMethod = pdetail.KeyIndicators.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList();
                model.ThingsILike = pdetail.KeyIndicators.FirstOrDefault().ThingsILike;
                model.PersonalId = pdetail.Personal.FirstOrDefault().PersonalId;
                model.Smoking = pdetail.Personal.FirstOrDefault().Smoking;
                model.DNR = pdetail.Personal.FirstOrDefault().DNR;
                model.FullName = model.ClientName;
                model.PreferredLanguage = client.LanguageId;
                model.Gender = client.GenderId;
                model.DateofBirth = client.DateOfBirth;
                model.Address = client.Address;
                model.PostCode = client.PostCode;
                model.Telephone = client.Telephone;
                model.PreferredName = client.Firstname;
                model.AccessCode = client.KeySafe;
                model.PreferredGender = client.ChoiceOfStaffId;
                model.KeyWorker = client.Keyworker;
                model.TeamLeader = client.TeamLeader;
                model.Religion = pdetail.Personal.FirstOrDefault().Religion;
                model.Nationality = pdetail.Personal.FirstOrDefault().Nationality;
                model.GetPersonCentred = pdetail.PersonCentred;
                model.ReviewId = pdetail.Review.FirstOrDefault().ReviewId;
                model.CP_PreDate = pdetail.Review.FirstOrDefault().CP_PreDate;
                model.CP_ReviewDate = pdetail.Review.FirstOrDefault().CP_ReviewDate;
                model.RA_PreDate = pdetail.Review.FirstOrDefault().RA_PreDate;
                model.RA_ReviewDate = pdetail.Review.FirstOrDefault().RA_ReviewDate;
            }
            #endregion

            #region NUTRITION
            if (nutrition.Count > 0)
            {
                if (nutrition.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    model.Nutrition_AvoidFood = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().AvoidFood;
                    model.Nutrition_DrinkType = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().DrinkType;
                    model.Nutrition_EatingDifficulty = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().EatingDifficulty;
                    model.Nutrition_FoodIntake = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().FoodIntake;
                    model.Nutrition_FoodStorage = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().FoodStorage;
                    model.Nutrition_MealPreparation = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().MealPreparation;
                    model.NutritionId = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().NutritionId;
                    model.Nutrition_RiskMitigations = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().RiskMitigations;
                    model.Nutrition_ServingMeal = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().ServingMeal;
                    model.Nutrition_SpecialDiet = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().SpecialDiet;
                    model.Nutrition_ThingsILike = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().ThingsILike;
                    model.Nutrition_WhenRestock = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().WhenRestock;
                    model.Nutrition_WhoRestock = nutrition.Where(s => s.ClientId == clientId).FirstOrDefault().WhoRestock;

                }

            }
            #endregion

            #region HYGIENE
            if (infection.Count > 0)
            {
                if (infection.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    model.Infection_Guideline = infection.Where(s => s.ClientId == clientId).FirstOrDefault().Guideline;
                    model.InfectionId = infection.Where(s => s.ClientId == clientId).FirstOrDefault().InfectionId;
                    model.Infection_Remarks = infection.Where(s => s.ClientId == clientId).FirstOrDefault().Remarks;
                    model.Infection_Status = infection.Where(s => s.ClientId == clientId).FirstOrDefault().Status;
                    model.Infection_TestDate = infection.Where(s => s.ClientId == clientId).FirstOrDefault().TestDate;
                    model.Infection_Type = infection.Where(s => s.ClientId == clientId).FirstOrDefault().Type;
                    model.Infection_VaccStatus = infection.Where(s => s.ClientId == clientId).FirstOrDefault().VaccStatus;
                }
            }
            if (mtask.Count > 0)
                if(mtask.Where(s => s.ClientId == clientId).Count() > 0)
                    model.GetManagingTasks = mtask.Where(s => s.ClientId == clientId).ToList();

            if (hygiene.Count > 0)
            {
                if (hygiene.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    model.HygieneId = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().HygieneId;
                    model.Hygiene_Cleaning = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().Cleaning;
                    model.Hygiene_CleaningFreq = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().CleaningFreq;
                    model.Hygiene_CleaningTools = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().CleaningTools;
                    model.Hygiene_DesiredCleaning = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().DesiredCleaning;
                    model.Hygiene_DirtyLaundry = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().DirtyLaundry;
                    model.Hygiene_DryLaundry = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().DryLaundry;
                    model.Hygiene_GeneralAppliance = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().GeneralAppliance;
                    model.Hygiene_Ironing = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().Ironing;
                    model.Hygiene_LaundryGuide = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().LaundryGuide;
                    model.Hygiene_LaundrySupport = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().LaundrySupport;
                    model.Hygiene_WashingMachine = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().WashingMachine;
                    model.Hygiene_WhoClean = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault().WhoClean;
                }
            }

            #endregion

            #region INTEREST AND OBJECTIVE
            if (Intobj != null)
            {
                model.Interest_CareGoal = Intobj.CareGoal;
                model.Interest_Brief = Intobj.Brief;
                model.GetInterest = Intobj.Interest;
                model.GetPersonalityTest = Intobj.PersonalityTest;
            }

            if (pets.Count > 0)
            {
                if (pets.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    model.Pet_Age = pets.Where(s => s.ClientId == clientId).FirstOrDefault().Age;
                    model.Pet_Type = pets.Where(s => s.ClientId == clientId).FirstOrDefault().Type;
                    model.PetsId = pets.Where(s => s.ClientId == clientId).FirstOrDefault().PetsId;
                    model.Pet_Name = pets.Where(s => s.ClientId == clientId).FirstOrDefault().Name;
                    model.Pet_Gender = pets.Where(s => s.ClientId == clientId).FirstOrDefault().Gender;
                    model.PetActivities = pets.Where(s => s.ClientId == clientId).FirstOrDefault().PetActivities;
                    model.PetCare = pets.Where(s => s.ClientId == clientId).FirstOrDefault().PetCare;
                    model.Pet_MealPattern = pets.Where(s => s.ClientId == clientId).FirstOrDefault().MealPattern;
                    model.PetInsurance = pets.Where(s => s.ClientId == clientId).FirstOrDefault().PetInsurance;
                    model.Pet_MealStorage = pets.Where(s => s.ClientId == clientId).FirstOrDefault().MealStorage;
                    model.Pet_VetVisit = pets.Where(s => s.ClientId == clientId).FirstOrDefault().VetVisit;
                }
            }
            #endregion

            #region HEALTH
            if (balance.Count > 0)
            {
                if (balance.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    model.GetBalance = balance.Where(s => s.ClientId == clientId).ToList();
                }
            }
            if (physical.Count > 0)
            {
                if (physical.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    model.Physical_Description = physical.Where(s => s.ClientId == clientId).FirstOrDefault().Description;
                    model.Physical_Mobility = physical.Where(s => s.ClientId == clientId).FirstOrDefault().Mobility;
                    model.PhysicalId = physical.Where(s => s.ClientId == clientId).FirstOrDefault().PhysicalId;
                    model.Physical_Name = physical.Where(s => s.ClientId == clientId).FirstOrDefault().Name;
                    model.Physical_Status = physical.Where(s => s.ClientId == clientId).FirstOrDefault().Status;
                }
            }
            if (hl.Count > 0)
            {
                if (hl.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    model.HL_AbilityToRead = hl.Where(s => s.ClientId == clientId).FirstOrDefault().AbilityToRead;
                    model.HL_AlcoholicDrink = hl.Where(s => s.ClientId == clientId).FirstOrDefault().AlcoholicDrink;
                    model.HL_AllowChats = hl.Where(s => s.ClientId == clientId).FirstOrDefault().AllowChats;
                    model.HL_BriefHealth = hl.Where(s => s.ClientId == clientId).FirstOrDefault().BriefHealth;
                    model.HL_CareSupport = hl.Where(s => s.ClientId == clientId).FirstOrDefault().CareSupport;
                    model.HL_ConstraintAttachment = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ConstraintAttachment;
                    model.HL_ConstraintDetails = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ConstraintDetails;
                    model.HL_ConstraintRequired = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ConstraintRequired;
                    model.HL_ContinenceIssue = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ContinenceIssue;
                    model.HL_ContinenceNeeds = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ContinenceNeeds;
                    model.HL_ContinenceSource = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ContinenceSource;
                    model.HL_DehydrationRisk = hl.Where(s => s.ClientId == clientId).FirstOrDefault().DehydrationRisk;
                    model.HL_EatingWithStaff = hl.Where(s => s.ClientId == clientId).FirstOrDefault().EatingWithStaff;
                    model.HL_Email = hl.Where(s => s.ClientId == clientId).FirstOrDefault().Email;
                    model.HL_FamilyUpdate = hl.Where(s => s.ClientId == clientId).FirstOrDefault().FamilyUpdate;
                    model.HL_FinanceManagement = hl.Where(s => s.ClientId == clientId).FirstOrDefault().FinanceManagement;
                    model.HLId = hl.Where(s => s.ClientId == clientId).FirstOrDefault().HLId;
                    model.HL_LaundaryRequired = hl.Where(s => s.ClientId == clientId).FirstOrDefault().LaundaryRequired;
                    model.HL_LetterOpening = hl.Where(s => s.ClientId == clientId).FirstOrDefault().LetterOpening;
                    model.HL_LifeStyle = hl.Where(s => s.ClientId == clientId).FirstOrDefault().LifeStyle;
                    model.HL_MeansOfComm = hl.Where(s => s.ClientId == clientId).FirstOrDefault().MeansOfComm;
                    model.HL_MovingAndHandling = hl.Where(s => s.ClientId == clientId).FirstOrDefault().MovingAndHandling;
                    model.HL_NeighbourInvolment = hl.Where(s => s.ClientId == clientId).FirstOrDefault().NeighbourInvolment;
                    model.HL_ObserveHealth = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ObserveHealth;
                    model.HL_PostalService = hl.Where(s => s.ClientId == clientId).FirstOrDefault().PostalService;
                    model.HL_PressureSore = hl.Where(s => s.ClientId == clientId).FirstOrDefault().PressureSore;
                    model.HL_ShoppingRequired = hl.Where(s => s.ClientId == clientId).FirstOrDefault().ShoppingRequired;
                    model.HL_Smoking = hl.Where(s => s.ClientId == clientId).FirstOrDefault().Smoking;
                    model.HL_SpecialCaution = hl.Where(s => s.ClientId == clientId).FirstOrDefault().SpecialCaution;
                    model.HL_SpecialCleaning = hl.Where(s => s.ClientId == clientId).FirstOrDefault().SpecialCleaning;
                    model.HL_SupportToBed = hl.Where(s => s.ClientId == clientId).FirstOrDefault().SupportToBed;
                    model.HL_TeaChocolateCoffee = hl.Where(s => s.ClientId == clientId).FirstOrDefault().TeaChocolateCoffee;
                    model.HL_TextFontSize = hl.Where(s => s.ClientId == clientId).FirstOrDefault().TextFontSize;
                    model.HL_TVandMusic = hl.Where(s => s.ClientId == clientId).FirstOrDefault().TVandMusic;
                    model.HL_VideoCallRequired = hl.Where(s => s.ClientId == clientId).FirstOrDefault().VideoCallRequired;
                    model.HL_WakeUp = hl.Where(s => s.ClientId == clientId).FirstOrDefault().WakeUp;
                }
            }
            if (sham.Count > 0)
            {
                if (sham.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    model.SHAM_AccessMedication = sham.Where(s => s.ClientId == clientId).FirstOrDefault().AccessMedication;
                    model.SHAM_AdminLvl = sham.Where(s => s.ClientId == clientId).FirstOrDefault().AdminLvl;
                    model.SHAM_By = sham.Where(s => s.ClientId == clientId).FirstOrDefault().By;
                    model.SHAM_Consent = sham.Where(s => s.ClientId == clientId).FirstOrDefault().Consent;
                    model.SHAM_Date = sham.Where(s => s.ClientId == clientId).FirstOrDefault().Date;
                    model.SHAM_FamilyMeds = sham.Where(s => s.ClientId == clientId).FirstOrDefault().FamilyMeds;
                    model.SHAM_MedKeyCode = sham.Where(s => s.ClientId == clientId).FirstOrDefault().MedKeyCode;
                    model.SHAM_MedicationAllergy = sham.Where(s => s.ClientId == clientId).FirstOrDefault().MedicationAllergy;
                    model.SHMId = sham.Where(s => s.ClientId == clientId).FirstOrDefault().SHMId;
                    model.SHAM_LeftoutMedicine = sham.Where(s => s.ClientId == clientId).FirstOrDefault().LeftoutMedicine;
                    model.SHAM_MedAccessDenial = sham.Where(s => s.ClientId == clientId).FirstOrDefault().MedAccessDenial;
                    model.SHAM_MedicationStorage = sham.Where(s => s.ClientId == clientId).FirstOrDefault().MedicationStorage;
                    model.SHAM_NameFormMedicaiton = sham.Where(s => s.ClientId == clientId).FirstOrDefault().NameFormMedicaiton;
                    model.SHAM_PharmaMARChart = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PharmaMARChart;
                    model.SHAM_PNRDoses = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PNRDoses;
                    model.SHAM_PNRMedsMissing = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PNRMedsMissing;
                    model.SHAM_SpecialStorage = sham.Where(s => s.ClientId == clientId).FirstOrDefault().SpecialStorage;
                    model.SHAM_NoMedAccess = sham.Where(s => s.ClientId == clientId).FirstOrDefault().NoMedAccess;
                    model.SHAM_MedsGPOrder = sham.Where(s => s.ClientId == clientId).FirstOrDefault().MedsGPOrder;
                    model.SHAM_WhoAdminister = sham.Where(s => s.ClientId == clientId).FirstOrDefault().WhoAdminister;
                    model.SHAM_PNRMedsAdmin = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PNRMedsAdmin;
                    model.SHAM_PNRMedList = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PNRMedList;
                    model.SHAM_OverdoseContact = sham.Where(s => s.ClientId == clientId).FirstOrDefault().OverdoseContact;
                    model.SHAM_TempMARChart = sham.Where(s => s.ClientId == clientId).FirstOrDefault().TempMARChart;
                    model.SHAM_FamilyReturnMed = sham.Where(s => s.ClientId == clientId).FirstOrDefault().FamilyReturnMed;
                    model.SHAM_PNRMedReq = sham.Where(s => s.ClientId == clientId).FirstOrDefault().PNRMedReq;
                    model.SHAM_Type = sham.Where(s => s.ClientId == clientId).FirstOrDefault().Type;
                }
            }
            if (shc.Count > 0)
            {
                if (shc.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    model.SHC_ClientAction = shc.Where(s => s.ClientId == clientId).FirstOrDefault().ClientAction;
                    model.SHC_ClinicRecommendation = shc.Where(s => s.ClientId == clientId).FirstOrDefault().ClinicRecommendation;
                    model.SHC_ConditionName = shc.Where(s => s.ClientId == clientId).FirstOrDefault().ConditionName;
                    model.SHC_FeelingAfterIncident = shc.Where(s => s.ClientId == clientId).FirstOrDefault().FeelingAfterIncident;
                    model.SHC_LivingActivities = shc.Where(s => s.ClientId == clientId).FirstOrDefault().LivingActivities;
                    model.SHC_FeelingBeforeIncident = shc.Where(s => s.ClientId == clientId).FirstOrDefault().FeelingBeforeIncident;
                    model.SHC_Frequency = shc.Where(s => s.ClientId == clientId).FirstOrDefault().Frequency;
                    model.HealthCondId = shc.Where(s => s.ClientId == clientId).FirstOrDefault().HealthCondId;
                    model.SHC_LifestyleSupport = shc.Where(s => s.ClientId == clientId).FirstOrDefault().LifestyleSupport;
                    model.SHC_PlanningHealthCondition = shc.Where(s => s.ClientId == clientId).FirstOrDefault().PlanningHealthCondition;
                    model.SHC_SourceInformation = shc.Where(s => s.ClientId == clientId).FirstOrDefault().SourceInformation;
                    model.SHC_Trigger = shc.Where(s => s.ClientId == clientId).FirstOrDefault().Trigger;
                }
            }
            if (history.Count > 0)
            {
                if (history.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    model.HistoryId = history.Where(s => s.ClientId == clientId).FirstOrDefault().HistoryId;
                    model.History_Cause = history.Where(s => s.ClientId == clientId).FirstOrDefault().Cause;
                    model.History_Date = history.Where(s => s.ClientId == clientId).FirstOrDefault().Date;
                    model.History_Details = history.Where(s => s.ClientId == clientId).FirstOrDefault().Details;
                    model.History_Prevention = history.Where(s => s.ClientId == clientId).FirstOrDefault().Prevention;
                }
            }
            #endregion

            #region ClassList
            model.ClassList = classItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
            foreach (var item in model.ClassList)
            {
                var child = bases.Where(s => s.KeyName == item.Text).FirstOrDefault().BaseRecordId;
                var childItems = await _baseRecord.GetBaseRecordWithItems(child);

                if (item.Text.ToString() == "Individuality")
                {
                    model.Individuality = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text, model.Individuality);
                }
                if (item.Text.ToString() == "RightsAndRespect")
                {
                    model.RightsAndRespect = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text, model.RightsAndRespect);
                }
                if (item.Text.ToString() == "Choice")
                {
                    model.Choice = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text, model.Choice);
                }
                if (item.Text.ToString() == "DignityAndPrivacy")
                {
                    model.DignityAndPrivacy = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text, model.DignityAndPrivacy);
                }
                if (item.Text.ToString() == "Partnership")
                {
                    model.Partnership = childItems.BaseRecordItems.Select(s => new SelectListItem(s.ValueName, s.BaseRecordItemId.ToString())).ToList();
                    model.FocusList.Add(item.Text, model.Partnership);
                }
            }
            int i = 1;
            if (model.GetPersonCentred.Count > 0)
            {
                foreach (var item in model.GetPersonCentred)
                {
                    if (i == 1)
                        model.Focus1 = item.Focus.Select(s => s.BaseRecordId).ToList();
                    if (i == 2)
                        model.Focus2 = item.Focus.Select(s => s.BaseRecordId).ToList();
                    if (i == 3)
                        model.Focus3 = item.Focus.Select(s => s.BaseRecordId).ToList();
                    if (i == 4)
                        model.Focus4 = item.Focus.Select(s => s.BaseRecordId).ToList();
                    if (i == 5)
                        model.Focus5 = item.Focus.Select(s => s.BaseRecordId).ToList();
                    i++;
                }
            }
            #endregion
            return model;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _pdetailService.Get();
            var staff = _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            List<CreateCarePlanView> reports = new List<CreateCarePlanView>();
            foreach (GetPersonalDetail item in entities)
            {
                var report = new CreateCarePlanView();
                report.PersonalDetailId = item.PersonalDetailId;
                report.ClientId = item.ClientId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                reports.Add(report);
            }
            return View(reports);
        }
    }
}
