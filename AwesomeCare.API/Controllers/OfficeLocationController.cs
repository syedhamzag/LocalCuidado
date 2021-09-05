using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.OfficeLocation;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OfficeLocationController : ControllerBase
    {
        private readonly ILogger<OfficeLocationController> logger;
        private readonly IGenericRepository<OfficeLocation> officeLocationRepository;

        public OfficeLocationController(ILogger<OfficeLocationController> logger,
            IGenericRepository<OfficeLocation> officeLocationRepository)
        {
            this.logger = logger;
            this.officeLocationRepository = officeLocationRepository;
        }

        /// <summary>
        /// Get Office Location by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetOfficeLocation), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var entity =await this.officeLocationRepository.GetEntity(id);
            var officeLocation = Mapper.Map<GetOfficeLocation>(entity);

            return Ok(officeLocation);
        }

        /// <summary>
        /// Get all Office Location
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(List<GetOfficeLocation>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var entities = await this.officeLocationRepository.GetEntities();
            var officeLocations = Mapper.Map<List<GetOfficeLocation>>(entities);

            return Ok(officeLocations);
        }

        /// <summary>
        /// Insert Office Location
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(GetOfficeLocation),StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody]PostOfficeLocation model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = Mapper.Map<OfficeLocation>(model);
            var officeLocation =await officeLocationRepository.InsertEntity(entity);
            var getEntity = Mapper.Map<GetOfficeLocation>(officeLocation);

            return CreatedAtAction(nameof(Get), new { id = getEntity.OfficeLocationId }, getEntity);
        }

        /// <summary>
        /// Update Office Location
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]PutOfficeLocation model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var getEntity =await officeLocationRepository.GetEntity(model.OfficeLocationId);
            var updateEntity = Mapper.Map<PutOfficeLocation, OfficeLocation>(model, getEntity);

            var entity = await officeLocationRepository.UpdateEntity(updateEntity);

            return Ok();
        }
    }
}
