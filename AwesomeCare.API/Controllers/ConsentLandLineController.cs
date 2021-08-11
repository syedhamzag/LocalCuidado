using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.QueryableExtensions;

namespace AwesomeCare.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConsentLandLineController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ConsentLandLine> _ConsentLandLineRepository;

        public ConsentLandLineController(AwesomeCareDbContext dbContext, IGenericRepository<ConsentLandLine> ConsentLandLineRepository)
        {
            _ConsentLandLineRepository = ConsentLandLineRepository;
            _dbContext = dbContext;
        }
        #region ConsentLandLine
        /// <summary>
        /// Get All ConsentLandLine
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetConsentLandLine>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ConsentLandLineRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ConsentLandLine
        /// </summary>
        /// <param name="postConsentLandLine"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostConsentLandLine postConsentLandLine)
        {
            if (postConsentLandLine == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsentLandLine = Mapper.Map<ConsentLandLine>(postConsentLandLine);
            await _ConsentLandLineRepository.InsertEntity(ConsentLandLine);
            return Ok();
        }
        /// <summary>
        /// Update ConsentLandLine
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostConsentLandLine models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ConsentLandLine = Mapper.Map<ConsentLandLine>(models);
            await _ConsentLandLineRepository.UpdateEntity(ConsentLandLine);
            return Ok();

        }
        /// <summary>
        /// Get ConsentLandLine by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetConsentLandLine), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getConsentLandLine = await (from c in _ConsentLandLineRepository.Table
                                            where c.PersonalDetailId == id.Value
                                            select new GetConsentLandLine
                                           {
                                               LandlineId = c.LandlineId,
                                               PersonalDetailId = c.PersonalDetailId,
                                               Date = c.Date,
                                               LogMethod = c.LogMethod,
                                               Signature = c.Signature,
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getConsentLandLine);
        }
        #endregion
    }
}