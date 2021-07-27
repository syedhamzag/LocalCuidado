using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData;
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
    public class ConsentDataController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ConsentData> _ConsentDataRepository;

        public ConsentDataController(AwesomeCareDbContext dbContext, IGenericRepository<ConsentData> ConsentDataRepository)
        {
            _ConsentDataRepository = ConsentDataRepository;
            _dbContext = dbContext;
        }
        #region ConsentData
        /// <summary>
        /// Get All ConsentData
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetConsentData>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ConsentDataRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ConsentData
        /// </summary>
        /// <param name="postConsentData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostConsentData postConsentData)
        {
            if (postConsentData == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ConsentData = Mapper.Map<ConsentData>(postConsentData);
            await _ConsentDataRepository.InsertEntity(ConsentData);
            return Ok();
        }
        /// <summary>
        /// Update ConsentData
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutConsentData models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ConsentData = Mapper.Map<ConsentData>(models);
            await _ConsentDataRepository.UpdateEntity(ConsentData);
            return Ok();

        }
        /// <summary>
        /// Get ConsentData by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetConsentData), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getConsentData = await (from c in _ConsentDataRepository.Table
                                           where c.DataId == id
                                           select new GetConsentData
                                           {
                                               DataId = c.DataId,
                                               ClientId = c.ClientId,
                                               Date = c.Date,
                                               Signature = c.Signature,
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getConsentData);
        }
        #endregion
    }
}