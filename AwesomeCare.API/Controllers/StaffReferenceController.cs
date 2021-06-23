using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffReference;
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
    public class StaffReferenceController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffReference> _StaffReferenceRepository;
        
        public StaffReferenceController(AwesomeCareDbContext dbContext, IGenericRepository<StaffReference> StaffReferenceRepository)
        {
            _StaffReferenceRepository = StaffReferenceRepository;
            _dbContext = dbContext;
        }
        #region StaffReference
        /// <summary>
        /// Get All StaffReference
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffReference>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffReferenceRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffReference
        /// </summary>
        /// <param name="postStaffReference"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffReference postStaffReference)
        {
            if (postStaffReference == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            var StaffReference = Mapper.Map<StaffReference>(postStaffReference);
            await _StaffReferenceRepository.InsertEntity(StaffReference);
            return Ok();
        }
        /// <summary>
        /// Update StaffReference
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetStaffReference), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutStaffReference model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StaffReference = Mapper.Map<StaffReference>(model);
            await _StaffReferenceRepository.UpdateEntity(StaffReference);
            return Ok();

        }
        /// <summary>
        /// Get StaffReference by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffReference), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getStaffReference = await (from c in _StaffReferenceRepository.Table
                                           where c.StaffReferenceId == id
                                           select new GetStaffReference
                                           {
                                               StaffReferenceId = c.StaffReferenceId,
                                               Date = c.Date,
                                               Reference = c.Reference,
                                               Status = c.Status,
                                               Address = c.Address,
                                               ApplicantRole = c.ApplicantRole,
                                               Attachment = c.Attachment,
                                               Caring = c.Caring,
                                               Contact = c.Contact,
                                               DateofEmployement = c.DateofEmployement,
                                               DateofExit = c.DateofExit,
                                               Email = c.Email,
                                               Integrity = c.Integrity,
                                               Knowledgeable = c.Knowledgeable,
                                               PreviousExperience = c.PreviousExperience,
                                               RefreeName = c.RefreeName,
                                               RehireStaff = c.RehireStaff,
                                               Relationship = c.Relationship,
                                               StaffId = c.StaffId,
                                               TeamWork = c.TeamWork,
                                               WorkUnderPressure = c.WorkUnderPressure
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffReference);
        }
        #endregion
    }
}