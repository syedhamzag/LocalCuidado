using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Medication;
using AwesomeCare.DataTransferObject.DTOs.MedicationManufacturer;
using AwesomeCare.DataTransferObject.DTOs.StaffRotaMed;
using AwesomeCare.DataTransferObject.EqualityComparers;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private IGenericRepository<Medication> _medicationRepository;
        private IGenericRepository<MedicationManufacturer> _medicationManufacturerRepository;
        private IGenericRepository<ClientMedication> _clientMedicationRepository;
        private IGenericRepository<ClientMedicationPeriod> _medicationPeriodRepository;
        private IGenericRepository<ClientMedicationDay> _medicationDayRepository;
        private IGenericRepository<ClientRotaDays> _clientRotaDaysRepository;
        private IGenericRepository<StaffMedRota> _staffMedRotaRepository;
        private IGenericRepository<ClientRotaType> _clientRotaTypeRepository;
        private IGenericRepository<Client> _clientRepository;
        private IGenericRepository<StaffPersonalInfo> _staffPersonalInfoRepository;
        private IGenericRepository<Rota> _rotaRepository;

        public MedicationController(IGenericRepository<Medication> medicationRepository, IGenericRepository<MedicationManufacturer> medicationManufacturerRepository, 
            IGenericRepository<ClientMedicationPeriod> medicationPeriodRepository, IGenericRepository<ClientMedicationDay> medicationDayRepository, 
            IGenericRepository<ClientMedication> clientMedicationRepository, IGenericRepository<ClientRotaDays> clientRotaDaysRepository, 
            IGenericRepository<Client> clientRepository, IGenericRepository<ClientRotaType> clientRotaTypeRepository, IGenericRepository<Rota> rotaRepository,
            IGenericRepository<StaffMedRota> staffMedRotaRepository, IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository)
        {
            _medicationRepository = medicationRepository;
            _medicationManufacturerRepository = medicationManufacturerRepository;
            _medicationPeriodRepository = medicationPeriodRepository;
            _medicationDayRepository = medicationDayRepository;
            _clientMedicationRepository = clientMedicationRepository;
            _clientRotaDaysRepository = clientRotaDaysRepository;
            _clientRepository = clientRepository;
            _clientRotaTypeRepository = clientRotaTypeRepository;
            _staffPersonalInfoRepository = staffPersonalInfoRepository;
            _staffMedRotaRepository = staffMedRotaRepository;
            _rotaRepository = rotaRepository;
        }
        #region Medication
        /// <summary>
        /// Get Medication by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetMedicationById")]
        [ProducesResponseType(type: typeof(GetMedication), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _medicationRepository.Table.ProjectTo<GetMedication>().FirstOrDefault(d => d.MedicationId == id && !d.Deleted);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get All Medications
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetMedication>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _medicationRepository.Table.Where(r => !r.Deleted).ProjectTo<GetMedication>().ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create Medication
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetMedication), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostMedication model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isMedication = _medicationRepository.Table.Any(r => r.MedicationName.ToLower().Equals(model.MedicationName.ToLower()));
            if (isMedication)
            {
                return BadRequest($"Medication {model.MedicationName} already exist");
            }
            var postEntity = Mapper.Map<Medication>(model);
            var newEntity = await _medicationRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetMedication>(newEntity);

            return CreatedAtRoute("GetMedicationById", new { id = getEntity.MedicationId }, getEntity);
        }

        /// <summary>
        /// Update Medication
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetMedication), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutMedication model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _medicationRepository.GetEntity(model.MedicationId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _medicationRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetMedication>(updateEntity);

            return Ok(getEntity);


        }

        #endregion
        #region Manufacturer
        /// <summary>
        /// Get Manufacturer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Manufacturer/{id}", Name = "GetManufacturerById")]
        [ProducesResponseType(type: typeof(GetMedicationManufacturer), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetManufacturer(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _medicationManufacturerRepository.Table.ProjectTo<GetMedicationManufacturer>().FirstOrDefault(d => d.MedicationManufacturerId == id && !d.Deleted);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get All Manufacturers
        /// </summary>
        /// <returns></returns>
        [HttpGet("Manufacturer", Name = "GetManufacturers")]
        [ProducesResponseType(type: typeof(List<GetMedicationManufacturer>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetManufacturers()
        {
            var getEntities = _medicationManufacturerRepository.Table.Where(r => !r.Deleted).ProjectTo<GetMedicationManufacturer>().ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create Manufacturer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Manufacturer")]
        [ProducesResponseType(type: typeof(GetMedicationManufacturer), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostManufacturer([FromBody]PostMedicationManufacturer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool exists = _medicationManufacturerRepository.Table.Any(r => r.Manufacturer.ToLower().Equals(model.Manufacturer.ToLower()));
            if (exists)
            {
                return BadRequest($"Manufacturer {model.Manufacturer} already exist");
            }
            var postEntity = Mapper.Map<MedicationManufacturer>(model);
            var newEntity = await _medicationManufacturerRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetMedicationManufacturer>(newEntity);

            return CreatedAtRoute("GetManufacturerById", new { id = getEntity.MedicationManufacturerId }, getEntity);
        }

        /// <summary>
        /// Update Manufacturer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("Manufacturer")]
        [ProducesResponseType(type: typeof(GetMedicationManufacturer), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutManufacturer([FromBody]PutMedicationManufacturer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _medicationManufacturerRepository.GetEntity(model.MedicationManufacturerId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _medicationManufacturerRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetMedicationManufacturer>(updateEntity);

            return Ok(getEntity);


        }

        #endregion

        #region Tracker
        /// <summary>
        /// Create Staff Medication Tracker
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(type: typeof(GetStaffMedRota), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PostStaffMedRota model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<StaffMedRota>(model);
            var newEntity = await _staffMedRotaRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetStaffMedRota>(newEntity);

            return Ok(getEntity);
        }
        /// <summary>
        /// Update Staff Medication Tracker
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType(type: typeof(GetStaffMedRota), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutStaffMedRota model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<StaffMedRota>(model);
            var newEntity = await _staffMedRotaRepository.UpdateEntity(postEntity);
            var getEntity = Mapper.Map<GetStaffMedRota>(newEntity);

            return Ok(getEntity);
        }
        [HttpGet]
        [Route("MedTracker/{sdate}/{edate}")]
        [ProducesResponseType(typeof(List<MedTracker>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult MedTracker(string sdate, string edate)
        {
            string format = "yyyy-MM-dd";
            bool isStartDateValid = DateTime.TryParseExact(sdate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime startDate);
            if (!isStartDateValid)
            {
                return BadRequest($"Invalid StartDate format, Format is {format}");
            }

            bool isStopDateValid = DateTime.TryParseExact(edate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);
            if (!isStopDateValid)
            {
                return BadRequest($"Invalid StopDate format, Format is {format}");
            }

            var tracker = (from smt in _staffMedRotaRepository.Table
                           join period in _medicationPeriodRepository.Table on smt.RotaId equals period.RotaId
                           join day in _medicationDayRepository.Table on period.ClientMedicationDayId equals day.ClientMedicationDayId
                           join med in _clientMedicationRepository.Table on day.ClientMedicationId equals med.ClientMedicationId
                           join c in _clientRepository.Table on med.ClientId equals c.ClientId
                           join crt in _clientRotaTypeRepository.Table on period.ClientRotaTypeId equals crt.ClientRotaTypeId
                           join r in _rotaRepository.Table on smt.RotaId equals r.RotaId
                           join st in _staffPersonalInfoRepository.Table on smt.Staff equals st.StaffPersonalInfoId
                           where smt.RotaDate >= startDate && smt.RotaDate <= stopDate
                         select new MedTracker
                         {
                             RotaDate = smt.RotaDate,
                             RotaName = r.RotaName,
                             ClientId = med.ClientId,
                             ClientIdNumber = c.IdNumber,
                             ClientMedicationId = med.ClientMedicationId,
                             ClientMedImage = med.ClientMedImage,
                             ClientName = string.Concat(c.Firstname,c.Middlename,c.Surname),
                             DOB = c.DateOfBirth,
                             Dossage = med.Dossage,
                             ExpiryDate = med.ExpiryDate,
                             Frequency = med.Frequency,
                             Gap_Hour = med.Gap_Hour,
                             Means = med.Means,
                             Medication = med.Medication.MedicationName,
                             MedicationId = med.MedicationId,
                             MedicationManufacturer = med.MedicationManufacturer.Manufacturer,
                             MedicationManufacturerId = med.MedicationManufacturerId,
                             PERIOD = crt.RotaType,
                             Remark = med.Remark,
                             StartDate = med.StartDate,
                             StopDate = med.StopDate,
                             TimeCritical = med.TimeCritical,
                             Route = med.Route,
                             Status = med.Status,
                             Type = med.Type,
                             StaffName = string.Concat(st.FirstName, st.LastName),
                             //Feedback = c.Feedback,
                             NoOfStaff = c.NumberOfStaff
                             
                         }).OrderBy(o => o.ClientId).ToList();

            var distincttracker = tracker.Distinct(new MedTrackerEqualityComparer()).ToList();
            return Ok(distincttracker);
        }
        #endregion
    }
}