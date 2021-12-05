using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientComplainRegister;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.ClientMedication;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using AwesomeCare.DataTransferObject.DTOs.ClientLogAudit;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationAudit;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AwesomeCare.DataTransferObject.DTOs.ClientVoice;
using AwesomeCare.DataTransferObject.DTOs.ClientMgtVisit;
using AwesomeCare.DataTransferObject.DTOs.ClientProgram;
using AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review;
using AwesomeCare.DataTransferObject.DTOs.ClientBloodCoagulationRecord;
using AwesomeCare.DataTransferObject.DTOs.ClientBMIChart;
using AwesomeCare.DataTransferObject.DTOs.ClientBloodPressure;
using AwesomeCare.DataTransferObject.DTOs.ClientBodyTemp;
using AwesomeCare.DataTransferObject.DTOs.ClientBowelMovement;
using AwesomeCare.DataTransferObject.DTOs.ClientEyeHealthMonitoring;
using AwesomeCare.DataTransferObject.DTOs.ClientFoodIntake;
using AwesomeCare.DataTransferObject.DTOs.ClientOxygenLvl;
using AwesomeCare.DataTransferObject.DTOs.ClientHeartRate;
using AwesomeCare.DataTransferObject.DTOs.ClientPainChart;
using AwesomeCare.DataTransferObject.DTOs.ClientPulseRate;
using AwesomeCare.DataTransferObject.DTOs.ClientSeizure;
using AwesomeCare.DataTransferObject.DTOs.ClientWoundCare;
using AwesomeCare.DataTransferObject.DTOs.Pets;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.PersonalHygiene;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.InfectionControl;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.ManagingTasks;
using AwesomeCare.DataTransferObject.DTOs.CarePlanNutrition;
using AwesomeCare.DataTransferObject.DTOs.Health.Balance;
using AwesomeCare.DataTransferObject.DTOs.Health.PhysicalAbility;
using AwesomeCare.DataTransferObject.DTOs.Health.HealthAndLiving;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthAndMedication;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthCondition;
using AwesomeCare.DataTransferObject.DTOs.Health.HistoryOfFall;
using AwesomeCare.DataTransferObject.DTOs.HospitalEntry;
using AwesomeCare.DataTransferObject.DTOs.HospitalExit;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHomeRiskAssessment;
using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
using AwesomeCare.DataTransferObject.DTOs.ClientDailyTask;

