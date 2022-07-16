using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Health.PhysicalAbility;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PhysicalAbilityController : ControllerBase
    {
        private IGenericRepository<PhysicalAbility> _physicalAbilityRepository;
        

        public PhysicalAbilityController(IGenericRepository<PhysicalAbility> physicalAbilityRepository)
        {
            _physicalAbilityRepository = physicalAbilityRepository;
        }
        #region PhysicalAbility
        /// <summary>
        /// Get All PhysicalAbility
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetPhysicalAbility>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _physicalAbilityRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create PhysicalAbility
        /// </summary>
        /// <param name="postPhysicalAbility"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostPhysicalAbility postPhysicalAbility)
        {
            if (postPhysicalAbility == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var PhysicalAbility = Mapper.Map<PhysicalAbility>(postPhysicalAbility);
            await _physicalAbilityRepository.InsertEntity(PhysicalAbility);
            return Ok();
        }
        /// <summary>
        /// Update PhysicalAbility
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostPhysicalAbility models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var PhysicalAbility = Mapper.Map<PhysicalAbility>(models);
            await _physicalAbilityRepository.UpdateEntity(PhysicalAbility);
            return Ok();

        }
        /// <summary>
        /// Get PhysicalAbility by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetPhysicalAbility), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getPhysicalAbility = await (from c in _physicalAbilityRepository.Table
                                           where c.PhysicalId == id.Value
                                           select new GetPhysicalAbility
                                           {
                                                ClientId = c.ClientId,
                                                PhysicalId = c.PhysicalId,
                                                Description = c.Description,
                                                Name = c.Name,
                                                Mobility = c.Mobility,
                                                Status = c.Status
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getPhysicalAbility);
        }
        /// <summary>
        /// Get PhysicalAbility by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetbyClient/{id}")]
        [ProducesResponseType(type: typeof(GetPhysicalAbility), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getPhysicalAbility = await (from c in _physicalAbilityRepository.Table
                                            where c.ClientId == id.Value
                                            select new GetPhysicalAbility
                                            {
                                                ClientId = c.ClientId,
                                                PhysicalId = c.PhysicalId,
                                                Description = c.Description,
                                                Name = c.Name,
                                                Mobility = c.Mobility,
                                                Status = c.Status
                                            }
                      ).FirstOrDefaultAsync();
            return Ok(getPhysicalAbility);
        }
        #endregion
    }
}
