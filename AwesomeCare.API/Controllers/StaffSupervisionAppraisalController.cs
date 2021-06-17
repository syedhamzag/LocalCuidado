using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffSupervisionAppraisal;
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
    public class StaffSupervisionAppraisalController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffSupervisionAppraisal> _StaffSupervisionAppraisalRepository;
        
        public StaffSupervisionAppraisalController(AwesomeCareDbContext dbContext, IGenericRepository<StaffSupervisionAppraisal> StaffSupervisionAppraisalRepository)
        {
            _StaffSupervisionAppraisalRepository = StaffSupervisionAppraisalRepository;
            _dbContext = dbContext;
        }
        #region StaffSupervisionAppraisal
        /// <summary>
        /// Get All StaffSupervisionAppraisal
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffSupervisionAppraisal>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffSupervisionAppraisalRepository.Table.ToList();
            return Ok(getEntities.Distinct().ToList());
        }
        /// <summary>
        /// Get All Supervision Appraisal
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRef/{Reference}")]
        [ProducesResponseType(type: typeof(List<GetStaffSupervisionAppraisal>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRef(string Reference)
        {
            var getEntities = _StaffSupervisionAppraisalRepository.Table.Where(s => s.Reference == Reference).ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffSupervisionAppraisal
        /// </summary>
        /// <param name="postStaffSupervisionAppraisal"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostStaffSupervisionAppraisal> postStaffSupervisionAppraisal)
        {
            if (postStaffSupervisionAppraisal == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in postStaffSupervisionAppraisal)
            {
                if (item.Attachment == null)
                    item.Attachment = "No Image";
            }
            var StaffSupervisionAppraisal = Mapper.Map<List<StaffSupervisionAppraisal>>(postStaffSupervisionAppraisal);
            await _StaffSupervisionAppraisalRepository.InsertEntities(StaffSupervisionAppraisal);
            return Ok();


        }
        /// <summary>
        /// Update StaffSupervisionAppraisal
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutStaffSupervisionAppraisal> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Entity = _dbContext.Set<StaffSupervisionAppraisal>();
            var filterEntity = Entity.Where(c => c.Reference == model.FirstOrDefault().Reference);
            foreach (StaffSupervisionAppraisal item in filterEntity)
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
                    var postEntity = Mapper.Map<StaffSupervisionAppraisal>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
            return Ok();

        }
        /// <summary>
        /// Get StaffSupervisionAppraisal by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffSupervisionAppraisal), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getStaffSupervisionAppraisal = await (from c in _StaffSupervisionAppraisalRepository.Table
                                           where c.StaffSupervisionAppraisalId == id
                                           select new GetStaffSupervisionAppraisal
                                           {
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               Deadline = c.Deadline,
                                               ProfessionalDevelopment = c.ProfessionalDevelopment,
                                               CondourAndWhistleBlowing = c.CondourAndWhistleBlowing,
                                               Details = c.Details,
                                               FiveStarRating =c.FiveStarRating,
                                               NoAbilityToSupport =c.NoAbilityToSupport,
                                               NoCondourAndWhistleBlowing = c.NoCondourAndWhistleBlowing,
                                               OfficerToAct =c.OfficerToAct,
                                               StaffAbility =c.StaffAbility,
                                               StaffComplaints = c.StaffComplaints,
                                               StaffDevelopment = c.StaffDevelopment,
                                               StaffId =c.StaffId,
                                               StaffRating = c.StaffRating,
                                               StaffSupportAreas = c.StaffSupportAreas,
                                               WorkTeam = c.WorkTeam,
                                               URL = c.URL
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffSupervisionAppraisal);
        }
        #endregion
    }
}