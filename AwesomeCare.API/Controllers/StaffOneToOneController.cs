using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffOneToOne;
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
    public class StaffOneToOneController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffOneToOne> _StaffOneToOneRepository;
        
        public StaffOneToOneController(AwesomeCareDbContext dbContext, IGenericRepository<StaffOneToOne> StaffOneToOneRepository)
        {
            _StaffOneToOneRepository = StaffOneToOneRepository;
            _dbContext = dbContext;
        }
        #region StaffOneToOne
        /// <summary>
        /// Get All StaffOneToOne
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffOneToOne>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffOneToOneRepository.Table.ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create StaffOneToOne
        /// </summary>
        /// <param name="postStaffOneToOne"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffOneToOne postStaffOneToOne)
        {
            if (postStaffOneToOne == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            var StaffOneToOne = Mapper.Map<StaffOneToOne>(postStaffOneToOne);
            await _StaffOneToOneRepository.InsertEntity(StaffOneToOne);
            return Ok();
        }
        /// <summary>
        /// Update StaffOneToOne
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutStaffOneToOne model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StaffOneToOne = Mapper.Map<StaffOneToOne>(model);
            await _StaffOneToOneRepository.UpdateEntity(StaffOneToOne);
            return Ok();

        }

        
        /// <summary>
        /// Get StaffOneToOne by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffOneToOne), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getStaffOneToOne = await (from c in _StaffOneToOneRepository.Table
                                           where c.OneToOneId == id
                                           select new GetStaffOneToOne
                                           {
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               Deadline = c.Deadline,
                                               CurrentEventArea = c.CurrentEventArea,
                                               DecisionsReached = c.DecisionsReached,
                                               ImprovementRecorded = c.ImprovementRecorded,
                                               PreviousSupervision = c.PreviousSupervision,
                                               Purpose =c.Purpose,
                                               StaffConclusion = c.StaffConclusion,
                                               StaffId = c.StaffId,
                                               StaffImprovedInAreas = c.StaffImprovedInAreas,
                                               URL = c.URL
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffOneToOne);
        }
        #endregion
    }
}