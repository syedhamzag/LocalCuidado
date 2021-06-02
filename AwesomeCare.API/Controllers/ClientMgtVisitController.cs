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
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientMgtVisit
        /// </summary>
        /// <param name="postClientMgtVisit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientMgtVisit postClientMgtVisit)
        {
            if (postClientMgtVisit == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientMgtVisit = Mapper.Map<ClientMgtVisit>(postClientMgtVisit);
            var newClientMgtVisit = await _clientMgtVisitRepository.InsertEntity(ClientMgtVisit);
            var getClientMgtVisit = Mapper.Map<GetClientMgtVisit>(newClientMgtVisit);
            return Ok(getClientMgtVisit);


        }
        /// <summary>
        /// Update ClientMgtVisit
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientMgtVisit), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutClientMgtVisit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _clientMgtVisitRepository.GetEntity(model.VisitId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _clientMgtVisitRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetClientMgtVisit>(updateEntity);
            return Ok(getEntity);

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