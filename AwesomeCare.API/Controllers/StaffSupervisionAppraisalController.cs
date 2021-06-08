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
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffSupervisionAppraisal
        /// </summary>
        /// <param name="postStaffSupervisionAppraisal"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffSupervisionAppraisal postStaffSupervisionAppraisal)
        {
            if (postStaffSupervisionAppraisal == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StaffSupervisionAppraisal = Mapper.Map<StaffSupervisionAppraisal>(postStaffSupervisionAppraisal);
            var newStaffSupervisionAppraisal = await _StaffSupervisionAppraisalRepository.InsertEntity(StaffSupervisionAppraisal);
            var getStaffSupervisionAppraisal = Mapper.Map<GetStaffSupervisionAppraisal>(newStaffSupervisionAppraisal);
            return Ok(getStaffSupervisionAppraisal);


        }
        /// <summary>
        /// Update StaffSupervisionAppraisal
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetStaffSupervisionAppraisal), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutStaffSupervisionAppraisal model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _StaffSupervisionAppraisalRepository.GetEntity(model.StaffSupervisionAppraisalId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _StaffSupervisionAppraisalRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetStaffSupervisionAppraisal>(updateEntity);
            return Ok(getEntity);

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