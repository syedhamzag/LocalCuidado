using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffAdlObs;
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
    public class StaffAdlObsController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<StaffAdlObs> _StaffAdlObsRepository;
        
        public StaffAdlObsController(AwesomeCareDbContext dbContext, IGenericRepository<StaffAdlObs> StaffAdlObsRepository)
        {
            _StaffAdlObsRepository = StaffAdlObsRepository;
            _dbContext = dbContext;
        }
        #region StaffAdlObs
        /// <summary>
        /// Get All StaffAdlObs
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffAdlObs>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffAdlObsRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffAdlObs
        /// </summary>
        /// <param name="postStaffAdlObs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffAdlObs postStaffAdlObs)
        {
            if (postStaffAdlObs == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var StaffAdlObs = Mapper.Map<StaffAdlObs>(postStaffAdlObs);
            var newStaffAdlObs = await _StaffAdlObsRepository.InsertEntity(StaffAdlObs);
            var getStaffAdlObs = Mapper.Map<GetStaffAdlObs>(newStaffAdlObs);
            return Ok(getStaffAdlObs);


        }
        /// <summary>
        /// Update StaffAdlObs
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetStaffAdlObs), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutStaffAdlObs model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _StaffAdlObsRepository.GetEntity(model.ObservationID);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _StaffAdlObsRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetStaffAdlObs>(updateEntity);
            return Ok(getEntity);

        }
        /// <summary>
        /// Get StaffAdlObs by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffAdlObs), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getStaffAdlObs = await (from c in _StaffAdlObsRepository.Table
                                           where c.ObservationID == id
                                           select new GetStaffAdlObs
                                           {
                                               ClientId = c.ClientId,
                                               ActionRequired = c.ActionRequired,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextCheckDate = c.NextCheckDate,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               Deadline = c.Deadline,
                                               Comments = c.Comments,
                                               Details = c.Details,
                                               FivePrinciples = c.FivePrinciples,
                                               OfficerToAct = c.OfficerToAct,
                                               StaffId = c.StaffId,
                                               UnderstandingofControl = c.UnderstandingofControl,
                                               UnderstandingofEquipment = c.UnderstandingofEquipment,
                                               UnderstandingofService = c.UnderstandingofService,
                                               URL = c.URL
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffAdlObs);
        }
        #endregion
    }
}