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
            return Ok(getEntities.Distinct().ToList());
        }

        /// <summary>
        /// Get All ClientProgram
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRef/{Reference}")]
        [ProducesResponseType(type: typeof(List<GetClientProgram>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRef(string Reference)
        {
            var getEntities = _clientProgramRepository.Table.Where(s => s.Reference == Reference).ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientProgram
        /// </summary>
        /// <param name="postClientProgram"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostClientProgram> postClientProgram)
        {
            if (postClientProgram == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in postClientProgram)
            {
                if (item.Attachment == null)
                    item.Attachment = "No Image";
            }

            var ClientProgram = Mapper.Map<List<ClientProgram>>(postClientProgram);
            await _clientProgramRepository.InsertEntities(ClientProgram);
            return Ok();
        }
        /// <summary>
        /// Update ClientProgram
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutClientProgram> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Entity = _dbContext.Set<ClientProgram>();
            var filterEntity = Entity.Where(c => c.Reference == model.FirstOrDefault().Reference);
            foreach (ClientProgram item in filterEntity)
            {
                var modelRecord = model.Select(s => s).Where(s => s.OfficerToAct == item.OfficerToAct).FirstOrDefault();
                if (modelRecord == null)
                {
                    _dbContext.Entry(item).State = EntityState.Deleted;

                }
                else
                {
                    var putEntity = Mapper.Map(modelRecord, item);
                    _dbContext.Entry(putEntity).State = EntityState.Modified;
                }

            }
            //Model not in Database
            foreach (var item in model)
            {
                var NotInDb = filterEntity.FirstOrDefault(r => r.OfficerToAct == item.OfficerToAct);
                if (NotInDb == null)
                {
                    var postEntity = Mapper.Map<ClientProgram>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
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
                                               OfficerToAct = c.OfficerToAct,
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