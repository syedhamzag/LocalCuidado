using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthCondition;
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
    public class SpecialHealthConditionController : ControllerBase
    {
        private IGenericRepository<SpecialHealthCondition> _spmedsRepository;


        public SpecialHealthConditionController(IGenericRepository<SpecialHealthCondition> spmedsRepository)
        {
            _spmedsRepository = spmedsRepository;

        }
        #region CarePlanHealth
        /// <summary>
        /// Get All CarePlanHealth
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetSpecialHealthCondition>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _spmedsRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create CarePlanHealth
        /// </summary>
        /// <param name="postCarePlanHealth"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostSpecialHealthCondition postCarePlanHealth)
        {
            if (postCarePlanHealth == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CarePlanHealth = Mapper.Map<SpecialHealthCondition>(postCarePlanHealth);
            await _spmedsRepository.InsertEntity(CarePlanHealth);
            return Ok();
        }
        /// <summary>
        /// Update CarePlanHealth
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostSpecialHealthCondition models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CarePlanHealth = Mapper.Map<SpecialHealthCondition>(models);
            await _spmedsRepository.UpdateEntity(CarePlanHealth);
            return Ok();

        }
        /// <summary>
        /// Get CarePlanHealth by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetSpecialHealthCondition), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHealth = await (from c in _spmedsRepository.Table
                                           where c.ClientId == id.Value
                                           select new GetSpecialHealthCondition
                                           {
                                               ClientAction = c.ClientAction,
                                               ClinicRecommendation = c.ClinicRecommendation,
                                               ConditionName = c.ConditionName,
                                               ClientId = c.ClientId,
                                               FeelingAfterIncident = c.FeelingAfterIncident,
                                               FeelingBeforeIncident = c.FeelingBeforeIncident,
                                               Frequency = c.Frequency,
                                               HealthCondId = c.HealthCondId,
                                               LifestyleSupport = c.LifestyleSupport,
                                               LivingActivities = c.LivingActivities,
                                               PlanningHealthCondition = c.PlanningHealthCondition,
                                               SourceInformation = c.SourceInformation,
                                               Trigger = c.Trigger
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHealth);
        }
        #endregion
    }
}
