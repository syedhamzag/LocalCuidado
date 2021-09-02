using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.PersonalHygiene;
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
    public class PersonalHygieneController : ControllerBase
    {
        private IGenericRepository<PersonalHygiene> _phygieneRepository;


        public PersonalHygieneController(IGenericRepository<PersonalHygiene> PersonalHygieneRepository)
        {
            _phygieneRepository = PersonalHygieneRepository;
        }
        #region CarePlanHygiene
        /// <summary>
        /// Get All CarePlanHygiene
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetPersonalHygiene>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _phygieneRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create CarePlanHygiene
        /// </summary>
        /// <param name="postCarePlanHygiene"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostPersonalHygiene postCarePlanHygiene)
        {
            if (postCarePlanHygiene == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CarePlanHygiene = Mapper.Map<PersonalHygiene>(postCarePlanHygiene);
            await _phygieneRepository.InsertEntity(CarePlanHygiene);
            return Ok();
        }
        /// <summary>
        /// Update CarePlanHygiene
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostPersonalHygiene models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CarePlanHygiene = Mapper.Map<PersonalHygiene>(models);
            await _phygieneRepository.UpdateEntity(CarePlanHygiene);
            return Ok();

        }
        /// <summary>
        /// Get CarePlanHygiene by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetPersonalHygiene), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHygiene = await (from c in _phygieneRepository.Table
                                           where c.HygieneId == id.Value
                                           select new GetPersonalHygiene
                                           {
                                               HygieneId = c.HygieneId,
                                               Cleaning = c.Cleaning,
                                               ClientId = c.ClientId,
                                               CleaningFreq = c.CleaningFreq,
                                               WhoClean = c.WhoClean,
                                               WashingMachine = c.WashingMachine,
                                               LaundrySupport = c.LaundrySupport,
                                               LaundryGuide = c.LaundryGuide,
                                               Ironing = c.Ironing,
                                               DryLaundry = c.DryLaundry,
                                               CleaningTools = c.CleaningTools,
                                               DirtyLaundry = c.DirtyLaundry,
                                               GeneralAppliance = c.GeneralAppliance,
                                               DesiredCleaning = c.DesiredCleaning,
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHygiene);
        }
        #endregion
    }
}
