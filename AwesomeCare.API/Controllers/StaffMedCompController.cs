using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffMedComp;
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
    public class StaffMedCompController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffMedComp> _StaffMedCompRepository;
        
        public StaffMedCompController(AwesomeCareDbContext dbContext, IGenericRepository<StaffMedComp> StaffMedCompRepository)
        {
            _StaffMedCompRepository = StaffMedCompRepository;
            _dbContext = dbContext;
        }
        #region StaffMedComp
        /// <summary>
        /// Get All StaffMedComp
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffMedComp>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffMedCompRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffMedComp
        /// </summary>
        /// <param name="postStaffMedComp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffMedComp postStaffMedComp)
        {
            if (postStaffMedComp == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StaffMedComp = Mapper.Map<StaffMedComp>(postStaffMedComp);
            var newStaffMedComp = await _StaffMedCompRepository.InsertEntity(StaffMedComp);
            var getStaffMedComp = Mapper.Map<GetStaffMedComp>(newStaffMedComp);
            return Ok(getStaffMedComp);


        }
        /// <summary>
        /// Update StaffMedComp
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetStaffMedComp), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutStaffMedComp model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _StaffMedCompRepository.GetEntity(model.MedCompId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _StaffMedCompRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetStaffMedComp>(updateEntity);
            return Ok(getEntity);

        }
        /// <summary>
        /// Get StaffMedComp by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffMedComp), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getStaffMedComp = await (from c in _StaffMedCompRepository.Table
                                           where c.MedCompId == id
                                           select new GetStaffMedComp
                                           {
                                               ClientId = c.ClientId,
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               CarePlan = c.CarePlan,
                                               Details = c.Details,
                                               Deadline = c.Deadline,
                                               StaffId = c.StaffId,
                                               OfficerToAct = c.OfficerToAct,
                                               RateStaff = c.RateStaff,
                                               ReadingMedicalPrescriptions = c.ReadingMedicalPrescriptions,
                                               UnderstandingofMedication = c.UnderstandingofMedication,
                                               UnderstandingofRights = c.UnderstandingofRights,
                                               URL = c.URL
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffMedComp);
        }
        #endregion
    }
}