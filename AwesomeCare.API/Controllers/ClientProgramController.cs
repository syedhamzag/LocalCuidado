using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientProgram;
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
    public class ClientProgramController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientProgram> _clientProgramRepository;
        
        public ClientProgramController(AwesomeCareDbContext dbContext, IGenericRepository<ClientProgram> clientProgramRepository)
        {
            _clientProgramRepository = clientProgramRepository;
            _dbContext = dbContext;
        }
        #region ClientProgram
        /// <summary>
        /// Get All ClientProgram
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientProgram>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientProgramRepository.Table.ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create ClientProgram
        /// </summary>
        /// <param name="postClientProgram"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientProgram postClientProgram)
        {
            if (postClientProgram == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ClientProgram = Mapper.Map<ClientProgram>(postClientProgram);
            await _clientProgramRepository.InsertEntity(ClientProgram);
            return Ok();
        }
        /// <summary>
        /// Update ClientProgram
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientProgram model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientProgram = Mapper.Map<ClientProgram>(model);
            await _clientProgramRepository.UpdateEntity(ClientProgram);
            return Ok();

        }

        /// <summary>
        /// Get ClientProgram by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientProgram), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientProgram = await (from c in _clientProgramRepository.Table
                                           where c.ProgramId == id
                                           select new GetClientProgram
                                           {
                                               ClientId = c.ClientId,
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               DaysOfChoice = c.DaysOfChoice,
                                               Deadline = c.Deadline,
                                               DetailsOfProgram = c.DetailsOfProgram,
                                               Observation = c.Observation,
                                               PlaceLocationProgram = c.PlaceLocationProgram,
                                               ProgramOfChoice = c.ProgramOfChoice,
                                               URL = c.URL
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientProgram);
        }
        #endregion
    }
}