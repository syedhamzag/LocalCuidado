using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.InfectionControl;
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
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InfectionControlController : ControllerBase
    {
        private IGenericRepository<InfectionControl> _infectionRepository;


        public InfectionControlController(IGenericRepository<InfectionControl> infectionRepository)
        {
            _infectionRepository = infectionRepository;
        }
        #region CarePlanHygiene
        /// <summary>
        /// Get All CarePlanHygiene
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetInfectionControl>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _infectionRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create CarePlanHygiene
        /// </summary>
        /// <param name="postCarePlanHygiene"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostInfectionControl postCarePlanHygiene)
        {
            if (postCarePlanHygiene == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CarePlanHygiene = Mapper.Map<InfectionControl>(postCarePlanHygiene);
            await _infectionRepository.InsertEntity(CarePlanHygiene);
            return Ok();
        }
        /// <summary>
        /// Update CarePlanHygiene
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostInfectionControl models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CarePlanHygiene = Mapper.Map<InfectionControl>(models);
            await _infectionRepository.UpdateEntity(CarePlanHygiene);
            return Ok();

        }
        /// <summary>
        /// Get CarePlanHygiene by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetInfectionControl), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHygiene = await (from c in _infectionRepository.Table
                                           where c.ClientId == id.Value
                                           select new GetInfectionControl
                                           {
                                               ClientId = c.ClientId,
                                               Status = c.Status,
                                               InfectionId = c.InfectionId,
                                               Guideline = c.Guideline,
                                               VaccStatus = c.VaccStatus,
                                               Type = c.Type,
                                               TestDate = c.TestDate,
                                               Remarks = c.Remarks,
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHygiene);
        }
        #endregion
    }
}
