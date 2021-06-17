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
        
        public StaffSurveyController(AwesomeCareDbContext dbContext, IGenericRepository<StaffSurvey> StaffSurveyRepository)
        {
            _StaffSurveyRepository = StaffSurveyRepository;
            _dbContext = dbContext;
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
            return Ok(getEntities.Distinct().ToList());
        }
        /// <summary>
        /// Get All Survey
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRef/{Reference}")]
        [ProducesResponseType(type: typeof(List<GetStaffSurvey>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRef(string Reference)
        {
            var getEntities = _StaffSurveyRepository.Table.Where(s => s.Reference == Reference).ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffSurvey
        /// </summary>
        /// <param name="postStaffSurvey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostStaffSurvey> postStaffSurvey)
        {
            if (postStaffSurvey == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in postStaffSurvey)
            {
                if (item.Attachment == null)
                    item.Attachment = "No Image";
            }
            var StaffSurvey = Mapper.Map<List<StaffSurvey>>(postStaffSurvey);
            await _StaffSurveyRepository.InsertEntities(StaffSurvey);
            
            return Ok();


        }
        /// <summary>
        /// Update StaffSurvey
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutStaffSurvey> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Entity = _dbContext.Set<StaffSurvey>();
            var filterEntity = Entity.Where(c => c.Reference == model.FirstOrDefault().Reference);
            foreach (StaffSurvey item in filterEntity)
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
                    var postEntity = Mapper.Map<StaffSurvey>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
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
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               StaffId = c.StaffId,
                                               OfficerToAct = c.OfficerToAct,
                                               Details = c.Details,
                                               HealthCareServicesSatisfaction = c.HealthCareServicesSatisfaction,
                                               CompanyManagement = c.CompanyManagement,
                                               Deadline = c.Deadline,
                                               AdequateTrainingReceived = c.AdequateTrainingReceived,
                                               AccessToPolicies = c.AccessToPolicies,
                                               AreaRequiringImprovements = c.AreaRequiringImprovements,
                                               WorkTeam = c.WorkTeam,
                                               SupportFromCompany = c.SupportFromCompany,
                                               WorkEnvironmentSuggestions = c.WorkEnvironmentSuggestions,
                                               URL = c.URL
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffSurvey);
        }
        #endregion
    }
}