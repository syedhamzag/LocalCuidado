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
            return Ok(getEntities.Distinct().ToList());
        }
        /// <summary>
        /// Get All ClientLogAudit
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRef/{Reference}")]
        [ProducesResponseType(type: typeof(List<GetClientLogAudit>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRef(string Reference)
        {
            var getEntities = _clientLogAuditRepository.Table.Where(s=>s.Reference==Reference).ToList();
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
            foreach (var item in postClientLogAudit)
            {
                if (item.EvidenceFilePath == null)
                    item.EvidenceFilePath = "No Image";
                if (item.EvidenceOfActionTaken == null)
                    item.EvidenceOfActionTaken = "No Image";
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
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutClientLogAudit> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Entity = _dbContext.Set<ClientLogAudit>();
            var filterEntity = Entity.Where(c => c.Reference == model.FirstOrDefault().Reference);
            foreach (ClientLogAudit item in filterEntity)
            {
                var modelRecord = model.Select(s => s).Where(s => s.OfficerToTakeAction == item.OfficerToTakeAction).FirstOrDefault();
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
                var NotInDb = filterEntity.FirstOrDefault(r => r.OfficerToTakeAction == item.OfficerToTakeAction);
                if (NotInDb == null)
                {
                    var postEntity = Mapper.Map<ClientLogAudit>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
            return Ok();

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
                                               LogAuditId = c.LogAuditId,
                                               Reference = c.Reference,
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