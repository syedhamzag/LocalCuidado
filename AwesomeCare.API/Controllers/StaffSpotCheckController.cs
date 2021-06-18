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
        
        public StaffSpotCheckController(AwesomeCareDbContext dbContext, IGenericRepository<StaffSpotCheck> StaffSpotCheckRepository)
        {
            _StaffSpotCheckRepository = StaffSpotCheckRepository;
            _dbContext = dbContext;
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
            return Ok(getEntities.Distinct().ToList());
        }

        /// <summary>
        /// Get All StaffSpotCheck
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRef/{Reference}")]
        [ProducesResponseType(type: typeof(List<GetStaffSpotCheck>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRef(string Reference)
        {
            var getEntities = _StaffSpotCheckRepository.Table.Where(s => s.Reference == Reference).ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffSpotCheck
        /// </summary>
        /// <param name="postStaffSpotCheck"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostStaffSpotCheck> postStaffSpotCheck)
        {
            if (postStaffSpotCheck == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in postStaffSpotCheck)
            {
                if (item.Attachment == null)
                    item.Attachment = "No Image";
            }

            var StaffSpotCheck = Mapper.Map<List<StaffSpotCheck>>(postStaffSpotCheck);
            await _StaffSpotCheckRepository.InsertEntities(StaffSpotCheck);
            return Ok();
        }
        /// <summary>
        /// Update StaffSpotCheck
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutStaffSpotCheck> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Entity = _dbContext.Set<StaffSpotCheck>();
            var filterEntity = Entity.Where(c => c.Reference == model.FirstOrDefault().Reference);
            foreach (StaffSpotCheck item in filterEntity)
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
                    var postEntity = Mapper.Map<StaffSpotCheck>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
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
                                               OfficerToAct = c.OfficerToAct,
                                               StaffArriveOnTime = c.StaffArriveOnTime,
                                               StaffDressCode = c.StaffDressCode,
                                               StaffId = c.StaffId
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getStaffSpotCheck);
        }
        #endregion
    }
}