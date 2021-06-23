using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientServiceWatch;
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
    public class ClientServiceWatchController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientServiceWatch> _clientServiceWatchRepository;
        
        public ClientServiceWatchController(AwesomeCareDbContext dbContext, IGenericRepository<ClientServiceWatch> clientServiceWatchRepository)
        {
            _clientServiceWatchRepository = clientServiceWatchRepository;
            _dbContext = dbContext;
        }
        #region ClientServiceWatch
        /// <summary>
        /// Get All ClientServiceWatch
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientServiceWatch>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientServiceWatchRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientServiceWatch
        /// </summary>
        /// <param name="postClientServiceWatch"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientServiceWatch postClientServiceWatch)
        {
            if (postClientServiceWatch == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ClientServiceWatch = Mapper.Map<ClientServiceWatch>(postClientServiceWatch);
            await _clientServiceWatchRepository.InsertEntity(ClientServiceWatch);
            return Ok();
        }
        /// <summary>
        /// Update ClientServiceWatch
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientServiceWatch model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientServiceWatch = Mapper.Map<ClientServiceWatch>(model);
            await _clientServiceWatchRepository.UpdateEntity(ClientServiceWatch);
            return Ok();

        }

        /// <summary>
        /// Get ClientServiceWatch by ServiceWatchId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientServiceWatch), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientServiceWatch = await (from c in _clientServiceWatchRepository.Table
                                           where c.WatchId == id
                                           select new GetClientServiceWatch
                                           {
                                               ClientId = c.ClientId,
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Deadline = c.Deadline,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               URL = c.URL,
                                               Observation = c.Observation,
                                               Incident = c.Incident,
                                               Contact = c.Contact,
                                               Details = c.Details
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientServiceWatch);
        }
        #endregion
    }
}