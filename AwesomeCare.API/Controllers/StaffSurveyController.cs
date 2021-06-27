using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffSurvey;
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
    public class StaffSurveyController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffSurvey> _StaffSurveyRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<SurveyOfficerToAct> _officertoactRepository;
        private IGenericRepository<SurveyWorkteam> _workteamRepository;

        public StaffSurveyController(AwesomeCareDbContext dbContext, IGenericRepository<StaffSurvey> StaffSurveyRepository,
            IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<SurveyOfficerToAct> officertoactRepository, IGenericRepository<SurveyWorkteam> workteamRepository)
        {
            _StaffSurveyRepository = StaffSurveyRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffRepository = staffRepository;
            _workteamRepository = workteamRepository;
        }
        #region StaffSurvey
        /// <summary>
        /// Get All StaffSurvey
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffSurvey>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffSurveyRepository.Table.ToList();      
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffSurvey
        /// </summary>
        /// <param name="postStaffSurvey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffSurvey postStaffSurvey)
        {
            if (postStaffSurvey == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StaffSurvey = Mapper.Map<StaffSurvey>(postStaffSurvey);
            await _StaffSurveyRepository.InsertEntity(StaffSurvey);
            
            return Ok();


        }
        /// <summary>
        /// Update StaffSurvey
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffSurvey models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<SurveyOfficerToAct>();
                var filterentity = entity.Where(c => c.StaffSurveyId == model.StaffSurveyId && c.StaffPersonalInfoId == model.StaffPersonalInfoId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            foreach (var model in models.Workteam.ToList())
            {
                var entity = _dbContext.Set<SurveyWorkteam>();
                var filterentity = entity.Where(c => c.StaffSurveyId == model.StaffSurveyId && c.StaffPersonalInfoId == model.StaffPersonalInfoId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();
            var StaffSurvey = Mapper.Map<StaffSurvey>(models);
            await _StaffSurveyRepository.UpdateEntity(StaffSurvey);
            return Ok();
        }
        /// <summary>
        /// Get StaffSurvey by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffSurvey), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getStaffSurvey = await (from c in _StaffSurveyRepository.Table
                                           where c.StaffSurveyId == id
                                           select new GetStaffSurvey
                                           {
                                               StaffSurveyId = c.StaffSurveyId,
                                               Reference = c.Reference,
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               StaffId = c.StaffId,
                                               Details = c.Details,
                                               HealthCareServicesSatisfaction = c.HealthCareServicesSatisfaction,
                                               CompanyManagement = c.CompanyManagement,
                                               Deadline = c.Deadline,
                                               AdequateTrainingReceived = c.AdequateTrainingReceived,
                                               AccessToPolicies = c.AccessToPolicies,
                                               AreaRequiringImprovements = c.AreaRequiringImprovements,
                                               SupportFromCompany = c.SupportFromCompany,
                                               WorkEnvironmentSuggestions = c.WorkEnvironmentSuggestions,
                                               URL = c.URL,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.StaffSurveyId == c.StaffSurveyId
                                                               select new GetSurveyOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               Workteam = (from com in _workteamRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.StaffSurveyId == c.StaffSurveyId
                                                               select new GetSurveyWorkteam
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffSurvey);
        }
        #endregion
    }
}