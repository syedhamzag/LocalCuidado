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
using AwesomeCare.DataTransferObject.DTOs.ClientVisit;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientMgtVisitController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientMgtVisit> _clientMgtVisitRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<VisitOfficerToAct> _officertoactRepository;
        private IGenericRepository<VisitStaffName> _staffnameRepository;

        public ClientMgtVisitController(AwesomeCareDbContext dbContext, IGenericRepository<ClientMgtVisit> clientMgtVisitRepository,
            IGenericRepository<StaffPersonalInfo> staffRepository,
        IGenericRepository<VisitOfficerToAct> officertoactRepository,
        IGenericRepository<VisitStaffName> staffnameRepository)
        {
            _clientMgtVisitRepository = clientMgtVisitRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _staffRepository = staffRepository;
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
            await _clientMgtVisitRepository.InsertEntity(ClientMgtVisit);
            return Ok();
        }
        /// <summary>
        /// Update ClientMgtVisit
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientMgtVisit models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<VisitOfficerToAct>();
                var filterentity = entity.Where(c => c.VisitId == model.VisitId && c.StaffPersonalInfoId == model.StaffPersonalInfoId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            foreach (var model in models.StaffName.ToList())
            {
                var entity = _dbContext.Set<VisitStaffName>();
                var filterentity = entity.Where(c => c.VisitId == model.VisitId && c.StaffPersonalInfoId == model.StaffPersonalInfoId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();

            var ClientMgtVisit = Mapper.Map<ClientMgtVisit>(models);
            await _clientMgtVisitRepository.UpdateEntity(ClientMgtVisit);
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
                                               VisitId = c.VisitId,
                                               Reference = c.Reference,
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
                                               RateManagers = c.RateManagers,
                                               RateServiceRecieving = c.RateServiceRecieving,
                                               ServiceRecommended = c.ServiceRecommended,
                                               URL = c.URL,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.VisitId == c.VisitId
                                                               select new GetVisitOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.VisitId == c.VisitId
                                                            select new GetVisitStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientMgtVisit);
        }
        #endregion
    }
}