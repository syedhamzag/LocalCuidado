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
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
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

        public CarePlanController(  IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IPersonalDetailService pdetailService,
                                    IBaseRecordService baseRecord, IClientInvolvingParty involvingparty, ICarePlanNutritionService nutritionService,
                                    IPersonalHygieneService phygieneService, IInfectionControlService infectionService, IManagingTasksService mataskService,
                                    IPetsService petsService, IInterestAndObjectiveService interestService, IBalanceService balanceService, IPhysicalAbilityService physicalService,
                                    IHistoryOfFallService historyService, IHealthAndLivingService hlService, ISpecialHealthAndMedicationService shamService,
                                    ISpecialHealthConditionService shcService) : base(fileUpload)
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
        }

        public async Task<IActionResult> CareView(int clientId)
        {
            var bases = await _baseRecord.GetBaseRecord();
            var baseClass = bases.Where(s => s.KeyName == "Class").FirstOrDefault().BaseRecordId;
            var classItems = await _baseRecord.GetBaseRecordWithItems(baseClass);

            var staff = _staffService.GetStaffs();
            var involve = _clientService.GetClient(clientId);
            var client = _clientService.GetClient(clientId);
            var details = _clientService.GetClientDetail();
            var party = _involvingparty.Get(clientId);
            var Relation = "N/A";
            if (party.Result != null)
            {
                Relation = party.Result.Relationship;
            }

            var pdetail = _pdetailService.Get(clientId);
            var nutrition = await _nutritionService.Get();
            var infection = await _infectionService.Get();
            var mtask = _mataskService.Get();
            var hygiene = await _phygieneService.Get();
            var obj = _interestService.Get(clientId);
            var pets = await _petsService.Get();
            var balance = await _balanceService.Get();
            var physical = await _physicalService.Get();
            var hl = await _hlService.Get();
            var history = await _historyService.Get();
            var sham = await _shamService.Get();
            var shc = await _shcService.Get();

            var model = new CreateCarePlanView()
            {
                #region PERSONAL DETAILS

                #region Personal Details
                ClientId = clientId,
                PersonalDetailId = pdetail.Result.PersonalDetailId,
                #endregion

                #region Capacity
                CapacityId = pdetail.Result.Capacity.FirstOrDefault().CapacityId,
                Implications = pdetail.Result.Capacity.FirstOrDefault().Implications,
                Pointer = pdetail.Result.Capacity.FirstOrDefault().Pointer,
                IndicatorList = pdetail.Result.Capacity.FirstOrDefault().Indicator.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                Indicator = pdetail.Result.Capacity.FirstOrDefault().Indicator.Select(s => s.BaseRecordId).ToList(),
                #endregion

                #region ConsentCare
                CareId = pdetail.Result.ConsentCare.FirstOrDefault().CareId,
                CareSignature = pdetail.Result.ConsentCare.FirstOrDefault().Signature,
                CareDate = pdetail.Result.ConsentCare.FirstOrDefault().Date,
                CareName = pdetail.Result.ConsentCare.FirstOrDefault().Name,
                CareRelation = Relation,
                #endregion

                #region ConsentData
                DataId = pdetail.Result.ConsentData.FirstOrDefault().DataId,
                DataSignature = pdetail.Result.ConsentData.FirstOrDefault().Signature,
                DataDate = pdetail.Result.ConsentData.FirstOrDefault().Date,
                DataName = pdetail.Result.ConsentData.FirstOrDefault().Name,
                DataRelation = Relation,
                #endregion

                #region ConsentLandLine
                LandLineId = pdetail.Result.ConsentLandline.FirstOrDefault().LandlineId,
                LandLineSignature = pdetail.Result.ConsentLandline.FirstOrDefault().Signature,
                LandLineDate = pdetail.Result.ConsentLandline.FirstOrDefault().Date,
                LandLogList = pdetail.Result.ConsentLandline.FirstOrDefault().LogMethod.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                LandLineLogMethod = pdetail.Result.ConsentLandline.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList(),
                LandName = pdetail.Result.ConsentLandline.FirstOrDefault().Name,
                LandRelation = Relation,
                #endregion

                #region Equipment
                GetEquipment = pdetail.Result.Equipment,
                StaffList = staff.Result.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList(),
                #endregion

                #region KeyIndicators
                AboutMe = pdetail.Result.KeyIndicators.FirstOrDefault().AboutMe,
                KeyId = pdetail.Result.KeyIndicators.FirstOrDefault().KeyId,
                FamilyRole = pdetail.Result.KeyIndicators.FirstOrDefault().FamilyRole,
                Debture = pdetail.Result.KeyIndicators.FirstOrDefault().Debture,
                LivingStatus = pdetail.Result.KeyIndicators.FirstOrDefault().LivingStatus,
                KeyLogList = pdetail.Result.KeyIndicators.FirstOrDefault().LogMethod.Select(s => new SelectListItem(s.ValueName, s.BaseRecordId.ToString())).ToList(),
                LogMethod = pdetail.Result.KeyIndicators.FirstOrDefault().LogMethod.Select(s => s.BaseRecordId).ToList(),
                ThingsILike = pdetail.Result.KeyIndicators.FirstOrDefault().ThingsILike,
                #endregion

                #region Personal
                PersonalId = pdetail.Result.Personal.FirstOrDefault().PersonalId,
                Smoking = pdetail.Result.Personal.FirstOrDefault().Smoking,
                DNR = pdetail.Result.Personal.FirstOrDefault().DNR,
                FullName = details.Result.Where(s => s.ClientId == clientId).Select(s => s.FullName).FirstOrDefault(),
                PreferredLanguage = client.Result.LanguageId,
                Gender = client.Result.GenderId,
                DateofBirth = client.Result.DateOfBirth,
                Address = client.Result.Address,
                PostCode = client.Result.PostCode,
                Telephone = client.Result.Telephone,
                PreferredName = client.Result.Firstname,
                AccessCode = client.Result.KeySafe,
                PreferredGender = client.Result.ChoiceOfStaffId,
                KeyWorker = client.Result.Keyworker,
                TeamLeader = client.Result.TeamLeader,
                Religion = pdetail.Result.Personal.FirstOrDefault().Religion,
                Nationality = pdetail.Result.Personal.FirstOrDefault().Nationality,
                #endregion

                #region Person Centred
                GetPersonCentred = pdetail.Result.PersonCentred,
                #endregion

                #region Review
                ReviewId = pdetail.Result.Review.FirstOrDefault().ReviewId,
                CP_PreDate = pdetail.Result.Review.FirstOrDefault().CP_PreDate,
                CP_ReviewDate = pdetail.Result.Review.FirstOrDefault().CP_ReviewDate,
                RA_PreDate = pdetail.Result.Review.FirstOrDefault().RA_PreDate,
                RA_ReviewDate = pdetail.Result.Review.FirstOrDefault().RA_ReviewDate,
                #endregion

                InvolingList = involve.Result.InvolvingParties.Select(s => new SelectListItem(s.Name, s.ClientInvolvingPartyId.ToString())).ToList(),
                EquipmentCount = pdetail.Result.Equipment.Count,
                PersonCentreCount = pdetail.Result.PersonCentred.Count,

                #endregion

                #region NUTRITION
                Nutrition_AvoidFood = nutrition.FirstOrDefault().AvoidFood,
                Nutrition_DrinkType = nutrition.FirstOrDefault().DrinkType,
                Nutrition_EatingDifficulty = nutrition.FirstOrDefault().EatingDifficulty,
                Nutrition_FoodIntake = nutrition.FirstOrDefault().FoodIntake,
                Nutrition_FoodStorage = nutrition.FirstOrDefault().FoodStorage,
                Nutrition_MealPreparation = nutrition.FirstOrDefault().MealPreparation,
                NutritionId = nutrition.FirstOrDefault().NutritionId,
                Nutrition_RiskMitigations = nutrition.FirstOrDefault().RiskMitigations,
                Nutrition_ServingMeal = nutrition.FirstOrDefault().ServingMeal,
                Nutrition_SpecialDiet = nutrition.FirstOrDefault().SpecialDiet,
                Nutrition_ThingsILike = nutrition.FirstOrDefault().ThingsILike,
                Nutrition_WhenRestock = nutrition.FirstOrDefault().WhenRestock,
                Nutrition_WhoRestock = nutrition.FirstOrDefault().WhoRestock,
                #endregion

                #region HYGIENE

                #region Infection Control
                Infection_Guideline = infection.FirstOrDefault().Guideline,
                InfectionId = infection.FirstOrDefault().InfectionId,
                Infection_Remarks = infection.FirstOrDefault().Remarks,
                Infection_Status = infection.FirstOrDefault().Status,
                Infection_TestDate = infection.FirstOrDefault().TestDate,
                Infection_Type = infection.FirstOrDefault().Type,
                Infection_VaccStatus = infection.FirstOrDefault().VaccStatus,
                #endregion

                #region Managing Tasks
                GetManagingTasks = mtask.Result,
                #endregion

                #region Personal Hygiene
                HygieneId = hygiene.FirstOrDefault().HygieneId,
                Hygiene_Cleaning = hygiene.FirstOrDefault().Cleaning,
                Hygiene_CleaningFreq = hygiene.FirstOrDefault().CleaningFreq,
                Hygiene_CleaningTools = hygiene.FirstOrDefault().CleaningTools,
                Hygiene_DesiredCleaning = hygiene.FirstOrDefault().DesiredCleaning,
                Hygiene_DirtyLaundry = hygiene.FirstOrDefault().DirtyLaundry,
                Hygiene_DryLaundry = hygiene.FirstOrDefault().DryLaundry,
                Hygiene_GeneralAppliance = hygiene.FirstOrDefault().GeneralAppliance,
                Hygiene_Ironing = hygiene.FirstOrDefault().Ironing,
                Hygiene_LaundryGuide = hygiene.FirstOrDefault().LaundryGuide,
                Hygiene_LaundrySupport = hygiene.FirstOrDefault().LaundrySupport,
                Hygiene_WashingMachine = hygiene.FirstOrDefault().WashingMachine,
                Hygiene_WhoClean = hygiene.FirstOrDefault().WhoClean,
                #endregion

                #endregion

                #region INTEREST AND OBJECTIVE

                #region Interest
                Interest_CareGoal = obj.Result.CareGoal,
                Interest_Brief = obj.Result.Brief,

                #region Lists
                GetInterest = obj.Result.Interest,
                GetPersonalityTest = obj.Result.PersonalityTest,
                #endregion

                #endregion

                #region Pets
                Pet_Age = pets.FirstOrDefault().Age,
                Pet_Type = pets.FirstOrDefault().Type,
                PetsId = pets.FirstOrDefault().PetsId,
                Pet_Name = pets.FirstOrDefault().Name,
                Pet_Gender = pets.FirstOrDefault().Gender,
                PetActivities = pets.FirstOrDefault().PetActivities,
                PetCare = pets.FirstOrDefault().PetCare,
                Pet_MealPattern = pets.FirstOrDefault().MealPattern,
                PetInsurance = pets.FirstOrDefault().PetInsurance,
                Pet_MealStorage = pets.FirstOrDefault().MealStorage,
                Pet_VetVisit = pets.FirstOrDefault().VetVisit,
                #endregion
                #endregion

                #region HEALTH

                #region Balance
                Balance_Description = balance.FirstOrDefault().Description,
                Balance_Mobility = balance.FirstOrDefault().Mobility,
                BalanceId = balance.FirstOrDefault().BalanceId,
                Balance_Name = balance.FirstOrDefault().Name,
                Balance_Status = balance.FirstOrDefault().Status,
                #endregion

                #region Physical Ability
                Physical_Description = physical.FirstOrDefault().Description,
                Physical_Mobility = physical.FirstOrDefault().Mobility,
                PhysicalId = physical.FirstOrDefault().PhysicalId,
                Physical_Name = physical.FirstOrDefault().Name,
                Physical_Status = physical.FirstOrDefault().Status,
                #endregion

                #region Health And Living
                HL_AbilityToRead = hl.FirstOrDefault().AbilityToRead,
                HL_AlcoholicDrink = hl.FirstOrDefault().AlcoholicDrink,
                HL_AllowChats = hl.FirstOrDefault().AllowChats,
                HL_BriefHealth = hl.FirstOrDefault().BriefHealth,
                HL_CareSupport = hl.FirstOrDefault().CareSupport,
                HL_ConstraintAttachment = hl.FirstOrDefault().ConstraintAttachment,
                HL_ConstraintDetails = hl.FirstOrDefault().ConstraintDetails,
                HL_ConstraintRequired = hl.FirstOrDefault().ConstraintRequired,
                HL_ContinenceIssue = hl.FirstOrDefault().ContinenceIssue,
                HL_ContinenceNeeds = hl.FirstOrDefault().ContinenceNeeds,
                HL_ContinenceSource = hl.FirstOrDefault().ContinenceSource,
                HL_DehydrationRisk = hl.FirstOrDefault().DehydrationRisk,
                HL_EatingWithStaff = hl.FirstOrDefault().EatingWithStaff,
                HL_Email = hl.FirstOrDefault().Email,
                HL_FamilyUpdate = hl.FirstOrDefault().FamilyUpdate,
                HL_FinanceManagement = hl.FirstOrDefault().FinanceManagement,
                HLId = hl.FirstOrDefault().HLId,
                HL_LaundaryRequired = hl.FirstOrDefault().LaundaryRequired,
                HL_LetterOpening = hl.FirstOrDefault().LetterOpening,
                HL_LifeStyle = hl.FirstOrDefault().LifeStyle,
                HL_MeansOfComm = hl.FirstOrDefault().MeansOfComm,
                HL_MovingAndHandling = hl.FirstOrDefault().MovingAndHandling,
                HL_NeighbourInvolment = hl.FirstOrDefault().NeighbourInvolment,
                HL_ObserveHealth = hl.FirstOrDefault().ObserveHealth,
                HL_PostalService = hl.FirstOrDefault().PostalService,
                HL_PressureSore = hl.FirstOrDefault().PressureSore,
                HL_ShoppingRequired = hl.FirstOrDefault().ShoppingRequired,
                HL_Smoking = hl.FirstOrDefault().Smoking,
                HL_SpecialCaution = hl.FirstOrDefault().SpecialCaution,
                HL_SpecialCleaning = hl.FirstOrDefault().SpecialCleaning,
                HL_SupportToBed = hl.FirstOrDefault().SupportToBed,
                HL_TeaChocolateCoffee = hl.FirstOrDefault().TeaChocolateCoffee,
                HL_TextFontSize = hl.FirstOrDefault().TextFontSize,
                HL_TVandMusic = hl.FirstOrDefault().TVandMusic,
                HL_VideoCallRequired = hl.FirstOrDefault().VideoCallRequired,
                HL_WakeUp = hl.FirstOrDefault().WakeUp,
                #endregion

                #region SHAM
                SHAM_AccessMedication = sham.FirstOrDefault().AccessMedication,
                SHAM_AdminLvl = sham.FirstOrDefault().AdminLvl,
                SHAM_By = sham.FirstOrDefault().By,
                SHAM_Consent = sham.FirstOrDefault().Consent,
                SHAM_Date = sham.FirstOrDefault().Date,
                SHAM_FamilyMeds = sham.FirstOrDefault().FamilyMeds,
                SHAM_MedKeyCode = sham.FirstOrDefault().MedKeyCode,
                SHAM_MedicationAllergy = sham.FirstOrDefault().MedicationAllergy,
                SHMId = sham.FirstOrDefault().SHMId,
                SHAM_LeftoutMedicine = sham.FirstOrDefault().LeftoutMedicine,
                SHAM_MedAccessDenial = sham.FirstOrDefault().MedAccessDenial,
                SHAM_MedicationStorage = sham.FirstOrDefault().MedicationStorage,
                SHAM_NameFormMedicaiton = sham.FirstOrDefault().NameFormMedicaiton,
                SHAM_PharmaMARChart = sham.FirstOrDefault().PharmaMARChart,
                SHAM_PNRDoses = sham.FirstOrDefault().PNRDoses,
                SHAM_PNRMedsMissing = sham.FirstOrDefault().PNRMedsMissing,
                SHAM_SpecialStorage = sham.FirstOrDefault().SpecialStorage,
                SHAM_NoMedAccess = sham.FirstOrDefault().NoMedAccess,
                SHAM_MedsGPOrder = sham.FirstOrDefault().MedsGPOrder,
                SHAM_WhoAdminister = sham.FirstOrDefault().WhoAdminister,
                SHAM_PNRMedsAdmin = sham.FirstOrDefault().PNRMedsAdmin,
                SHAM_PNRMedList = sham.FirstOrDefault().PNRMedList,
                SHAM_OverdoseContact = sham.FirstOrDefault().OverdoseContact,
                SHAM_TempMARChart = sham.FirstOrDefault().TempMARChart,
                SHAM_FamilyReturnMed = sham.FirstOrDefault().FamilyReturnMed,
                SHAM_PNRMedReq = sham.FirstOrDefault().PNRMedReq,
                SHAM_Type = sham.FirstOrDefault().Type,
                #endregion

                #region SHC
                SHC_ClientAction = shc.FirstOrDefault().ClientAction,
                SHC_ClinicRecommendation = shc.FirstOrDefault().ClinicRecommendation,
                SHC_ConditionName = shc.FirstOrDefault().ConditionName,
                SHC_FeelingAfterIncident = shc.FirstOrDefault().FeelingAfterIncident,
                SHC_LivingActivities = shc.FirstOrDefault().LivingActivities,
                SHC_FeelingBeforeIncident = shc.FirstOrDefault().FeelingBeforeIncident,
                SHC_Frequency = shc.FirstOrDefault().Frequency,
                HealthCondId = shc.FirstOrDefault().HealthCondId,
                SHC_LifestyleSupport = shc.FirstOrDefault().LifestyleSupport,
                SHC_PlanningHealthCondition = shc.FirstOrDefault().PlanningHealthCondition,
                SHC_SourceInformation = shc.FirstOrDefault().SourceInformation,
                SHC_Trigger = shc.FirstOrDefault().Trigger,
                #endregion

                #region History Of Fall
                HistoryId = history.FirstOrDefault().HistoryId,
                History_Cause = history.FirstOrDefault().Cause,
                History_Date = history.FirstOrDefault().Date,
                History_Details = history.FirstOrDefault().Details,
                History_Prevention = history.FirstOrDefault().Prevention,
                #endregion

                #endregion

            };

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
            #endregion

            return View(model);
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
