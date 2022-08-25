using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Health.HistoryOfFall;
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
    public class HistoryOfFallController : ControllerBase
    {
        private IGenericRepository<HistoryOfFall> _HistoryOfFallRepository;


        public HistoryOfFallController(IGenericRepository<HistoryOfFall> HistoryOfFallRepository)
        {
            _HistoryOfFallRepository = HistoryOfFallRepository;

        }
        #region CarePlanHealth
        /// <summary>
        /// Get All CarePlanHealth
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetHistoryOfFall>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _HistoryOfFallRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create CarePlanHealth
        /// </summary>
        /// <param name="postCarePlanHealth"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostHistoryOfFall postCarePlanHealth)
        {
            if (postCarePlanHealth == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CarePlanHealth = Mapper.Map<HistoryOfFall>(postCarePlanHealth);
            await _HistoryOfFallRepository.InsertEntity(CarePlanHealth);
            return Ok();
        }
        /// <summary>
        /// Update CarePlanHealth
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostHistoryOfFall models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CarePlanHealth = Mapper.Map<HistoryOfFall>(models);
            await _HistoryOfFallRepository.UpdateEntity(CarePlanHealth);
            return Ok();

        }
        /// <summary>
        /// Get CarePlanHealth by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetHistoryOfFall), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHealth = await (from c in _HistoryOfFallRepository.Table
                                           where c.HistoryId == id.Value
                                           select new GetHistoryOfFall
                                           {
                                               Cause = c.Cause,
                                               Details = c.Details,
                                               Date = c.Date,
                                               ClientId = c.ClientId,
                                               HistoryId = c.HistoryId,
                                               Prevention = c.Prevention
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHealth);
        }
        /// <summary>
        /// Get CarePlanHealth by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetbyClient/{id}")]
        [ProducesResponseType(type: typeof(GetHistoryOfFall), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHealth = await (from c in _HistoryOfFallRepository.Table
                                           where c.ClientId == id.Value
                                           select new GetHistoryOfFall
                                           {
                                               Cause = c.Cause,
                                               Details = c.Details,
                                               Date = c.Date,
                                               ClientId = c.ClientId,
                                               HistoryId = c.HistoryId,
                                               Prevention = c.Prevention
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHealth);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _HistoryOfFallRepository.GetEntity(id);
            await _HistoryOfFallRepository.DeleteEntity(entity);
            return Ok();
        }
        #endregion
    }
}
