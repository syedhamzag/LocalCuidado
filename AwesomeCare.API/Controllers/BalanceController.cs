using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Health.Balance;
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
    public class BalanceController : ControllerBase
    {
        private IGenericRepository<Balance> _balanceRepository;


        public BalanceController(IGenericRepository<Balance> balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }
        #region CarePlanHealth
        /// <summary>
        /// Get All CarePlanHealth
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetBalance>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _balanceRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create CarePlanHealth
        /// </summary>
        /// <param name="postCarePlanHealth"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostBalance postCarePlanHealth)
        {
            if (postCarePlanHealth == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CarePlanHealth = Mapper.Map<Balance>(postCarePlanHealth);
            await _balanceRepository.InsertEntity(CarePlanHealth);
            return Ok();
        }
        /// <summary>
        /// Update CarePlanHealth
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostBalance models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CarePlanHealth = Mapper.Map<Balance>(models);
            await _balanceRepository.UpdateEntity(CarePlanHealth);
            return Ok();

        }
        /// <summary>
        /// Get CarePlanHealth by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetBalance), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHealth = await (from c in _balanceRepository.Table
                                           where c.ClientId == id.Value
                                           select new GetBalance
                                           {
                                               BalanceId = c.BalanceId,
                                               Description = c.Description,
                                               ClientId = c.ClientId,
                                               Mobility = c.Mobility,
                                               Name = c.Name,
                                               Status = c.Status

                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHealth);
        }
        #endregion
    }
}
