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
using AwesomeCare.DataTransferObject.DTOs.ClientMedication;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private IGenericRepository<ClientMedicationDay> _clientMedicationDayRepository;
        private IGenericRepository<ClientMedicationPeriod> _clientMedicationPeriodRepository;
        private IGenericRepository<RotaDayofWeek> _rotaDayOfWeekRepository;
        private IGenericRepository<ClientRotaType> _clientRotaTypeRepository;
        private IGenericRepository<Medication> _medicationRepository;
        private IGenericRepository<MedicationManufacturer> _medicationManufacturerRepository;
        private AwesomeCareDbContext _dbContext;
        public ClientController(AwesomeCareDbContext dbContext, IGenericRepository<Client> clientRepository, IGenericRepository<ClientMedicationPeriod> clientMedicationPeriodRepository,
            IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, IGenericRepository<ClientMedicationDay> clientMedicationDayRepository,
            IGenericRepository<BaseRecordModel> baseRecordRepository, IGenericRepository<ClientMedication> clientMedicationRepository,
            IGenericRepository<RotaDayofWeek> rotaDayOfWeekRepository, IGenericRepository<ClientRotaType> clientRotaTypeRepository,
            IGenericRepository<Medication> medicationRepository, IGenericRepository<MedicationManufacturer> medicationManufacturerRepository)
        {
            _clientRepository = clientRepository;
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
        public async Task<IActionResult> PostClient([FromBody]PostClient postClient)
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
                                       PassportFilePath = client.PassportFilePath
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
                                       PassportFilePath = client.PassportFilePath
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
        public async Task<IActionResult> PutClient([FromBody]PutClient model, int? clientId)
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
        public async Task<IActionResult> PostMedication([FromBody]PostClientMedication model)
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
                return CreatedAtAction("GetMedication", new { id = newEntity.ClientMedicationId, clientId=newEntity.ClientId }, getEntity);
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
            if ( !clientId.HasValue)
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

        #endregion

    }
}