using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.QueryableExtensions;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConsentCareController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ConsentCare> _ConsentCareRepository;

        public ConsentCareController(AwesomeCareDbContext dbContext, IGenericRepository<ConsentCare> ConsentCareRepository)
        {
            _ConsentCareRepository = ConsentCareRepository;
            _dbContext = dbContext;
        }
        #region ConsentCare
        /// <summary>
        /// Get All ConsentCare
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetConsentCare>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ConsentCareRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ConsentCare
        /// </summary>
        /// <param name="postConsentCare"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostConsentCare postConsentCare)
        {
            if (postConsentCare == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsentCare = Mapper.Map<ConsentCare>(postConsentCare);
            await _ConsentCareRepository.InsertEntity(ConsentCare);
            return Ok();
        }
        /// <summary>
        /// Update ConsentCare
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostConsentCare models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ConsentCare = Mapper.Map<ConsentCare>(models);
            await _ConsentCareRepository.UpdateEntity(ConsentCare);
            return Ok();

        }
        /// <summary>
        /// Get ConsentCare by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetConsentCare), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getConsentCare = await (from c in _ConsentCareRepository.Table
                                        where c.PersonalDetailId == id.Value
                                        select new GetConsentCare
                                           {
                                               CareId = c.CareId,
                                               PersonalDetailId = c.PersonalDetailId,
                                               Date = c.Date,
                                               Signature = c.Signature,
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getConsentCare);
        }
        #endregion
    }
}