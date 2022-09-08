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
using AwesomeCare.DataTransferObject.DTOs.FilesAndRecord;
using AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment;
using AwesomeCare.DataTransferObject.DTOs.Client.CareObj;
using AwesomeCare.DataTransferObject.DTOs.CuidiBuddy;
using AwesomeCare.DataTransferObject.DTOs.ClientHealthCondition;
using AwesomeCare.DataTransferObject.DTOs.ClientHobbies;
using AwesomeCare.DataTransferObject.DTOs.CareReview;
using AwesomeCare.DataTransferObject.DTOs;

namespace AwesomeCare.API.Controllers
{
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
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<HealthCondition> _healthconRepository;
        private IGenericRepository<Hobbies> _hobbyRepository;


        private AwesomeCareDbContext _dbContext;
        public ClientController(AwesomeCareDbContext dbContext, IGenericRepository<Client> clientRepository, IGenericRepository<ClientMedication> clientMedicationRepository, IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<HealthCondition> healthconRepository,
            IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, IGenericRepository<BaseRecordModel> baseRecordRepository, IGenericRepository<RotaDayofWeek> rotaDayOfWeekRepository, IGenericRepository<Hobbies> hobbyRepository,
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
            _staffRepository = staffRepository;
            _healthconRepository = healthconRepository;
            _hobbyRepository = hobbyRepository;
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

            var getClient = await (from client in _clientRepository.Table
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
                                       KeyworkerId = client.KeyworkerId,
                                       IdNumber = client.IdNumber,
                                       GenderId = client.GenderId,
                                       NumberOfCalls = client.NumberOfCalls,
                                       AreaCodeId = client.AreaCodeId,
                                       TeritoryId = client.TeritoryId,
                                       ServiceId = client.ServiceId,
                                       ProvisionVenue = client.ProvisionVenue,
                                       PostCode = client.PostCode,
                                       Rate = client.Rate,
                                       TeamLeaderId = client.TeamLeaderId,
                                       DateOfBirth = client.DateOfBirth,
                                       Telephone = client.Telephone,
                                       LanguageId = client.LanguageId,
                                       KeySafe = client.KeySafe,
                                       ChoiceOfStaffId = client.ChoiceOfStaffId,
                                       StatusId = client.StatusId,
                                       CapacityId = client.CapacityId,
                                       ProviderReference = client.ProviderReference,
                                       NumberOfStaff = client.NumberOfStaff,
                                       UniqueId = client.UniqueId,
                                       PassportFilePath = client.PassportFilePath,
                                       Latitude = client.Latitude,
                                       Longitude = client.Longitude,
                                       Address = client.Address,
                                       PreferredName = client.PreferredName,
                                       ClientManager = client.ClientManager,
                                       Aid = client.Aid,
                                       Pin = client.Pin,
                                       Denture = client.Denture,
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
                                       GetCuidiBuddy = (from cu in client.CuidiBuddy
                                                        select new GetCuidiBuddy
                                                        {
                                                            CuidiBuddyId = cu.CuidiBuddyId,
                                                        }).ToList(),
                                       GetClientHealthCondition = (from hc in client.ClientHealthCondition
                                                                   select new GetClientHealthCondition
                                                                   {
                                                                       HCId = hc.HCId,
                                                                   
                                                                   }).ToList(),
                                       GetClientHobbies = (from hc in client.ClientHobbies
                                                                   select new GetClientHobbies
                                                                   {
                                                                       HId = hc.HId,

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
                                       Latitude = client.Latitude,
                                       Longitude = client.Longitude,
                                       Address = client.Address,
                                       PreferredName = client.PreferredName,
                                       ClientManager = client.ClientManager,
                                       Aid = client.Aid,
                                       Denture = client.Denture,
                                       ManagerName = _staffRepository.Table.Where(s=>s.StaffPersonalInfoId==client.ClientManager).Select(n=>n.FirstName+" "+n.MiddleName+" "+n.LastName).FirstOrDefault()
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
        /// Update Client Medication
        /// </summary>
        /// <returns></returns>
        [HttpPut("Medication")]
        [ProducesResponseType(type: typeof(GetClientMedication), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutMedication([FromBody] PutClientMedication model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }
                var entity = Mapper.Map<ClientMedication>(model);
                var newEntity = await _clientMedicationRepository.UpdateEntity(entity);
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
                                                                                             RotaType = cltp.RotaType,
                                                                                             RotaId = pd.RotaId,
                                                                                             StopTime = pd.StopTime,
                                                                                             StartTime = pd.StartTime
                                                                                             
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
                                    StopDate = clmd.StopDate,
                                    ClientMedImage = clmd.ClientMedImage,
                                    Means = clmd.Means,
                                    Type = clmd.Type,
                                    TimeCritical = clmd.TimeCritical
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
                                                                                             RotaType = cltp.RotaType,
                                                                                             RotaId = pd.RotaId,
                                                                                             StartTime = pd.StartTime,
                                                                                             StopTime = pd.StopTime
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

        #region Client_Details
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetHealthHobby/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHealthHobby(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientHealthCondition = (from chc in client.ClientHealthCondition
                                                                   join hc in _healthconRepository.Table on chc.HCId equals hc.HCId
                                                                   select new GetClientHealthCondition
                                                                   {
                                                                       HCId = chc.HCId,
                                                                       Name = hc.Name
                                                                   }).ToList(),
                                       GetClientHobbies =       (from ch in client.ClientHobbies
                                                                   join h in _hobbyRepository.Table on ch.HId equals h.HId
                                                                   select new GetClientHobbies
                                                                   {
                                                                       HId = ch.HId,
                                                                       Name = h.Name
                                                                   }).ToList(),


                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetInvolvingParty/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInvolvingParty(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
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
                                                           }).ToList()

                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetComplain/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetComplain(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientComplain = (from com in client.ComplainRegister
                                                            select new GetClientComplainRegister
                                                            {
                                                                COMPLAINANTCONTACT = com.COMPLAINANTCONTACT,
                                                                EvidenceFilePath = com.EvidenceFilePath,
                                                                ACTIONTAKEN = com.ACTIONTAKEN,
                                                                DATERECIEVED = com.DATERECIEVED,
                                                                DUEDATE = com.DUEDATE,
                                                                REMARK = com.REMARK,
                                                                INCIDENTDATE = com.INCIDENTDATE,
                                                                Reference = com.Reference,
                                                                SOURCEOFCOMPLAINTS = com.SOURCEOFCOMPLAINTS,
                                                            }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetLogAudit/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLogAudit(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientLogAudit = (from log in client.ClientLogAudit
                                                            select new GetClientLogAudit
                                                            {
                                                                ActionRecommended = log.ActionRecommended,
                                                                EvidenceOfActionTaken = log.EvidenceOfActionTaken,
                                                                ActionTaken = log.ActionTaken,
                                                                Communication = log.Communication,
                                                                Date = log.Date,
                                                                Deadline = log.Deadline,
                                                                NextDueDate = log.NextDueDate,
                                                                EvidenceFilePath = log.EvidenceFilePath,
                                                                ImproperDocumentation = log.ImproperDocumentation,
                                                                IsCareDifference = log.IsCareDifference,
                                                                IsCareExpected = log.IsCareExpected,
                                                                LessonLearntAndShared = log.LessonLearntAndShared,
                                                                NameOfAuditor = log.NameOfAuditor,
                                                                Observations = log.Observations,
                                                                ProperDocumentation = log.ProperDocumentation,
                                                                LogAuditId = log.LogAuditId,
                                                                Status = log.Status,
                                                                Reference = log.Reference,
                                                                Remarks = log.Remarks,
                                                                
                                                            }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetMedAudit/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMedAudit(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientMedAudit = (from med in client.ClientMedAudit
                                                            select new GetClientMedAudit
                                                            {
                                                                ActionRecommended = med.ActionRecommended,
                                                                EvidenceOfActionTaken = med.EvidenceOfActionTaken,
                                                                ActionTaken = med.ActionTaken,
                                                                Date = med.Date,
                                                                Deadline = med.Deadline,
                                                                NextDueDate = med.NextDueDate,
                                                                Attachment = med.Attachment,
                                                                LessonLearntAndShared = med.LessonLearntAndShared,
                                                                Observations = med.Observations,
                                                                MedAuditId = med.MedAuditId,
                                                                Status = med.Status
                                                            }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetVoice/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVoice(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientVoice = (from v in client.ClientVoice
                                                         select new GetClientVoice
                                                         {
                                                             ActionRequired = v.ActionRequired,
                                                             Attachment = v.Attachment,
                                                             Remarks = v.Remarks,
                                                             Status = v.Status,
                                                             ActionsTakenByMPCC = v.ActionsTakenByMPCC,
                                                             AreasOfImprovements = v.AreasOfImprovements,
                                                             Date   = v.Date,
                                                             Deadline = v.Deadline,
                                                             EvidenceOfActionTaken = v.EvidenceOfActionTaken,
                                                             NextCheckDate = v.NextCheckDate,
                                                             Reference = v.Reference,  
                                                             VoiceId = v.VoiceId,
                                                         }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetMgtVisit/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMgtVisit(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientMgtVisit = (from mg in client.ClientMgtVisit
                                                            select new GetClientMgtVisit
                                                            {
                                                                ActionRequired = mg.ActionRequired,
                                                                Attachment = mg.Attachment,
                                                                ActionsTakenByMPCC = mg.ActionsTakenByMPCC,
                                                                Date = mg.Date,
                                                                Deadline = mg.Deadline,
                                                                EvidenceOfActionTaken = mg.EvidenceOfActionTaken,
                                                                NextCheckDate = mg.NextCheckDate,
                                                                Observation = mg.Observation,
                                                                Reference = mg.Reference,
                                                                Remarks = mg.Remarks,
                                                                Status = mg.Status,
                                                                VisitId = mg.VisitId,
                                                            }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetProgram/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProgram(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientProgram = (from cp in client.ClientProgram
                                                           select new GetClientProgram
                                                           {
                                                               ActionRequired = cp.ActionRequired,
                                                               Attachment = cp.Attachment,
                                                               Date = cp.Date,
                                                               Status = cp.Status,
                                                               Observation  = cp.Observation,
                                                               Deadline = cp.Deadline,
                                                               NextCheckDate=cp.NextCheckDate,
                                                               Reference = cp.Reference,
                                                               Remarks=cp.Remarks,
                                                               URL = cp.URL,
                                                               ProgramId = cp.ProgramId,
                                                           }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetServiceWatch/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetServiceWatch(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientServiceWatch = (from sw in client.ClientServiceWatch
                                                                select new GetClientServiceWatch
                                                                {
                                                                    ActionRequired = sw.ActionRequired,
                                                                    Attachment = sw.Attachment,
                                                                    Date= sw.Date,
                                                                    Deadline = sw.Deadline,
                                                                    NextCheckDate = sw.NextCheckDate,
                                                                    Observation = sw.Observation,
                                                                    Status = sw.Status,
                                                                    URL = sw.URL,
                                                                    Remarks = sw.Remarks,
                                                                    Reference = sw.Reference,
                                                                    WatchId = sw.WatchId,
                                                                }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBloodCoag/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBloodCoag(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientBloodCoagulationRecord = (from coag in client.ClientBloodCoagulationRecord
                                                                          select new GetClientBloodCoagulationRecord
                                                                          {
                                                                              Date = coag.Date,
                                                                              Comment = coag.Comment,
                                                                              BloodRecordId = coag.BloodRecordId,
                                                                              BloodStatus = coag.BloodStatus,
                                                                              Deadline = coag.Deadline,
                                                                              PhysicianResponce = coag.PhysicianResponce,
                                                                              Reference = coag.Reference,
                                                                              Remark = coag.Remark,
                                                                              Status = coag.Status,
                                                                          }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPressure/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPressure(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientBloodPressure = (from pres in client.ClientBloodPressure
                                                                 select new GetClientBloodPressure
                                                                 {
                                                                     Date = pres.Date,
                                                                     Comment = pres.Comment,
                                                                     BloodPressureId = pres.BloodPressureId,
                                                                     Deadline = pres.Deadline,
                                                                     PhysicianResponse  = pres.PhysicianResponse,
                                                                     Reference = pres.Reference,
                                                                     Remarks = pres.Remarks,
                                                                     Status = pres.Status,
                                                                     
                                                                 }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBMIChart/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBMIChart(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientBMIChart = (from bmi in client.ClientBMIChart
                                                            select new GetClientBMIChart
                                                            {
                                                                Date = bmi.Date,
                                                                Comment = bmi.Comment,
                                                                BMIChartId = bmi.BMIChartId,
                                                                Deadline = bmi.Deadline,
                                                                PhysicianResponse = bmi.PhysicianResponse,
                                                                Reference = bmi.Reference,
                                                                Remarks = bmi.Remarks,
                                                                Status = bmi.Status,
                                                            }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBodyTemp/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBodyTemp(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientBodyTemp = (from temp in client.ClientBodyTemp
                                                            select new GetClientBodyTemp
                                                            {
                                                                Date = temp.Date,
                                                                Comment = temp.Comment,
                                                                BodyTempId = temp.BodyTempId,
                                                                Deadline = temp.Deadline,
                                                                PhysicianResponse = temp.PhysicianResponse,
                                                                Reference = temp.Reference,
                                                                Remarks = temp.Remarks,
                                                                Status = temp.Status,
                                                            }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBowel/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBowel(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientBowelMovement = (from bowel in client.ClientBowelMovement
                                                                 select new GetClientBowelMovement
                                                                 {
                                                                     Date = bowel.Date,
                                                                     Comment = bowel.Comment,
                                                                     BowelMovementId = bowel.BowelMovementId,
                                                                     Deadline = bowel.Deadline,
                                                                     PhysicianResponse = bowel.PhysicianResponse,
                                                                     Reference = bowel.Reference,
                                                                     Remarks = bowel.Remarks,
                                                                     Status = bowel.Status,
                                                                 }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetEyeHealth/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEyeHealth(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientEyeHealthMonitoring = (from eye in client.ClientEyeHealthMonitoring
                                                                       select new GetClientEyeHealthMonitoring
                                                                       {
                                                                           Date = eye.Date,
                                                                           Comment = eye.Comment,
                                                                           EyeHealthId = eye.EyeHealthId,
                                                                           Deadline = eye.Deadline,
                                                                           PhysicianResponse = eye.PhysicianResponse,
                                                                           Reference = eye.Reference,
                                                                           Remarks = eye.Remarks,
                                                                           Status = eye.Status,
                                                                       }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetFoodIntake/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFoodIntake(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientFoodIntake = (from food in client.ClientFoodIntake
                                                              select new GetClientFoodIntake
                                                              {
                                                                  Date = food.Date,
                                                                  Comment = food.Comment,
                                                                  FoodIntakeId = food.FoodIntakeId,
                                                                  Deadline = food.Deadline,
                                                                  PhysicianResponse = food.PhysicianResponse,
                                                                  Reference = food.Reference,
                                                                  Remarks = food.Remarks,
                                                                  Status = food.Status,
                                                              }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetHeartRate/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHeartRate(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientHeartRate = (from heart in client.ClientHeartRate
                                                             select new GetClientHeartRate
                                                             {
                                                                 Date = heart.Date,
                                                                 Comment = heart.Comment,
                                                                 HeartRateId = heart.HeartRateId,
                                                                 Deadline = heart.Deadline,
                                                                 PhysicianResponse = heart.PhysicianResponse,
                                                                 Reference = heart.Reference,
                                                                 Remarks = heart.Remarks,
                                                                 Status = heart.Status,
                                                             }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetOxygenLvl/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOxygenLvl(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientOxygenLvl = (from lvl in client.ClientOxygenLvl
                                                             select new GetClientOxygenLvl
                                                             {
                                                                 Date = lvl.Date,
                                                                 Comment = lvl.Comment,
                                                                 OxygenLvlId = lvl.OxygenLvlId,
                                                                 Deadline = lvl.Deadline,
                                                                 PhysicianResponse = lvl.PhysicianResponse,
                                                                 Reference = lvl.Reference,
                                                                 Remarks = lvl.Remarks,
                                                                 Status = lvl.Status,
                                                             }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPainChart/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPainChart(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientPainChart = (from pain in client.ClientPainChart
                                                             select new GetClientPainChart
                                                             {
                                                                 Date = pain.Date,
                                                                 Comment = pain.Comment,
                                                                 PainChartId = pain.PainChartId,
                                                                 Deadline = pain.Deadline,
                                                                 PhysicianResponse = pain.PhysicianResponse,
                                                                 Reference = pain.Reference,
                                                                 Remarks = pain.Remarks,
                                                                 Status = pain.Status,
                                                             }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPulseRate/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPulseRate(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientPulseRate = (from pulse in client.ClientPulseRate
                                                             select new GetClientPulseRate
                                                             {
                                                                 Date = pulse.Date,
                                                                 Comment = pulse.Comment,
                                                                 PulseRateId = pulse.PulseRateId,
                                                                 Deadline = pulse.Deadline,
                                                                 PhysicianResponse = pulse.PhysicianResponse,
                                                                 Reference = pulse.Reference,
                                                                 Remarks = pulse.Remarks,
                                                                 Status = pulse.Status,
                                                             }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetSeizure/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSeizure(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientSeizure = (from seiz in client.ClientSeizure
                                                           select new GetClientSeizure
                                                           {
                                                               Date = seiz.Date,
                                                               Remarks = seiz.Remarks,
                                                               SeizureId = seiz.SeizureId,
                                                               Deadline = seiz.Deadline,
                                                               PhysicianResponse = seiz.PhysicianResponse,
                                                               Reference = seiz.Reference,
                                                               Status = seiz.Status,
                                                               WhatHappened = seiz.WhatHappened,
                                                           }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetWoundCare/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWoundCare(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientWoundCare = (from wc in client.ClientWoundCare
                                                             select new GetClientWoundCare
                                                             {
                                                                Date = wc.Date,
                                                                Comment = wc.Comment,
                                                                WoundCareId = wc.WoundCareId,
                                                                Deadline = wc.Deadline,
                                                                PhysicianResponse  = wc.PhysicianResponse,
                                                                Reference = wc.Reference,
                                                                Remarks = wc.Remarks,
                                                                Status = wc.Status,
                                                             }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetReview/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReview(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetReview = (from rew in client.PersonalDetail
                                                    select new GetReview
                                                    {
                                                        CP_PreDate = rew.Review.CP_PreDate,
                                                        CP_ReviewDate = rew.Review.CP_ReviewDate,

                                                    }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPets/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPets(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetPets = (from pet in client.Pets
                                                  select new GetPets
                                                  {
                                                      PetsId = pet.PetsId,
                                                      Age = pet.Age,
                                                      ClientId = pet.ClientId,
                                                      Gender = pet.Gender,
                                                      Name = pet.Name,
                                                      MealPattern = pet.MealPattern,
                                                      MealStorage = pet.MealStorage,
                                                      PetCare = pet.PetCare,
                                                      PetInsurance = pet.PetInsurance,
                                                      Type = pet.Type,
                                                      PetActivities = pet.PetActivities,
                                                      VetVisit = pet.VetVisit
                                                  }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetInterestAndObj/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInterestAndObj(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetInterestAndObjective = (from iao in client.InterestAndObjective
                                                                  select new GetInterestAndObjective
                                                                  {
                                                                      CareGoal = iao.CareGoal,
                                                                  }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPersonalHyg/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPersonalHyg(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetPersonalHygiene = (from ph in client.PersonalHygiene
                                                             select new GetPersonalHygiene
                                                             {
                                                                 LaundrySupport = ph.LaundrySupport,
                                                                 LaundryGuide = ph.LaundryGuide,
                                                                 
                                                             }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetInfectionControl/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInfectionControl(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetInfectionControl = (from ic in client.InfectionControl
                                                              select new GetInfectionControl
                                                              {
                                                                  TestDate = ic.TestDate,
                                                                  Remarks = ic.Remarks
                                                              }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetManagingTask/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetManagingTask(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetManagingTasks = (from mt in client.ManagingTasks
                                                           select new GetManagingTasks
                                                           {
                                                               Help = mt.Help,
                                                           }).ToList(),
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetCarePlanNut/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCarePlanNut(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetCarePlanNutrition = (from cpn in client.CarePlanNutrition
                                                               select new GetCarePlanNutrition
                                                               {
                                                                   SpecialDiet = cpn.SpecialDiet,
                                                                   AvoidFood = cpn.AvoidFood
                                                               }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetBalance/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBalance(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetBalance = (from bln in client.Balance
                                                     select new GetBalance
                                                     {
                                                         Name = bln.Name,
                                                         Description = bln.Description
                                                     }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPhysicalAbility/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPhysicalAbility(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetPhysicalAbility = (from pab in client.PhysicalAbility
                                                             select new GetPhysicalAbility
                                                             {
                                                                 Name = pab.Name,
                                                                 Description = pab.Description
                                                             }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetHealthAndLiving/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHealthAndLiving(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetHealthAndLiving = (from hal in client.HealthAndLiving
                                                             select new GetHealthAndLiving
                                                             {
                                                                 BriefHealth = hal.BriefHealth,
                                                                 WakeUp = hal.WakeUp
                                                             }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetHealthAndMed/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHealthAndMed(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetSpecialHealthAndMedication = (from sham in client.SpecialHealthAndMedication
                                                                        select new GetSpecialHealthAndMedication
                                                                        {
                                                                            Date = sham.Date,
                                                                            By = sham.By,
                                                                            AccessMedication = sham.AccessMedication,
                                                                            AdminLvl = sham.AdminLvl,
                                                                            ClientId = sham.ClientId,
                                                                            Consent = sham.Consent,
                                                                            FamilyMeds = sham.FamilyMeds,
                                                                            FamilyReturnMed = sham.FamilyReturnMed,
                                                                            LeftoutMedicine = sham.LeftoutMedicine,
                                                                            MedAccessDenial = sham.MedAccessDenial,
                                                                            MedicationAllergy = sham.MedicationAllergy,
                                                                            MedicationStorage = sham.MedicationStorage,
                                                                            MedKeyCode = sham.MedKeyCode,
                                                                            MedsGPOrder = sham.MedsGPOrder,
                                                                            NameFormMedicaiton = sham.NameFormMedicaiton,
                                                                            NoMedAccess = sham.NoMedAccess,
                                                                            OverdoseContact = sham.OverdoseContact,
                                                                            PharmaMARChart = sham.PharmaMARChart,
                                                                            PNRDoses = sham.PNRDoses,
                                                                            PNRMedList = sham.PNRMedList,
                                                                            PNRMedReq = sham.PNRMedReq,
                                                                            PNRMedsAdmin = sham.PNRMedsAdmin,
                                                                            PNRMedsMissing = sham.PNRMedsMissing,
                                                                            SHMId = sham.SHMId,
                                                                            SpecialStorage = sham.SpecialStorage,
                                                                            TempMARChart = sham.TempMARChart,
                                                                            Type = sham.Type,
                                                                            WhoAdminister = sham.WhoAdminister
                                                                        }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }/// <summary>
         /// Get Client by Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet("GetHealthCondition/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHealthCondition(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetSpecialHealthCondition = (from shc in client.SpecialHealthCondition
                                                                    select new GetSpecialHealthCondition
                                                                    {
                                                                        ConditionName = shc.ConditionName,
                                                                        SourceInformation = shc.SourceInformation,
                                                                        ClientAction = shc.ClientAction,
                                                                        ClientId = shc.ClientId,
                                                                        ClinicRecommendation = shc.ClinicRecommendation,
                                                                        FeelingAfterIncident = shc.FeelingAfterIncident,
                                                                        FeelingBeforeIncident = shc.FeelingBeforeIncident,
                                                                        Frequency = shc.Frequency,
                                                                        HealthCondId = shc.HealthCondId,
                                                                        LifestyleSupport = shc.LifestyleSupport,
                                                                        LivingActivities = shc.LivingActivities,
                                                                        PlanningHealthCondition = shc.PlanningHealthCondition,
                                                                        Trigger = shc.Trigger
                                                                    }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }/// <summary>
         /// Get Client by Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet("GetHistoryOfFall/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHistoryOfFall(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetHistoryOfFall = (from hof in client.HistoryOfFall
                                                           select new GetHistoryOfFall
                                                           {
                                                               Date = hof.Date,
                                                               Cause = hof.Cause,
                                                               Details = hof.Details,
                                                               HistoryId = hof.HistoryId,
                                                               Prevention = hof.Prevention,
                                                               ClientId = hof.ClientId
                                                           }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }/// <summary>
         /// Get Client by Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet("GetHospitalEntry/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHospitalEntry(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetHospitalEntry = (from hen in client.HospitalEntry
                                                           select new GetHospitalEntry
                                                           {
                                                               HospitalEntryId = hen.HospitalEntryId,
                                                               ClientId = hen.ClientId,
                                                               Date = hen.Date,
                                                               Reference = hen.Reference,
                                                               Attachment = hen.Attachment,
                                                           }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }/// <summary>
         /// Get Client by Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet("GetHospitalExit/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHospitalExit(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetHospitalExit = (from hex in client.HospitalExit
                                                          select new GetHospitalExit
                                                          {
                                                              Date = hex.Date,
                                                              Reference = hex.Reference
                                                          }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }/// <summary>
         /// Get Client by Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet("GetHomeRisk/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHomeRisk(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetHomeRiskAssessment = (from hra in client.HomeRiskAssessment
                                                                select new GetHomeRiskAssessment
                                                                {
                                                                    Heading = hra.Heading
                                                                }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }/// <summary>
         /// Get Client by Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet("GetDutyOnCall/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDutyOnCall(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetDutyOnCall = (from doc in client.DutyOnCall
                                                        select new GetDutyOnCall
                                                        {
                                                            Attachment = doc.Attachment,
                                                            Subject = doc.Subject,
                                                            RefNo = doc.RefNo,
                                                            DateOfCall = doc.DateOfCall

                                                        }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }/// <summary>
         /// Get Client by Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet("GetDailyTask/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDailyTask(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientDailyTask = (from cdt in client.ClientDailyTask
                                                             select new GetClientDailyTask
                                                             {
                                                                 Date = cdt.Date,
                                                                 AmendmentDate = cdt.AmendmentDate,
                                                                 DailyTaskName = cdt.DailyTaskName
                                                             }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }/// <summary>
         /// Get Client by Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet("GetBestInterest/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBestInterest(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetBestInterestAssessment = (from mca in client.BestInterestAssessment
                                                                    select new GetBestInterestAssessment
                                                                    {
                                                                        Date = mca.Date,
                                                                        Name = mca.Name,
                                                                        Signature = mca.Signature,
                                                                        Position = mca.Position
                                                                    }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }/// <summary>
         /// Get Client by Id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet("GetFilesAndRecord/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFilesAndRecord(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetFilesAndRecord = (from f in client.FilesAndRecord
                                                            select new GetFilesAndRecord
                                                            {
                                                                Date = f.Date,
                                                                Subject = f.Subject,
                                                                Attachment = f.Attachment
                                                            }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetCareObj/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCareObj(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetClientCareObj = (from o in client.ClientCareObj
                                                           select new GetClientCareObj
                                                           {
                                                               Date = o.Date,
                                                               Note = o.Note,
                                                           }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetCareReview/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCareReview(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetCareReview = (from o in client.CareReview
                                                           select new GetCareReview
                                                           {
                                                               Date = o.Date,
                                                               Name = o.Name,
                                                               Note = o.Note,
                                                               Action = o.Action,
                                                               CareReviewId = o.CareReviewId,
                                                               ClientName = client.Firstname +" "+ client.Surname

                                                           }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }

        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetIncidentReport/{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetIncidentReport(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   where client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       GetIncidentReports = (from o in client.IncidentReporting
                                                             join staff in _staffRepository.Table on o.ReportingStaffId equals staff.StaffPersonalInfoId
                                                             join involved in _staffRepository.Table on o.StaffInvolvedId equals involved.StaffPersonalInfoId
                                                             join bases in _baseRecordItemRepository.Table on o.IncidentTypeId equals bases.BaseRecordItemId
                                                        select new GetIncidentReport
                                                        {
                                                            IncidentType = bases.ValueName,
                                                            ReportingStaff = staff.FirstName +" "+ staff.LastName,
                                                            StaffInvolved = involved.FirstName +" "+ involved.LastName,

                                                        }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }
        #endregion

    }
}