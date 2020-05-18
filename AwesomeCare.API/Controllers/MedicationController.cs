using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Medication;
using AwesomeCare.DataTransferObject.DTOs.MedicationManufacturer;
using AwesomeCare.Model.Models;
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
        public MedicationController(IGenericRepository<Medication> medicationRepository, IGenericRepository<MedicationManufacturer> medicationManufacturerRepository)
        {
            _medicationRepository = medicationRepository;
            _medicationManufacturerRepository = medicationManufacturerRepository;
        }

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


    }
}