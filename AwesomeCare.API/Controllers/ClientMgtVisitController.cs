using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientMgtVisit;
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
    public class ClientMgtVisitController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientMgtVisit> _clientMgtVisitRepository;
        
        public ClientMgtVisitController(AwesomeCareDbContext dbContext, IGenericRepository<ClientMgtVisit> clientMgtVisitRepository)
        {
            _clientMgtVisitRepository = clientMgtVisitRepository;
            _dbContext = dbContext;
        }
        #region ClientMgtVisit
        /// <summary>
        /// Get All ClientMgtVisit
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientMgtVisit>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientMgtVisitRepository.Table.ToList();
            return Ok(getEntities.Distinct().ToList());
        }

        /// <summary>
        /// Get All ClientMgtVisit
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRef/{Reference}")]
        [ProducesResponseType(type: typeof(List<GetClientMgtVisit>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRef(string Reference)
        {
            var getEntities = _clientMgtVisitRepository.Table.Where(s => s.Reference == Reference).ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientMgtVisit
        /// </summary>
        /// <param name="postClientMgtVisit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostClientMgtVisit> postClientMgtVisit)
        {
            if (postClientMgtVisit == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in postClientMgtVisit)
            {
                if (item.Attachment == null)
                    item.Attachment = "No Image";
                if (item.EvidenceOfActionTaken == null)
                    item.EvidenceOfActionTaken = "No Image";
            }

            var ClientMgtVisit = Mapper.Map<List<ClientMgtVisit>>(postClientMgtVisit);
            await _clientMgtVisitRepository.InsertEntities(ClientMgtVisit);
            return Ok();
        }
        /// <summary>
        /// Update ClientMgtVisit
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutClientMgtVisit> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Entity = _dbContext.Set<ClientMgtVisit>();
            var filterEntity = Entity.Where(c => c.Reference == model.FirstOrDefault().Reference);
            foreach (ClientMgtVisit item in filterEntity)
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
                    var postEntity = Mapper.Map<ClientMgtVisit>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
            return Ok();

        }

        /// <summary>
        /// Get ClientMgtVisit by MgtVisitId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientMgtVisit), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientMgtVisit = await (from c in _clientMgtVisitRepository.Table
                                           where c.VisitId == id
                                           select new GetClientMgtVisit
                                           {
                                               ClientId = c.ClientId,
                                               ActionRequired = c.ActionRequired,
                                               ActionsTakenByMPCC = c.ActionsTakenByMPCC,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Deadline = c.Deadline,
                                               EvidenceOfActionTaken = c.EvidenceOfActionTaken,
                                               LessonLearntAndShared = c.LessonLearntAndShared,
                                               Remarks = c.Remarks,
                                               RotCause = c.RotCause,
                                               Status = c.Status,
                                               HowToComplain = c.HowToComplain,
                                               ImprovementExpect = c.ImprovementExpect,
                                               Observation = c.Observation,
                                               OfficerToAct = c.OfficerToAct,
                                               RateManagers = c.RateManagers,
                                               RateServiceRecieving = c.RateServiceRecieving,
                                               ServiceRecommended = c.ServiceRecommended,
                                               StaffBestSupport = c.StaffBestSupport,
                                               URL = c.URL
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientMgtVisit);
        }
        #endregion
    }
}