namespace AwesomeCare.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private IGenericRepository<BaseRecordItemModel> _baseRecordItemRepository;
        private IGenericRepository<BaseRecordModel> _baseRecordRepository;
        private IGenericRepository<ClientMedication> _clientMedicationRepository;
        private IGenericRepository<RotaDayofWeek> _rotaDayOfWeekRepository;
        private IGenericRepository<ClientRotaType> _clientRotaTypeRepository;
        private IGenericRepository<Medication> _medicationRepository;
        private IGenericRepository<MedicationManufacturer> _medicationManufacturerRepository;


        private AwesomeCareDbContext _dbContext;
        public ClientController(AwesomeCareDbContext dbContext, IGenericRepository<Client> clientRepository, IGenericRepository<ClientMedication> clientMedicationRepository,
            IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, IGenericRepository<BaseRecordModel> baseRecordRepository, IGenericRepository<RotaDayofWeek> rotaDayOfWeekRepository, 
            IGenericRepository<ClientRotaType> clientRotaTypeRepository, IGenericRepository<Medication> medicationRepository, IGenericRepository<MedicationManufacturer> medicationManufacturerRepository)
        {
            _clientRepository = clientRepository;
            _baseRecordItemRepository = baseRecordItemRepository;
            _baseRecordRepository = baseRecordRepository;
            _dbContext = dbContext;
            _clientMedicationRepository = clientMedicationRepository;
            _rotaDayOfWeekRepository = rotaDayOfWeekRepository;
            _clientRotaTypeRepository = clientRotaTypeRepository;
            _medicationRepository = medicationRepository;
            _medicationManufacturerRepository = medicationManufacturerRepository;
        }
        /// <summary>
        /// Create Client
        /// </summary>
        /// <param name="postClient"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostClient([FromBody] PostClient postClient)
        {

            if (postClient == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Check if client is not already registered

            var isClientRegistered = _clientRepository.Table.Any(c => c.Email.Trim().ToLower().Equals(postClient.Email.Trim().ToLower()));
            if (isClientRegistered)
            {
                ModelState.AddModelError("", $"Client with email address {postClient.Email} is already registered");
                return BadRequest(ModelState);
            }

            var client = Mapper.Map<Client>(postClient);
            var newClient = await _clientRepository.InsertEntity(client);

            newClient.UniqueId = $"AHS/CT/{DateTime.Now.ToString("yy")}/{ newClient.ClientId.ToString("D6")}";
            newClient = await _clientRepository.UpdateEntity(newClient);

            var getClient = Mapper.Map<GetClient>(newClient);
            return CreatedAtAction("GetClient", new { id = getClient.ClientId }, getClient);


        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetClient/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   join baseRecItem in _baseRecordItemRepository.Table on client.StatusId equals baseRecItem.BaseRecordItemId
                                   join baseRec in _baseRecordRepository.Table on baseRecItem.BaseRecordId equals baseRec.BaseRecordId
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       Firstname = client.Firstname,
                                       Middlename = client.Middlename,
                                       Surname = client.Surname,
                                       Email = client.Email,
                                       About = client.About,
                                       Hobbies = client.Hobbies,
                                       StartDate = client.StartDate,
                                       EndDate = client.EndDate,
                                       Keyworker = client.Keyworker,
                                       IdNumber = client.IdNumber,
                                       GenderId = client.GenderId,
                                       NumberOfCalls = client.NumberOfCalls,
                                       AreaCodeId = client.AreaCodeId,
                                       TeritoryId = client.TeritoryId,
                                       ServiceId = client.ServiceId,
                                       ProvisionVenue = client.ProvisionVenue,
                                       PostCode = client.PostCode,
                                       Rate = client.Rate,
                                       TeamLeader = client.TeamLeader,
                                       DateOfBirth = client.DateOfBirth,
                                       Telephone = client.Telephone,
                                       LanguageId = client.LanguageId,
                                       KeySafe = client.KeySafe,
                                       ChoiceOfStaffId = client.ChoiceOfStaffId,
                                       StatusId = client.StatusId,
                                       Status = baseRecItem.ValueName,
                                       CapacityId = client.CapacityId,
                                       ProviderReference = client.ProviderReference,
                                       NumberOfStaff = client.NumberOfStaff,
                                       UniqueId = client.UniqueId,
                                       PassportFilePath = client.PassportFilePath,
                                       Latitude = client.Latitude,
                                       Longitude = client.Longitude,
                                       Address = client.Address,
                                       PreferredName = client.PreferredName,
                                       InvolvingParties = (from inv in client.InvolvingParties
                                                           select new GetClientInvolvingPartyForEdit
                                                           {
                                                               Address = inv.Address,
                                                               ClientId = inv.ClientId,
                                                               ClientInvolvingPartyId = inv.ClientInvolvingPartyId,
                                                               ClientInvolvingPartyItemId = inv.ClientInvolvingPartyItemId,
                                                               Email = inv.Email,
                                                               Name = inv.Name,
                                                               Relationship = inv.Relationship,
                                                               Telephone = inv.Telephone
                                                           }).ToList(),
                                       GetClientComplain = (from com in client.ComplainRegister
                                                            select new GetClientComplainRegister
                                                           {
                                                                COMPLAINANTCONTACT = com.COMPLAINANTCONTACT,
                                                                EvidenceFilePath = com.EvidenceFilePath
                                                           }).ToList(),
                                       GetClientLogAudit = (from log in client.ClientLogAudit
                                                            select new GetClientLogAudit
                                                            {
                                                                ActionRecommended = log.ActionRecommended,
                                                                EvidenceOfActionTaken = log.EvidenceOfActionTaken
                                                            }).ToList(),
                                       GetClientMedAudit = (from med in client.ClientMedAudit
                                                            select new GetClientMedAudit
                                                            {
                                                                ActionRecommended = med.ActionRecommended,
                                                                EvidenceOfActionTaken = med.EvidenceOfActionTaken
                                                            }).ToList(),
                                       GetClientVoice =     (from v in client.ClientVoice
                                                            select new GetClientVoice
                                                            {
                                                                ActionRequired = v.ActionRequired,
                                                                Attachment = v.Attachment
                                                            }).ToList(),
                                       GetClientMgtVisit = (from mg in client.ClientMgtVisit
                                                         select new GetClientMgtVisit
                                                         {
                                                             ActionRequired = mg.ActionRequired,
                                                             Attachment = mg.Attachment
                                                         }).ToList(),
                                       GetClientProgram = (from cp in client.ClientProgram
                                                         select new GetClientProgram
                                                         {
                                                             ActionRequired = cp.ActionRequired,
                                                             Attachment = cp.Attachment
                                                         }).ToList(),
                                       GetClientServiceWatch = (from sw in client.ClientServiceWatch
                                                         select new GetClientServiceWatch
                                                         {
                                                             ActionRequired = sw.ActionRequired,
                                                             Attachment = sw.Attachment
                                                         }).ToList(),
                                       RegulatoryContact = (from reg in client.RegulatoryContact
                                                            join baseRecordItem in _baseRecordItemRepository.Table on reg.BaseRecordItemId equals baseRecordItem.BaseRecordItemId
                                                            join baseRecord in _baseRecordRepository.Table on baseRecordItem.BaseRecordId equals baseRecord.BaseRecordId
                                                            select new GetClientRegulatoryContactForEdit
                                                            {
                                                                ClientId = reg.ClientId,
                                                                ClientRegulatoryContactId = reg.ClientRegulatoryContactId,
                                                                BaseRecordItemId = reg.BaseRecordItemId,
                                                                DatePerformed = reg.DatePerformed,
                                                                DueDate = reg.DueDate,
                                                                Evidence = reg.Evidence,
                                                                RegulatoryContact = baseRecordItem.ValueName
                                                            }).ToList(),
                                       GetClientBloodCoagulationRecord = (from coag in client.ClientBloodCoagulationRecord
                                                                select new GetClientBloodCoagulationRecord
                                                                {
                                                                    Date = coag.Date,
                                                                    Comment = coag.Comment
                                                                }).ToList(),
                                       GetClientBloodPressure= (from pres in client.ClientBloodPressure
                                                                          select new GetClientBloodPressure
                                                                          {
                                                                              Date = pres.Date,
                                                                              Comment = pres.Comment
                                                                          }).ToList(),
                                       GetClientBMIChart= (from bmi in client.ClientBMIChart
                                                                 select new GetClientBMIChart
                                                                 {
                                                                     Date = bmi.Date,
                                                                     Comment = bmi.Comment
                                                                 }).ToList(),
                                       GetClientBodyTemp = (from temp in client.ClientBodyTemp
                                                            select new GetClientBodyTemp
                                                            {
                                                                Date = temp.Date,
                                                                Comment = temp.Comment
                                                            }).ToList(),
                                       GetClientBowelMovement = (from bowel in client.ClientBowelMovement
                                                            select new GetClientBowelMovement
                                                            {
                                                                Date = bowel.Date,
                                                                Comment = bowel.Comment
                                                            }).ToList(),
                                       GetClientEyeHealthMonitoring = (from eye in client.ClientEyeHealthMonitoring
                                                                 select new GetClientEyeHealthMonitoring
                                                                 {
                                                                     Date = eye.Date,
                                                                     Comment = eye.Comment
                                                                 }).ToList(),
                                       GetClientFoodIntake= (from food in client.ClientFoodIntake
                                                                       select new GetClientFoodIntake
                                                                       {
                                                                           Date = food.Date,
                                                                           Comment = food.Comment
                                                                       }).ToList(),
                                       GetClientHeartRate = (from heart in client.ClientHeartRate
                                                              select new GetClientHeartRate
                                                              {
                                                                  Date = heart.Date,
                                                                  Comment = heart.Comment
                                                              }).ToList(),
                                       GetClientOxygenLvl = (from lvl in client.ClientOxygenLvl
                                                             select new GetClientOxygenLvl
                                                             {
                                                                 Date = lvl.Date,
                                                                 Comment = lvl.Comment
                                                             }).ToList(),
                                       GetClientPainChart = (from pain in client.ClientPainChart
                                                             select new GetClientPainChart
                                                             {
                                                                 Date = pain.Date,
                                                                 Comment = pain.Comment
                                                             }).ToList(),
                                       GetClientPulseRate = (from pulse in client.ClientPulseRate
                                                             select new GetClientPulseRate
                                                             {
                                                                 Date = pulse.Date,
                                                                 Comment = pulse.Comment
                                                             }).ToList(),
                                       GetClientSeizure = (from seiz in client.ClientSeizure
                                                             select new GetClientSeizure
                                                             {
                                                                 Date = seiz.Date,
                                                                 Remarks = seiz.Remarks
                                                             }).ToList(),
                                       GetClientWoundCare = (from wc in client.ClientWoundCare
                                                           select new GetClientWoundCare
                                                           {
                                                               Date = wc.Date,
                                                               Comment = wc.Comment
                                                           }).ToList(),
                                       //GetReview = (from rew in client.PersonalDetail
                                       //             select new GetReview
                                       //             {
                                       //                 CP_PreDate = rew.Review.CP_PreDate,
                                       //                 CP_ReviewDate = rew.Review.CP_ReviewDate
                                       //             }).ToList(),
                                       //GetPets = (from pet in client.Pets
                                       //             select new GetPets
                                       //             {
                                       //                 PetsId = pet.PetsId,
                                       //                 Age = pet.Age,
                                       //                 ClientId = pet.ClientId,
                                       //                 Gender = pet.Gender,
                                       //                 Name = pet.Name,
                                       //                 MealPattern = pet.MealPattern,
                                       //                 MealStorage = pet.MealStorage,
                                       //                 PetCare = pet.PetCare,
                                       //                 PetInsurance = pet.PetInsurance,
                                       //                 Type = pet.Type,
                                       //                 PetActivities = pet.PetActivities,
                                       //                 VetVisit = pet.VetVisit
                                       //             }).ToList(),
                                       //GetInterestAndObjective = (from iao in client.InterestAndObjective
                                       //           select new GetInterestAndObjective
                                       //           {
                                       //               CareGoal = iao.CareGoal,
                                       //           }).ToList(),
                                       //GetPersonalHygiene = (from ph in client.PersonalHygiene
                                       //           select new GetPersonalHygiene
                                       //           {
                                       //               LaundrySupport = ph.LaundrySupport,
                                       //               LaundryGuide = ph.LaundryGuide
                                       //           }).ToList(),
                                       //GetInfectionControl = (from ic in client.InfectionControl
                                       //           select new GetInfectionControl
                                       //           {
                                       //               TestDate = ic.TestDate,
                                       //               Remarks = ic.Remarks
                                       //           }).ToList(),
                                       //GetManagingTasks = (from mt in client.ManagingTasks
                                       //           select new GetManagingTasks
                                       //           {
                                       //               Help = mt.Help,
                                       //           }).ToList(),
                                       //GetCarePlanNutrition = (from cpn in client.CarePlanNutrition
                                       //           select new GetCarePlanNutrition
                                       //           {
                                       //               SpecialDiet = cpn.SpecialDiet,
                                       //               AvoidFood = cpn.AvoidFood
                                       //           }).ToList(),
                                       //GetBalance = (from bln in client.Balance
                                       //           select new GetBalance
                                       //           {
                                       //               Name = bln.Name,
                                       //               Description = bln.Description
                                       //           }).ToList(),
                                       //GetPhysicalAbility = (from pab in client.PhysicalAbility
                                       //           select new GetPhysicalAbility
                                       //           {
                                       //               Name = pab.Name,
                                       //               Description = pab.Description
                                       //           }).ToList(),
                                       //GetHealthAndLiving = (from hal in client.HealthAndLiving
                                       //           select new GetHealthAndLiving
                                       //           {
                                       //               BriefHealth = hal.BriefHealth,
                                       //               WakeUp = hal.WakeUp
                                       //           }).ToList(),
                                       //GetSpecialHealthAndMedication = (from sham in client.SpecialHealthAndMedication
                                       //           select new GetSpecialHealthAndMedication
                                       //           {
                                       //               Date = sham.Date,
                                       //               By = sham.By,
                                       //               AccessMedication = sham.AccessMedication,
                                       //               AdminLvl = sham.AdminLvl,
                                       //               ClientId = sham.ClientId,
                                       //               Consent = sham.Consent,
                                       //               FamilyMeds = sham.FamilyMeds,
                                       //               FamilyReturnMed = sham.FamilyReturnMed,
                                       //               LeftoutMedicine = sham.LeftoutMedicine,
                                       //               MedAccessDenial = sham.MedAccessDenial,
                                       //               MedicationAllergy = sham.MedicationAllergy,
                                       //               MedicationStorage = sham.MedicationStorage,
                                       //               MedKeyCode = sham.MedKeyCode,
                                       //               MedsGPOrder = sham.MedsGPOrder,
                                       //               NameFormMedicaiton = sham.NameFormMedicaiton,
                                       //               NoMedAccess = sham.NoMedAccess,
                                       //               OverdoseContact = sham.OverdoseContact,
                                       //               PharmaMARChart = sham.PharmaMARChart,
                                       //               PNRDoses = sham.PNRDoses,
                                       //               PNRMedList = sham.PNRMedList,
                                       //               PNRMedReq = sham.PNRMedReq,
                                       //               PNRMedsAdmin = sham.PNRMedsAdmin,
                                       //               PNRMedsMissing = sham.PNRMedsMissing,
                                       //               SHMId = sham.SHMId,
                                       //               SpecialStorage = sham.SpecialStorage,
                                       //               TempMARChart = sham.TempMARChart,
                                       //               Type = sham.Type,
                                       //               WhoAdminister = sham.WhoAdminister
                                       //           }).ToList(),
                                       //GetSpecialHealthCondition = (from shc in client.SpecialHealthCondition
                                       //           select new GetSpecialHealthCondition
                                       //           {
                                       //               ConditionName = shc.ConditionName,
                                       //               SourceInformation = shc.SourceInformation,
                                       //               ClientAction = shc.ClientAction,
                                       //               ClientId = shc.ClientId,
                                       //               ClinicRecommendation = shc.ClinicRecommendation,
                                       //               FeelingAfterIncident = shc.FeelingAfterIncident,
                                       //               FeelingBeforeIncident = shc.FeelingBeforeIncident,
                                       //               Frequency = shc.Frequency,
                                       //               HealthCondId = shc.HealthCondId,
                                       //               LifestyleSupport = shc.LifestyleSupport,
                                       //               LivingActivities = shc.LivingActivities,
                                       //               PlanningHealthCondition = shc.PlanningHealthCondition,
                                       //               Trigger = shc.Trigger
                                       //           }).ToList(),
                                       //GetHistoryOfFall = (from hof in client.HistoryOfFall
                                       //           select new GetHistoryOfFall
                                       //           {
                                       //               Date = hof.Date,
                                       //               Cause = hof.Cause,
                                       //               Details = hof.Details,
                                       //               HistoryId = hof.HistoryId,
                                       //               Prevention = hof.Prevention,
                                       //               ClientId = hof.ClientId
                                       //           }).ToList(),
                                       GetHospitalEntry = (from hen in client.HospitalEntry
                                                           select new GetHospitalEntry
                                                           {
                                                               HospitalEntryId = hen.HospitalEntryId,
                                                               ClientId = hen.ClientId,
                                                               Date = hen.Date,
                                                               Reference = hen.Reference,
                                                               Attachment = hen.Attachment,
                                                           }).ToList(),
                                       GetHospitalExit = (from hex in client.HospitalExit
                                                          select new GetHospitalExit
                                                          {
                                                              Date = hex.Date,
                                                              Reference = hex.Reference
                                                          }).ToList(),
                                       //GetHomeRiskAssessment = (from hra in client.HomeRiskAssessment
                                       //                         select new GetHomeRiskAssessment
                                       //                         {
                                       //                             Heading = hra.Heading
                                       //                         }).ToList(),
                                       
                                       GetDutyOnCall = (from doc in client.DutyOnCall
                                                        select new GetDutyOnCall
                                                        { 
                                                            Attachment = doc.Attachment,
                                                            Subject = doc.Subject,
                                                            RefNo = doc.RefNo,
                                                            DateOfCall = doc.DateOfCall
                                                        
                                                        }).ToList(),
                                       GetClientDailyTask = (from cdt in  client.ClientDailyTask
                                                             select new GetClientDailyTask
                                                             {
                                                                Date = cdt.Date,
                                                                AmendmentDate = cdt.AmendmentDate
                                                             }).ToList()
                                       
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }

        [HttpGet]
        [ProducesResponseType(type: typeof(List<GetClient>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClients()
        {

            var getClient = await (from client in _clientRepository.Table
                                   join baseRecItem in _baseRecordItemRepository.Table on client.StatusId equals baseRecItem.BaseRecordItemId
                                   join baseRec in _baseRecordRepository.Table on baseRecItem.BaseRecordId equals baseRec.BaseRecordId
                                   // where baseRec.KeyName == "Client_Status"
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       Firstname = client.Firstname,
                                       Middlename = client.Middlename,
                                       Surname = client.Surname,
                                       Email = client.Email,
                                       About = client.About,
                                       Hobbies = client.Hobbies,
                                       StartDate = client.StartDate,
                                       EndDate = client.EndDate,
                                       Keyworker = client.Keyworker,
                                       IdNumber = client.IdNumber,
                                       GenderId = client.GenderId,
                                       NumberOfCalls = client.NumberOfCalls,
                                       AreaCodeId = client.AreaCodeId,
                                       TeritoryId = client.TeritoryId,
                                       ServiceId = client.ServiceId,
                                       ProvisionVenue = client.ProvisionVenue,
                                       PostCode = client.PostCode,
                                       Rate = client.Rate,
                                       TeamLeader = client.TeamLeader,
                                       DateOfBirth = client.DateOfBirth,
                                       Telephone = client.Telephone,
                                       LanguageId = client.LanguageId,
                                       KeySafe = client.KeySafe,
                                       ChoiceOfStaffId = client.ChoiceOfStaffId,
                                       StatusId = client.StatusId,
                                       Status = baseRecItem.ValueName,
                                       CapacityId = client.CapacityId,
                                       ProviderReference = client.ProviderReference,
                                       NumberOfStaff = client.NumberOfStaff,
                                       UniqueId = client.UniqueId,
                                       PassportFilePath = client.PassportFilePath,
                                       Longitude = client.Longitude,
                                       Address = client.Address,
                                       Latitude = client.Latitude
                                   }
                      ).ToListAsync();

            return Ok(getClient);
        }

        /// <summary>
        /// Get Client with few properties
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetClientDetails")]
        [ProducesResponseType(type: typeof(List<GetClientDetail>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClientDetails()
        {
            //var kk = (from client in _clientRepository.Table
            //          join baseRecItem in _baseRecordItemRepository.Table on client.StatusId equals baseRecItem.BaseRecordItemId
            //          join baseRec in _baseRecordRepository.Table on baseRecItem.BaseRecordId equals baseRec.BaseRecordId
            //          where client.ClientId == id.Value
            //          select client).pro


            var clientDetails = await _clientRepository.Table.ProjectTo<GetClientDetail>().ToListAsync();
            return Ok(clientDetails);
        }


        [HttpGet("{clientId}")]
        [ProducesResponseType(type: typeof(List<GetClient>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditClient(int? clientId)
        {
            if (!clientId.HasValue)
                return BadRequest("id Parameter is required");
            var getClient = await _clientRepository.Table.Where(c => c.ClientId == clientId).ProjectTo<GetClientForEdit>().FirstOrDefaultAsync();//.Include(c => c.RegulatoryContact).Include(i => i.InvolvingParties).FirstOrDefaultAsync();

            return Ok(getClient);
        }

        [HttpPut("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutClient([FromBody] PutClient model, int? clientId)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = Mapper.Map<Client>(model);
            client.ClientId = clientId.Value;
            _dbContext.Attach(client);
            var properties = model.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                _dbContext.Entry(client).Property(prop.Name).IsModified = true;
            }
            var id = await _dbContext.SaveChangesAsync();
            return Ok(id);
        }

        #region Medication
        /// <summary>
        /// Create Client Medication
        /// </summary>
        /// <returns></returns>
        [HttpPost("Medication")]
        [ProducesResponseType(type: typeof(GetClientMedication), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostMedication([FromBody] PostClientMedication model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }
                var entity = Mapper.Map<ClientMedication>(model);
                var newEntity = await _clientMedicationRepository.InsertEntity(entity);
                var getEntity = Mapper.Map<GetClientMedication>(newEntity);
                return CreatedAtAction("GetMedication", new { id = newEntity.ClientMedicationId, clientId = newEntity.ClientId }, getEntity);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        ///  Get Client Medication By clientId and Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("Medication/{clientId}/{id}", Name = "GetClientMedication")]
        [ProducesResponseType(type: typeof(GetClientMedication), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMedication(int? id, int? clientId)
        {
            if (!id.HasValue || !clientId.HasValue)
            {
                return BadRequest("All Parameters are required");
            }

            var entity = await (from clmd in _clientMedicationRepository.Table
                                join med in _medicationRepository.Table on clmd.MedicationId equals med.MedicationId
                                join medMf in _medicationManufacturerRepository.Table on clmd.MedicationManufacturerId equals medMf.MedicationManufacturerId
                                where clmd.ClientMedicationId == id && clmd.ClientId == clientId
                                select new GetClientMedication
                                {
                                    ClientMedicationId = clmd.ClientMedicationId,
                                    ClientMedicationDay = (from dy in clmd.ClientMedicationDay
                                                           join rtwk in _rotaDayOfWeekRepository.Table on dy.RotaDayofWeekId equals rtwk.RotaDayofWeekId
                                                           select new GetClientMedicationDay
                                                           {
                                                               ClientMedicationDayId = dy.ClientMedicationDayId,
                                                               ClientMedicationId = dy.ClientMedicationId,
                                                               DayOfWeek = rtwk.DayofWeek,
                                                               RotaDayofWeekId = dy.RotaDayofWeekId,
                                                               ClientMedicationPeriod = (from pd in dy.ClientMedicationPeriod
                                                                                         join cltp in _clientRotaTypeRepository.Table on pd.ClientRotaTypeId equals cltp.ClientRotaTypeId
                                                                                         select new GetClientMedicationPeriod
                                                                                         {
                                                                                             ClientRotaTypeId = cltp.ClientRotaTypeId,
                                                                                             ClientMedicationDayId = pd.ClientMedicationDayId,
                                                                                             ClientMedicationPeriodId = pd.ClientMedicationPeriodId,
                                                                                             RotaType = cltp.RotaType
                                                                                         }).ToList(),
                                                           }).ToList(),
                                    Dossage = clmd.Dossage,
                                    ExpiryDate = clmd.ExpiryDate,
                                    Frequency = clmd.Frequency,
                                    Gap_Hour = clmd.Gap_Hour,
                                    MedicationId = clmd.MedicationId,
                                    MedicationManufacturerId = clmd.MedicationManufacturerId,
                                    ClientId = clmd.ClientId,
                                    Medication = med.MedicationName,
                                    MedicationManufacturer = medMf.Manufacturer,
                                    Remark = clmd.Remark,
                                    Route = clmd.Route,
                                    StartDate = clmd.StartDate,
                                    Status = clmd.Status,
                                    StopDate = clmd.StopDate
                                }).FirstOrDefaultAsync();

            return Ok(entity);
        }

        /// <summary>
        /// Get Client Medications by ClientId
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("Medication/{clientId}", Name = "GetClientMedications")]
        [ProducesResponseType(type: typeof(List<GetClientMedication>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMedication(int? clientId)
        {
            if (!clientId.HasValue)
            {
                return BadRequest("All Parameters are required");
            }
            var entity = await (from clmd in _clientMedicationRepository.Table
                                join med in _medicationRepository.Table on clmd.MedicationId equals med.MedicationId
                                join medMf in _medicationManufacturerRepository.Table on clmd.MedicationManufacturerId equals medMf.MedicationManufacturerId
                                where clmd.ClientId == clientId
                                select new GetClientMedication
                                {
                                    ClientMedicationId = clmd.ClientMedicationId,
                                    ClientMedicationDay = (from dy in clmd.ClientMedicationDay
                                                           join rtwk in _rotaDayOfWeekRepository.Table on dy.RotaDayofWeekId equals rtwk.RotaDayofWeekId
                                                           select new GetClientMedicationDay
                                                           {
                                                               ClientMedicationDayId = dy.ClientMedicationDayId,
                                                               ClientMedicationId = dy.ClientMedicationId,
                                                               DayOfWeek = rtwk.DayofWeek,
                                                               RotaDayofWeekId = dy.RotaDayofWeekId,
                                                               ClientMedicationPeriod = (from pd in dy.ClientMedicationPeriod
                                                                                         join cltp in _clientRotaTypeRepository.Table on pd.ClientRotaTypeId equals cltp.ClientRotaTypeId
                                                                                         select new GetClientMedicationPeriod
                                                                                         {
                                                                                             ClientRotaTypeId = cltp.ClientRotaTypeId,
                                                                                             ClientMedicationDayId = pd.ClientMedicationDayId,
                                                                                             ClientMedicationPeriodId = pd.ClientMedicationPeriodId,
                                                                                             RotaType = cltp.RotaType
                                                                                         }).ToList(),
                                                           }).ToList(),
                                    Dossage = clmd.Dossage,
                                    ExpiryDate = clmd.ExpiryDate,
                                    Frequency = clmd.Frequency,
                                    Gap_Hour = clmd.Gap_Hour,
                                    MedicationId = clmd.MedicationId,
                                    MedicationManufacturerId = clmd.MedicationManufacturerId,
                                    ClientId = clmd.ClientId,
                                    Medication = med.MedicationName,
                                    MedicationManufacturer = medMf.Manufacturer,
                                    Remark = clmd.Remark,
                                    Route = clmd.Route,
                                    StartDate = clmd.StartDate,
                                    Status = clmd.Status,
                                    StopDate = clmd.StopDate
                                }).ToListAsync();

            return Ok(entity);
        }

        [HttpPost("GeoLocation/{clientId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutGeolocation(int clientId, [FromBody] PutGeolocation putGeolocation)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var client = await _clientRepository.Table.FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (client == null) return BadRequest();

            client.Latitude = putGeolocation.Latitude;
            client.Longitude = putGeolocation.Longitude;

            await _clientRepository.UpdateEntity(client);

            return Ok();
        }

        #endregion

    }
}