using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffSpotCheck;
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
    public class StaffSpotCheckController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffSpotCheck> _StaffSpotCheckRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<SpotCheckOfficerToAct> _officertoactRepository;

        public StaffSpotCheckController(AwesomeCareDbContext dbContext, IGenericRepository<StaffSpotCheck> StaffSpotCheckRepository,
            IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<SpotCheckOfficerToAct> officertoactRepository)
        {
            _StaffSpotCheckRepository = StaffSpotCheckRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffRepository = staffRepository;
        }
        #region StaffSpotCheck
        /// <summary>
        /// Get All StaffSpotCheck
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffSpotCheck>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffSpotCheckRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffSpotCheck
        /// </summary>
        /// <param name="postStaffSpotCheck"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffSpotCheck postStaffSpotCheck)
        {
            if (postStaffSpotCheck == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var StaffSpotCheck = Mapper.Map<StaffSpotCheck>(postStaffSpotCheck);
            await _StaffSpotCheckRepository.InsertEntity(StaffSpotCheck);
            return Ok();
        }
        /// <summary>
        /// Update StaffSpotCheck
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffSpotCheck models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<SpotCheckOfficerToAct>();
                var filterentity = entity.Where(c => c.SpotCheckId == model.SpotCheckId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            var result = _dbContext.SaveChanges();
            var StaffSpotCheck = Mapper.Map<StaffSpotCheck>(models);
            await _StaffSpotCheckRepository.UpdateEntity(StaffSpotCheck);
            return Ok();

        }

        
        /// <summary>
        /// Get StaffSpotCheck by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffSpotCheck), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getStaffSpotCheck = await (from c in _StaffSpotCheckRepository.Table
                                           where c.SpotCheckId == id
                                           select new GetStaffSpotCheck
                                           {
                                               SpotCheckId = c.SpotCheckId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               URL = c.URL,
                                               AreaComments = c.AreaComments,
                                               Deadline = c.Deadline,
                                               Details = c.Details,
                                               StaffArriveOnTime = c.StaffArriveOnTime,
                                               StaffDressCode = c.StaffDressCode,
                                               StaffId = c.StaffId,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.SpotCheckId == c.SpotCheckId
                                                               select new GetSpotCheckOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffSpotCheck);
        }
        #endregion
    }
}