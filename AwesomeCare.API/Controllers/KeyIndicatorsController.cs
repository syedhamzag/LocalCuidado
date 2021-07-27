using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators;
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
    public class KeyIndicatorsController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<KeyIndicators> _KeyIndicatorsRepository;

        public KeyIndicatorsController(AwesomeCareDbContext dbContext, IGenericRepository<KeyIndicators> KeyIndicatorsRepository)
        {
            _KeyIndicatorsRepository = KeyIndicatorsRepository;
            _dbContext = dbContext;
        }
        #region KeyIndicators
        /// <summary>
        /// Get All KeyIndicators
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetKeyIndicators>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _KeyIndicatorsRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create KeyIndicators
        /// </summary>
        /// <param name="postKeyIndicators"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostKeyIndicators postKeyIndicators)
        {
            if (postKeyIndicators == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var KeyIndicators = Mapper.Map<KeyIndicators>(postKeyIndicators);
            await _KeyIndicatorsRepository.InsertEntity(KeyIndicators);
            return Ok();
        }
        /// <summary>
        /// Update KeyIndicators
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutKeyIndicators models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var KeyIndicators = Mapper.Map<KeyIndicators>(models);
            await _KeyIndicatorsRepository.UpdateEntity(KeyIndicators);
            return Ok();

        }
        /// <summary>
        /// Get KeyIndicators by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetKeyIndicators), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getKeyIndicators = await (from c in _KeyIndicatorsRepository.Table
                                           where c.KeyId == id
                                           select new GetKeyIndicators
                                           {
                                               KeyId = c.KeyId,
                                               ClientId = c.ClientId,
                                               AboutMe = c.AboutMe,
                                               FamilyRole = c.FamilyRole,
                                               Debture = c.Debture,
                                               LivingStatus = c.LivingStatus,
                                               LogMethod = c.LogMethod,
                                               ThingsILike = c.ThingsILike
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getKeyIndicators);
        }
        #endregion
    }
}