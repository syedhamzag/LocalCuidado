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
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal;
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
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
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

namespace AwesomeCare.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private IGenericRepository<ClientInvolvingParty> _clientInvolvingPartRepository;
        private IGenericRepository<ClientComplainRegister> _complainRepository;
        private IGenericRepository<BaseRecordItemModel> _baseRecordItemRepository;
        private IGenericRepository<BaseRecordModel> _baseRecordRepository;
        private IGenericRepository<ClientMedication> _clientMedicationRepository;
        private IGenericRepository<ClientMedicationDay> _clientMedicationDayRepository;
        private IGenericRepository<ClientMedicationPeriod> _clientMedicationPeriodRepository;
        private IGenericRepository<RotaDayofWeek> _rotaDayOfWeekRepository;
        private IGenericRepository<ClientRotaType> _clientRotaTypeRepository;
        private IGenericRepository<ClientLogAudit> _clientLogAuditRepository;
        private IGenericRepository<ClientMedAudit> _clientMedAuditRepository;
        private IGenericRepository<ClientVoice> _clientVoiceRepository;
        private IGenericRepository<ClientMgtVisit> _clientMgtVisitRepository;
        private IGenericRepository<ClientProgram> _clientProgramRepository;
        private IGenericRepository<ClientServiceWatch> _clientServiceWatchRepository;
        private IGenericRepository<Medication> _medicationRepository;
        private IGenericRepository<MedicationManufacturer> _medicationManufacturerRepository;
        private IGenericRepository<ClientBloodCoagulationRecord> _bloodcoagRepository;
        private IGenericRepository<ClientBMIChart> _bmichartRepository;
        private IGenericRepository<ClientBloodPressure> _bloodpressureRepository;
        private IGenericRepository<ClientBodyTemp> _bodytempRepository;
        private IGenericRepository<ClientBowelMovement> _bowelmovementRepository;
        private IGenericRepository<ClientEyeHealthMonitoring> _eyehealthRepository;
        private IGenericRepository<ClientFoodIntake> _foodintakeRepository;
        private IGenericRepository<ClientOxygenLvl> _oxygenlvlRepository;
        private IGenericRepository<ClientPulseRate> _pulserateRepository;
        private IGenericRepository<ClientHeartRate> _heartrateRepository;
        private IGenericRepository<ClientSeizure> _seizureRepository;
        private IGenericRepository<ClientWoundCare> _woundcareRepository;
        private IGenericRepository<ClientPainChart> _painchartRepository;
        private IGenericRepository<PersonalDetail> _personalRepository;
        private IGenericRepository<SpecialHealthAndMedication> _spcmedRepository;
        private IGenericRepository<SpecialHealthCondition> _speccondRepository;
        private IGenericRepository<PhysicalAbility> _phyabRepository;
        private IGenericRepository<Balance> _balanceRepository;
        private IGenericRepository<HistoryOfFall> _historyRepository;
        private IGenericRepository<HealthAndLiving> _healthlivingRepository;
        private IGenericRepository<CarePlanNutrition> _cpnutRepository;
        private IGenericRepository<PersonalHygiene> _phygieneRepository;
        private IGenericRepository<InfectionControl> _infectionRepository;
        private IGenericRepository<ManagingTasks> _mtaskRepository;
        private IGenericRepository<InterestAndObjective> _objRepository;
        private IGenericRepository<Pets> _petsRepository;


        private AwesomeCareDbContext _dbContext;
        public ClientController(AwesomeCareDbContext dbContext, IGenericRepository<Client> clientRepository, IGenericRepository<ClientMedicationPeriod> clientMedicationPeriodRepository,
            IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, IGenericRepository<ClientMedicationDay> clientMedicationDayRepository,
            IGenericRepository<BaseRecordModel> baseRecordRepository, IGenericRepository<ClientMedication> clientMedicationRepository, IGenericRepository<ClientComplainRegister> complainRepository,
            IGenericRepository<RotaDayofWeek> rotaDayOfWeekRepository, IGenericRepository<ClientRotaType> clientRotaTypeRepository, IGenericRepository<ClientInvolvingParty> clientInvolvingPartRepository,
            IGenericRepository<Medication> medicationRepository, IGenericRepository<MedicationManufacturer> medicationManufacturerRepository,
            IGenericRepository<ClientLogAudit> clientLogAuditRepository, IGenericRepository<ClientMedAudit> clientMedAuditRepository, IGenericRepository<ClientVoice> clientVoiceRepository,
            IGenericRepository<ClientMgtVisit> clientMgtVisitRepository, IGenericRepository<ClientProgram> clientProgramRepository,IGenericRepository<ClientServiceWatch> clientServiceWatchRepository,
            IGenericRepository<ClientBloodCoagulationRecord> bloodcoagRepository,
            IGenericRepository<ClientBMIChart> bmichartRepository,
            IGenericRepository<ClientBloodPressure> bloodpressureRepository,
            IGenericRepository<ClientBodyTemp> bodytempRepository,
            IGenericRepository<ClientBowelMovement> bowelmovementRepository,
            IGenericRepository<ClientEyeHealthMonitoring> eyehealthRepository,
            IGenericRepository<ClientFoodIntake> foodintakeRepository,
            IGenericRepository<ClientOxygenLvl> oxygenlvlRepository,
            IGenericRepository<ClientPulseRate> pulserateRepository,
            IGenericRepository<ClientHeartRate> heartrateRepository,
            IGenericRepository<ClientSeizure> seizureRepository,
            IGenericRepository<ClientWoundCare> woundcareRepository,
            IGenericRepository<ClientPainChart> painchartRepository,
            IGenericRepository<PersonalDetail> personalRepository,
            IGenericRepository<SpecialHealthAndMedication> spcmedRepository,
            IGenericRepository<SpecialHealthCondition> speccondRepository,
            IGenericRepository<PhysicalAbility> phyabRepository,
            IGenericRepository<Balance> balanceRepository,
            IGenericRepository<HistoryOfFall> historyRepository,
            IGenericRepository<HealthAndLiving> healthlivingRepository,
            IGenericRepository<CarePlanNutrition> cpnutRepository,
            IGenericRepository<PersonalHygiene> phygieneRepository,
            IGenericRepository<InfectionControl> infectionRepository,
            IGenericRepository<ManagingTasks> mtaskRepository,
            IGenericRepository<InterestAndObjective> objRepository,
            IGenericRepository<Pets> petsRepository)
        {
            _clientRepository = clientRepository;
            _complainRepository = complainRepository;
            _baseRecordItemRepository = baseRecordItemRepository;
            _baseRecordRepository = baseRecordRepository;
            _dbContext = dbContext;
            _clientMedicationRepository = clientMedicationRepository;
            _clientMedicationDayRepository = clientMedicationDayRepository;
            _clientMedicationPeriodRepository = clientMedicationPeriodRepository;
            _rotaDayOfWeekRepository = rotaDayOfWeekRepository;
            _clientRotaTypeRepository = clientRotaTypeRepository;
            _medicationRepository = medicationRepository;
            _medicationManufacturerRepository = medicationManufacturerRepository;
            _clientLogAuditRepository = clientLogAuditRepository;
            _clientMedAuditRepository = clientMedAuditRepository;
            _clientVoiceRepository = clientVoiceRepository;
            _clientMgtVisitRepository = clientMgtVisitRepository;
            _clientProgramRepository = clientProgramRepository;
            _clientServiceWatchRepository = clientServiceWatchRepository;
            _clientInvolvingPartRepository = clientInvolvingPartRepository;
            _bloodcoagRepository = bloodcoagRepository;
            _bmichartRepository = bmichartRepository;
            _bloodpressureRepository = bloodpressureRepository;
            _bodytempRepository = bodytempRepository;
            _bowelmovementRepository = bowelmovementRepository;
            _eyehealthRepository = eyehealthRepository;
            _foodintakeRepository = foodintakeRepository;
            _oxygenlvlRepository = oxygenlvlRepository;
            _pulserateRepository = pulserateRepository;
            _heartrateRepository = heartrateRepository;
            _seizureRepository = seizureRepository;
            _woundcareRepository = woundcareRepository;
            _painchartRepository = painchartRepository;
            _personalRepository = personalRepository;
            _spcmedRepository = spcmedRepository;
            _speccondRepository = speccondRepository;
            _phyabRepository = phyabRepository;
            _balanceRepository = balanceRepository;
            _historyRepository = historyRepository;
            _healthlivingRepository = healthlivingRepository;
            _cpnutRepository = cpnutRepository;
            _phygieneRepository = phygieneRepository;
            _infectionRepository = infectionRepository;
            _mtaskRepository = mtaskRepository;
            _objRepository = objRepository;
            _petsRepository = petsRepository;
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
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status201Created)]
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
                                       GetClientComplain = (from com in _complainRepository.Table
                                                            where com.ClientId == id.Value
                                                            select new GetClientComplainRegister
                                                           {
                                                                COMPLAINANTCONTACT = com.COMPLAINANTCONTACT,
                                                                EvidenceFilePath = com.EvidenceFilePath
                                                           }).ToList(),
                                       GetClientLogAudit = (from log in _clientLogAuditRepository.Table
                                                            where log.ClientId == id.Value
                                                            select new GetClientLogAudit
                                                            {
                                                                ActionRecommended = log.ActionRecommended,
                                                                EvidenceOfActionTaken = log.EvidenceOfActionTaken
                                                            }).ToList(),
                                       GetClientMedAudit = (from med in _clientMedAuditRepository.Table
                                                            where med.ClientId == id.Value
                                                            select new GetClientMedAudit
                                                            {
                                                                ActionRecommended = med.ActionRecommended,
                                                                EvidenceOfActionTaken = med.EvidenceOfActionTaken
                                                            }).ToList(),
                                       GetClientVoice =     (from v in _clientVoiceRepository.Table
                                                            where v.ClientId == id.Value
                                                            select new GetClientVoice
                                                            {
                                                                ActionRequired = v.ActionRequired,
                                                                Attachment = v.Attachment
                                                            }).ToList(),
                                       GetClientMgtVisit = (from mg in _clientMgtVisitRepository.Table
                                                         where mg.ClientId == id.Value
                                                         select new GetClientMgtVisit
                                                         {
                                                             ActionRequired = mg.ActionRequired,
                                                             Attachment = mg.Attachment
                                                         }).ToList(),
                                       GetClientProgram = (from cp in _clientProgramRepository.Table
                                                         where cp.ClientId == id.Value
                                                         select new GetClientProgram
                                                         {
                                                             ActionRequired = cp.ActionRequired,
                                                             Attachment = cp.Attachment
                                                         }).ToList(),
                                       GetClientServiceWatch = (from sw in _clientServiceWatchRepository.Table
                                                         where sw.ClientId == id.Value
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
                                       GetClientBloodCoagulationRecord = (from sw in _bloodcoagRepository.Table
                                                                where sw.ClientId == id.Value
                                                                select new GetClientBloodCoagulationRecord
                                                                {
                                                                    Date = sw.Date,
                                                                    Comment = sw.Comment
                                                                }).ToList(),
                                       GetClientBloodPressure= (from sw in _bloodpressureRepository.Table
                                                                          where sw.ClientId == id.Value
                                                                          select new GetClientBloodPressure
                                                                          {
                                                                              Date = sw.Date,
                                                                              Comment = sw.Comment
                                                                          }).ToList(),
                                       GetClientBMIChart= (from sw in _bmichartRepository.Table
                                                                 where sw.ClientId == id.Value
                                                                 select new GetClientBMIChart
                                                                 {
                                                                     Date = sw.Date,
                                                                     Comment = sw.Comment
                                                                 }).ToList(),
                                       GetClientBodyTemp = (from sw in _bodytempRepository.Table
                                                            where sw.ClientId == id.Value
                                                            select new GetClientBodyTemp
                                                            {
                                                                Date = sw.Date,
                                                                Comment = sw.Comment
                                                            }).ToList(),
                                       GetClientBowelMovement = (from sw in _bowelmovementRepository.Table
                                                            where sw.ClientId == id.Value
                                                            select new GetClientBowelMovement
                                                            {
                                                                Date = sw.Date,
                                                                Comment = sw.Comment
                                                            }).ToList(),
                                       GetClientEyeHealthMonitoring = (from sw in _eyehealthRepository.Table
                                                                 where sw.ClientId == id.Value
                                                                 select new GetClientEyeHealthMonitoring
                                                                 {
                                                                     Date = sw.Date,
                                                                     Comment = sw.Comment
                                                                 }).ToList(),
                                       GetClientFoodIntake= (from sw in _foodintakeRepository.Table
                                                                       where sw.ClientId == id.Value
                                                                       select new GetClientFoodIntake
                                                                       {
                                                                           Date = sw.Date,
                                                                           Comment = sw.Comment
                                                                       }).ToList(),
                                       GetClientHeartRate = (from sw in _heartrateRepository.Table
                                                              where sw.ClientId == id.Value
                                                              select new GetClientHeartRate
                                                              {
                                                                  Date = sw.Date,
                                                                  Comment = sw.Comment
                                                              }).ToList(),
                                       GetClientOxygenLvl = (from sw in _oxygenlvlRepository.Table
                                                             where sw.ClientId == id.Value
                                                             select new GetClientOxygenLvl
                                                             {
                                                                 Date = sw.Date,
                                                                 Comment = sw.Comment
                                                             }).ToList(),
                                       GetClientPainChart = (from sw in _painchartRepository.Table
                                                             where sw.ClientId == id.Value
                                                             select new GetClientPainChart
                                                             {
                                                                 Date = sw.Date,
                                                                 Comment = sw.Comment
                                                             }).ToList(),
                                       GetClientPulseRate = (from sw in _pulserateRepository.Table
                                                             where sw.ClientId == id.Value
                                                             select new GetClientPulseRate
                                                             {
                                                                 Date = sw.Date,
                                                                 Comment = sw.Comment
                                                             }).ToList(),
                                       GetClientSeizure = (from sw in _seizureRepository.Table
                                                             where sw.ClientId == id.Value
                                                             select new GetClientSeizure
                                                             {
                                                                 Date = sw.Date,
                                                                 Remarks = sw.Remarks
                                                             }).ToList(),
                                       GetClientWoundCare = (from sw in _woundcareRepository.Table
                                                           where sw.ClientId == id.Value
                                                           select new GetClientWoundCare
                                                           {
                                                               Date = sw.Date,
                                                               Comment = sw.Comment
                                                           }).ToList(),
                                       GetReview = (from sw in _personalRepository.Table
                                                    where sw.ClientId == id.Value
                                                    select new GetReview
                                                    {
                                                        CP_PreDate = sw.Review.CP_PreDate,
                                                        CP_ReviewDate = sw.Review.CP_ReviewDate
                                                    }).ToList(),
                                       GetPets = (from sw in _petsRepository.Table
                                                    where sw.ClientId == id.Value
                                                    select new GetPets
                                                    {
                                                        Name = sw.Name,
                                                        Age = sw.Age
                                                    }).ToList(),
                                       GetInterestAndObjective = (from sw in _objRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetInterestAndObjective
                                                  {
                                                      CareGoal = sw.CareGoal,
                                                  }).ToList(),
                                       GetPersonalHygiene = (from sw in _phygieneRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetPersonalHygiene
                                                  {
                                                      LaundrySupport = sw.LaundrySupport,
                                                      LaundryGuide = sw.LaundryGuide
                                                  }).ToList(),
                                       GetInfectionControl = (from sw in _infectionRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetInfectionControl
                                                  {
                                                      TestDate = sw.TestDate,
                                                      Remarks = sw.Remarks
                                                  }).ToList(),
                                       GetManagingTasks = (from sw in _mtaskRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetManagingTasks
                                                  {
                                                      Help = sw.Help,
                                                  }).ToList(),
                                       GetCarePlanNutrition = (from sw in _cpnutRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetCarePlanNutrition
                                                  {
                                                      SpecialDiet = sw.SpecialDiet,
                                                      AvoidFood = sw.AvoidFood
                                                  }).ToList(),
                                       GetBalance = (from sw in _balanceRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetBalance
                                                  {
                                                      Name = sw.Name,
                                                      Description = sw.Description
                                                  }).ToList(),
                                       GetPhysicalAbility = (from sw in _phyabRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetPhysicalAbility
                                                  {
                                                      Name = sw.Name,
                                                      Description = sw.Description
                                                  }).ToList(),
                                       GetHealthAndLiving = (from sw in _healthlivingRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetHealthAndLiving
                                                  {
                                                      BriefHealth = sw.BriefHealth,
                                                      WakeUp = sw.WakeUp
                                                  }).ToList(),
                                       GetSpecialHealthAndMedication = (from sw in _spcmedRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetSpecialHealthAndMedication
                                                  {
                                                      Date = sw.Date,
                                                      By = sw.By
                                                  }).ToList(),
                                       GetSpecialHealthCondition = (from sw in _speccondRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetSpecialHealthCondition
                                                  {
                                                      ConditionName = sw.ConditionName,
                                                      SourceInformation = sw.SourceInformation
                                                  }).ToList(),
                                       GetHistoryOfFall = (from sw in _historyRepository.Table
                                                  where sw.ClientId == id.Value
                                                  select new GetHistoryOfFall
                                                  {
                                                      Date = sw.Date,
                                                      Cause = sw.Cause
                                                  }).ToList(),
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