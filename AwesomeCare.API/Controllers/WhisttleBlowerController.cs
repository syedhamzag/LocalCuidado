using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.WhisttleBlower;
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
    public class WhisttleBlowerController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<WhisttleBlower> _WhisttleBlowerRepository;
        
        public WhisttleBlowerController(AwesomeCareDbContext dbContext, IGenericRepository<WhisttleBlower> WhisttleBlowerRepository)
        {
            _WhisttleBlowerRepository = WhisttleBlowerRepository;
            _dbContext = dbContext;
        }
        #region WhisttleBlower
        /// <summary>
        /// Get All WhisttleBlower
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetWhisttleBlower>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _WhisttleBlowerRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create WhisttleBlower
        /// </summary>
        /// <param name="postWhisttleBlower"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostWhisttleBlower postWhisttleBlower)
        {
            if (postWhisttleBlower == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var WhisttleBlower = Mapper.Map<WhisttleBlower>(postWhisttleBlower);
            var newWhisttleBlower = await _WhisttleBlowerRepository.InsertEntity(WhisttleBlower);
            var getWhisttleBlower = Mapper.Map<GetWhisttleBlower>(newWhisttleBlower);
            return Ok(getWhisttleBlower);


        }
        /// <summary>
        /// Update WhisttleBlower
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetWhisttleBlower), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutWhisttleBlower model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _WhisttleBlowerRepository.GetEntity(model.WhisttleBlowerId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _WhisttleBlowerRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetWhisttleBlower>(updateEntity);
            return Ok(getEntity);

        }
        /// <summary>
        /// Get WhisttleBlower by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetWhisttleBlower), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getWhisttleBlower = await (from c in _WhisttleBlowerRepository.Table
                                           where c.WhisttleBlowerId == id
                                           select new GetWhisttleBlower
                                           {
                                               WhisttleBlowerId = c.WhisttleBlowerId,
                                               Date = c.Date,
                                               UserName = c.UserName,
                                               StaffName = c.StaffName,
                                               IncidentDate = c.IncidentDate,
                                               Happening = c.Happening,
                                               Evidence = c.Evidence,
                                               Witness = c.Witness,
                                               LikeCalling = c.LikeCalling,
                                               Status = c.Status
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getWhisttleBlower);
        }
        #endregion
    }
}