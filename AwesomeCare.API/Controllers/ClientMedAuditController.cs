using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationAudit;
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
    public class ClientMedAuditController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientMedAudit> _clientMedAuditRepository;
        
        public ClientMedAuditController(AwesomeCareDbContext dbContext, IGenericRepository<ClientMedAudit> clientMedAuditRepository, IGenericRepository<Client> clientRepository)
        {
            _clientMedAuditRepository = clientMedAuditRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
        }
        #region ClientMedAudit
        /// <summary>
        /// Get All ClientMedAudit
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientMedAudit>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientMedAuditRepository.Table.ToList();
            return Ok(getEntities.Distinct().ToList());
        }
        /// <summary>
        /// Get All ClientMedAudit
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRef/{Reference}")]
        [ProducesResponseType(type: typeof(List<GetClientMedAudit>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRef(string Reference)
        {
            var getEntities = _clientMedAuditRepository.Table.Where(s => s.Reference == Reference).ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientMedAudit
        /// </summary>
        /// <param name="postClientMedAudit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostClientMedAudit> postClientMedAudit)
        {
            if (postClientMedAudit == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in postClientMedAudit)
            {
                if (item.EvidenceOfActionTaken == null)
                    item.EvidenceOfActionTaken = "No Image";
                if (item.Attachment == null)
                    item.Attachment = "No Image";
            }

            var ClientMedAudit = Mapper.Map<List<ClientMedAudit>>(postClientMedAudit);
            await _clientMedAuditRepository.InsertEntities(ClientMedAudit);
            return Ok();
        }
        /// <summary>
        /// Update ClientMedAudit
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutClientMedAudit> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Entity = _dbContext.Set<ClientMedAudit>();
            var filterEntity = Entity.Where(c => c.Reference == model.FirstOrDefault().Reference);
            foreach (ClientMedAudit item in filterEntity)
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
                    var postEntity = Mapper.Map<ClientMedAudit>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
            return Ok();

        }

        /// <summary>
        /// Get ClientMedAudit by MedAuditId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientMedAudit), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientMedAudit = await (from c in _clientMedAuditRepository.Table
                                           where c.MedAuditId == id
                                           select new GetClientMedAudit
                                           {
                                               ClientId = c.ClientId,
                                               ActionRecommended = c.ActionRecommended,
                                               ActionTaken = c.ActionTaken,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextDueDate = c.NextDueDate,
                                               Deadline = c.Deadline,
                                               EvidenceOfActionTaken = c.EvidenceOfActionTaken,
                                               GapsInAdmistration = c.GapsInAdmistration,
                                               HardCopyReview = c.HardCopyReview,
                                               LessonLearntAndShared = c.LessonLearntAndShared,
                                               LogURL = c.LogURL,
                                               MarChartReview = c.MarChartReview,
                                               MedicationConcern = c.MedicationConcern,
                                               MedicationInfoUploadEefficiency = c.MedicationInfoUploadEefficiency,
                                               MedicationSupplyEfficiency = c.MedicationSupplyEfficiency,
                                               NameOfAuditor = c.NameOfAuditor,
                                               Observations = c.Observations,
                                               OfficerToTakeAction = c.OfficerToTakeAction,
                                               Remarks = c.Remarks,
                                               RepeatOfIncident = c.RepeatOfIncident,
                                               RightsOfMedication = c.RightsOfMedication,
                                               RotCause = c.RotCause,
                                               Status = c.Status,
                                               ThinkingServiceUsers = c.ThinkingServiceUsers,
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientMedAudit);
        }
        #endregion
    }
}