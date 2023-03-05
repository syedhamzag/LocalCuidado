using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Balance;
using AwesomeCare.Admin.Services.BestInterestAssessment;
using AwesomeCare.Admin.Services.CarePlanNutrition;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientProgram;
using AwesomeCare.Admin.Services.ClientServiceWatch;
using AwesomeCare.Admin.Services.ClientDailyTask;
using AwesomeCare.Admin.Services.ClientInvolvingParty;
using AwesomeCare.Admin.Services.ClientRota;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.Services.HealthAndLiving;
using AwesomeCare.Admin.Services.HistoryOfFall;
using AwesomeCare.Admin.Services.InfectionControl;
using AwesomeCare.Admin.Services.InterestAndObjective;
using AwesomeCare.Admin.Services.ManagingTasks;
using AwesomeCare.Admin.Services.Nutrition;
using AwesomeCare.Admin.Services.PersonalDetail;
using AwesomeCare.Admin.Services.PersonalHygiene;
using AwesomeCare.Admin.Services.Pets;
using AwesomeCare.Admin.Services.PhysicalAbility;
using AwesomeCare.Admin.Services.RotaDayofWeek;
using AwesomeCare.Admin.Services.RotaTask;
using AwesomeCare.Admin.Services.SpecialHealthAndMedication;
using AwesomeCare.Admin.Services.SpecialHealthCondition;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.CarePlan;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment;
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
        private IClientDailyTaskService _clientDailyTaskService;
        private IClientProgramService _clientProgramService;
        private IClientServiceWatchService _clientServiceWatchService;
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
        IClientRotaTypeService _clientRotaTypeService;
        IClientRotaNameService _clientRotaNameService;
        IRotaTaskService _rotaTaskService;
        IRotaDayofWeekService _rotaDayOfWeekService;
        IClientRotaService _clientRotaService;
        private INutritionService _clientNutritionService;
        private readonly QRCodeGenerator _qRCodeGenerator;
        private IBestInterestAssessmentService _bestInterestAssessment;

        public CarePlanController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IPersonalDetailService pdetailService,
                                    IBaseRecordService baseRecord, IClientInvolvingParty involvingparty, ICarePlanNutritionService nutritionService, INutritionService clientNutritionService,
                                    IPersonalHygieneService phygieneService, IInfectionControlService infectionService, IManagingTasksService mataskService,
                                    IPetsService petsService, IInterestAndObjectiveService interestService, IBalanceService balanceService, IPhysicalAbilityService physicalService,
                                    IHistoryOfFallService historyService, IHealthAndLivingService hlService, ISpecialHealthAndMedicationService shamService, IBestInterestAssessmentService bestInterestAssessment,
                                    ISpecialHealthConditionService shcService, IHomeRiskAssessmentService clientHomeRiskAssessment, QRCodeGenerator qRCodeGenerator, IClientRotaService clientRotaService,
                                    IRotaDayofWeekService rotaDayOfWeekService, IRotaTaskService rotaTaskService, IClientRotaTypeService clientRotaTypeService, IClientRotaNameService clientRotaNameService,
                                    IClientDailyTaskService clientDailyTaskService, IClientProgramService clientProgramService, IClientServiceWatchService clientServiceWatchService) : base(fileUpload)
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
            _clientRotaTypeService = clientRotaTypeService;
            _clientRotaNameService = clientRotaNameService;
            _rotaTaskService = rotaTaskService;
            _rotaDayOfWeekService = rotaDayOfWeekService;
            _clientRotaService = clientRotaService;
            _clientNutritionService = clientNutritionService;
            _bestInterestAssessment = bestInterestAssessment;
            _clientDailyTaskService = clientDailyTaskService;
            _clientProgramService = clientProgramService;
            _clientServiceWatchService = clientServiceWatchService;
        }
        public async Task<IActionResult> CareView(int clientId)
        {
            CreateCarePlanView model = await GetCarPlan(clientId);
            return View(model);
        }
        public async Task<CreateCarePlanView> GetCarPlan(int clientId)
        {
            var staff = await _staffService.GetStaffs();
            var bases = await _baseRecord.GetBaseRecord();
            var baseClass = bases.Where(s => s.KeyName == "Class").FirstOrDefault().BaseRecordId;
            var classItems = await _baseRecord.GetBaseRecordWithItems(baseClass);

            var clientNutrition = await _clientNutritionService.GetForEdit(clientId);
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
            var InvolvingParty = party.Where(s => s.ClientId == clientId).Select(s => s.Relationship);
            var Relation = "N/A";
            var program = await _clientProgramService.Get();
            var daily = await _clientDailyTaskService.Get();
            var swatch = await _clientServiceWatchService.Get();
            if (InvolvingParty != null)
            {
                Relation = InvolvingParty.FirstOrDefault();
            }
            var model = new CreateCarePlanView();
            var client = await _clientService.GetClient(clientId);
            model.GetClientHealthCondition = client.GetClientHealthCondition.ToList();
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
            var baseRecordItem = await _baseRecord.GetBaseRecordItem();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Home_Risk_Assessment_Heading").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = baseRecordItem.ToList();
            #region Client Nutrition
            if (clientNutrition.Count > 0)
            {
                model.ClientCleaning = clientNutrition.FirstOrDefault().ClientCleaning;
                model.ClientShopping = clientNutrition.FirstOrDefault().ClientShopping;
                model.ClientMealDays = clientNutrition.FirstOrDefault().ClientMealDays;
            }
            #endregion
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
                model.IndicatorList = pdetail.Capacity.FirstOrDefault().Indicator.Select(s => s.ValueName).ToList();
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
                model.LandLogList = pdetail.ConsentLandline.FirstOrDefault().LogMethod.Select(s => s.ValueName).ToList();
                model.LandLineLogMethod = pdetail.ConsentLandline.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList();
                model.LandName = pdetail.ConsentLandline.FirstOrDefault().Name;
                model.LandRelation = Relation;
                model.GetEquipment = pdetail.Equipment;
                model.StaffList = staff.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                model.AboutMe = pdetail.KeyIndicators.FirstOrDefault().AboutMe;
                model.KeyId = pdetail.KeyIndicators.FirstOrDefault().KeyId;
                model.FamilyRole = pdetail.KeyIndicators.FirstOrDefault().FamilyRole;
                model.Debture = pdetail.KeyIndicators.FirstOrDefault().Debture;
                model.LivingStatus = pdetail.KeyIndicators.FirstOrDefault().LivingStatus;
                model.KeyLogList = pdetail.KeyIndicators.FirstOrDefault().LogMethod.Select(s => s.ValueName).ToList();
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
                model.InvolingList = party.Where(s => s.ClientId == clientId).Select(s => new SelectListItem(s.Name, s.ClientInvolvingPartyId.ToString())).ToList();
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
                if (mtask.Where(s => s.ClientId == clientId).Count() > 0)
                    model.GetManagingTasks = mtask.Where(s => s.ClientId == clientId).ToList();

            if (hygiene.Count > 0)
            {
                if (hygiene.Where(s => s.ClientId == clientId).Count() > 0)
                {
                    var phygiene = hygiene.Where(s => s.ClientId == clientId).FirstOrDefault();
                    model.HygieneId = phygiene.HygieneId;
                    model.Hygiene_Cleaning = phygiene.Cleaning;
                    model.Hygiene_CleaningFreq = phygiene.CleaningFreq;
                    model.Hygiene_CleaningTools = phygiene.CleaningTools;
                    model.Hygiene_DesiredCleaning = phygiene.DesiredCleaning;
                    model.Hygiene_DirtyLaundry = phygiene.DirtyLaundry;
                    model.Hygiene_DryLaundry = phygiene.DryLaundry;
                    model.Hygiene_GeneralAppliance = phygiene.GeneralAppliance;
                    model.Hygiene_Ironing = phygiene.Ironing;
                    model.Hygiene_LaundryGuide = phygiene.LaundryGuide;
                    model.Hygiene_LaundrySupport = phygiene.LaundrySupport;
                    model.Hygiene_WashingMachine = phygiene.WashingMachine;
                    model.Hygiene_WhoClean = phygiene.WhoClean;
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
                    model.PhysicalAbility = physical.Where(s => s.ClientId == clientId).ToList();
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

            #region Rottering
            var rotaTypes = await _clientRotaTypeService.Get();
            var rotas = await _clientRotaNameService.Get();
            var rotaTasks = await _rotaTaskService.Get();
            var weekDays = await _rotaDayOfWeekService.Get();

            var clientRotas = await _clientRotaService.GetForEdit(clientId);

            model.Rotas = rotas.Select(r => new SelectListItem { Text = r.RotaName, Value = r.RotaId.ToString() }).ToList();
            model.RotaTasks = rotaTasks.Select(r => new SelectListItem { Text = r.TaskName, Value = r.RotaTaskId.ToString() }).ToList();
            model.RotaTypes = rotaTypes;
            model.WeekDays = weekDays;
            model.ClientRotas = clientRotas;
            #endregion

            #region Best Interest

            var bestInterest = await _bestInterestAssessment.Get(clientId);
            if (bestInterest.IsSuccessStatusCode && bestInterest.StatusCode == System.Net.HttpStatusCode.OK)
            {
                GetBestInterestAssessment mcaBest = bestInterest.Content as GetBestInterestAssessment;
                if (mcaBest != null)
                {

                    model.careIssuebaseRecordList = baseRecord.Where(s => s.KeyName == "MCA_Care_Issues").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
                    model.getBestInterestAssessment = mcaBest;
                    model.baseRecordBestInterestList = baseRecord.ToList();
                    model.BestHeadingList = baseRecord.Where(s => s.KeyName == "Health_Task_Heading").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
                    model.Heading2List = baseRecord.Where(s => s.KeyName == "Health_Task_Heading2").Select(s => s.BaseRecordItems).FirstOrDefault().ToList();
                    model.BaseRecordList = baseRecord.ToList();
                    model.BelieveTaskCount = mcaBest.GetBelieveTask.Count;
                    model.CareIssuesTaskCount = model.baseRecordList.Count;
                    model.Date = mcaBest.Date;
                    model.BestId = mcaBest.BestId;
                    model.Name = mcaBest.Name;
                    model.Position = mcaBest.Position;
                    model.Signature = mcaBest.Signature;
                    model.GetBelieveTask = (from t in mcaBest.GetBelieveTask
                                            select new GetBelieveTask
                                            {
                                                BelieveTaskId = t.BelieveTaskId,
                                                BestId = t.BestId,
                                                ReasonableBelieve = t.ReasonableBelieve,
                                            }).ToList();
                    model.GetCareIssuesTask = (from t in mcaBest.GetCareIssuesTask
                                               select new GetCareIssuesTask
                                               {
                                                   CareIssuesTaskId = t.CareIssuesTaskId,
                                                   BestId = t.BestId,
                                                   Issues = t.Issues,
                                               }).ToList();
                    model.GetHealthTask = (from t in mcaBest.GetHealthTask
                                           select new GetHealthTask
                                           {
                                               HealthTaskId = t.HealthTaskId,
                                               BestId = t.BestId,
                                               HeadingId = t.HeadingId,
                                               Title = t.Title,
                                               Answer = t.Answer,
                                               Remarks = t.Remarks
                                           }).ToList();
                    model.GetHealthTask2 = (from t in mcaBest.GetHealthTask2
                                            select new GetHealthTask2
                                            {
                                                HealthTask2Id = t.HealthTask2Id,
                                                BestId = t.BestId,
                                                Heading2Id = t.Heading2Id,
                                                Title = t.Title,
                                                Answer = t.Answer,
                                                Remark = t.Remark
                                            }).ToList();
                }
            }

            #endregion


            model.GetProgram = program.Where(s => s.ClientId == clientId).ToList();
            model.GetServiceWatch = swatch.Where(s => s.ClientId == clientId).ToList();
            model.GetClientDailyTask = daily.Where(s => s.ClientId == clientId).ToList();
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
