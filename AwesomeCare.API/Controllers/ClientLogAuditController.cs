using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientLogAudit;
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
    public class ClientLogAuditController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientLogAudit> _clientLogAuditRepository;

        public ClientLogAuditController(AwesomeCareDbContext dbContext, IGenericRepository<ClientLogAudit> clientLogAuditRepository, IGenericRepository<Client> clientRepository)
        {
            _clientLogAuditRepository = clientLogAuditRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
        }
        #region ClientLogAudit
        /// <summary>
        /// Get All ClientLogAudit
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientLogAudit>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientLogAuditRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientLogAudit
        /// </summary>
        /// <param name="postClientLogAudit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostClientLogAudit> postClientLogAudit)
        {
            if (postClientLogAudit == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientLogAudit = Mapper.Map<List<ClientLogAudit>>(postClientLogAudit);
            await _clientLogAuditRepository.InsertEntities(ClientLogAudit);
            return Ok();
        }
        /// <summary>
        /// Update ClientLogAudit
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientLogAudit), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutClientLogAudit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _clientLogAuditRepository.GetEntity(model.LogAuditId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _clientLogAuditRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetClientLogAudit>(updateEntity);

            return Ok(getEntity);

        }
        /// <summary>
        /// Get ClientLogAudit by LogAuditId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientLogAudit), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientLogAudit = await (from c in _clientLogAuditRepository.Table
                                           where c.LogAuditId == id.Value
                                           select new GetClientLogAudit
                                           {
                                               ClientId = c.ClientId,
                                               ActionRecommended = c.ActionRecommended,
                                               ActionTaken = c.ActionTaken,
                                               EvidenceFilePath = c.EvidenceFilePath,
                                               Date = c.Date,
                                               NextDueDate = c.NextDueDate,
                                               Deadline = c.Deadline,
                                               EvidenceOfActionTaken = c.EvidenceOfActionTaken,
                                               LessonLearntAndShared = c.LessonLearntAndShared,
                                               LogURL = c.LogURL,
                                               NameOfAuditor = c.NameOfAuditor,
                                               Observations = c.Observations,
                                               OfficerToTakeAction = c.OfficerToTakeAction,
                                               Remarks = c.Remarks,
                                               RepeatOfIncident = c.RepeatOfIncident,
                                               RotCause = c.RotCause,
                                               Status = c.Status,
                                               ThinkingServiceUsers = c.ThinkingServiceUsers,
                                               Communication = c.Communication,
                                               ImproperDocumentation = c.ImproperDocumentation,
                                               IsCareDifference = c.IsCareDifference,
                                               IsCareExpected = c.IsCareExpected,
                                               ProperDocumentation = c.ProperDocumentation,
                                               ThinkingStaff = c.ThinkingStaff,
                                               ThinkingStaffStop = c.ThinkingStaffStop
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientLogAudit);
        }
        #endregion

    }
}