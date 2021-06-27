using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffSupervision;
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
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<SupervisionOfficerToAct> _officertoactRepository;
        private IGenericRepository<SupervisionWorkteam> _workteamRepository;

        public StaffSupervisionAppraisalController(AwesomeCareDbContext dbContext, IGenericRepository<StaffSupervisionAppraisal> StaffSupervisionAppraisalRepository,
            IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<SupervisionOfficerToAct> officertoactRepository, IGenericRepository<SupervisionWorkteam> workteamRepository)
        {
            _StaffSupervisionAppraisalRepository = StaffSupervisionAppraisalRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffRepository = staffRepository;
            _workteamRepository = workteamRepository;
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
            await _StaffSupervisionAppraisalRepository.InsertEntity(StaffSupervisionAppraisal);
            return Ok();
        }
        /// <summary>
        /// Update StaffSupervisionAppraisal
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffSupervisionAppraisal models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<SupervisionOfficerToAct>();
                var filterentity = entity.Where(c => c.StaffSupervisionAppraisalId == model.StaffSupervisionAppraisalId && c.StaffPersonalInfoId == model.StaffPersonalInfoId).ToList();
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
                var entity = _dbContext.Set<SupervisionWorkteam>();
                var filterentity = entity.Where(c => c.StaffSupervisionAppraisalId == model.StaffSupervisionAppraisalId && c.StaffPersonalInfoId == model.StaffPersonalInfoId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            var result = _dbContext.SaveChanges();
            var StaffSupervisionAppraisal = Mapper.Map<StaffSupervisionAppraisal>(models);
            await _StaffSupervisionAppraisalRepository.UpdateEntity(StaffSupervisionAppraisal);
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
                                               StaffSupervisionAppraisalId = c.StaffSupervisionAppraisalId,
                                               Reference = c.Reference,
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
                                               StaffAbility =c.StaffAbility,
                                               StaffComplaints = c.StaffComplaints,
                                               StaffDevelopment = c.StaffDevelopment,
                                               StaffId =c.StaffId,
                                               StaffRating = c.StaffRating,
                                               StaffSupportAreas = c.StaffSupportAreas,
                                               URL = c.URL,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.StaffSupervisionAppraisalId == c.StaffSupervisionAppraisalId
                                                               select new GetSupervisionOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               Workteam = (from com in _workteamRepository.Table
                                                           join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                           where com.StaffSupervisionAppraisalId == c.StaffSupervisionAppraisalId
                                                           select new GetSupervisionWorkteam
                                                           {
                                                               StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                               StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                           }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffSupervisionAppraisal);
        }
        #endregion
    }
}