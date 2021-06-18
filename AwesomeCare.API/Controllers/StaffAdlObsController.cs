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
    [AllowAnonymous]
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
            return Ok(getEntities.Distinct().ToList());
        }

        /// <summary>
        /// Get All ClientLogAudit
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRef/{Reference}")]
        [ProducesResponseType(type: typeof(List<GetStaffAdlObs>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRef(string Reference)
        {
            var getEntities = _StaffAdlObsRepository.Table.Where(s => s.Reference == Reference).ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffAdlObs
        /// </summary>
        /// <param name="postStaffAdlObs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostStaffAdlObs> postStaffAdlObs)
        {
            if (postStaffAdlObs == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in postStaffAdlObs)
            {
                if (item.Attachment == null)
                    item.Attachment = "No Image";
            }

            var StaffAdlObs = Mapper.Map<List<StaffAdlObs>>(postStaffAdlObs);
            await _StaffAdlObsRepository.InsertEntities(StaffAdlObs);
            return Ok();
        }
        /// <summary>
        /// Update StaffAdlObs
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutStaffAdlObs> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Entity = _dbContext.Set<StaffAdlObs>();
            var filterEntity = Entity.Where(c => c.Reference == model.FirstOrDefault().Reference);
            foreach (StaffAdlObs item in filterEntity)
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
                    var postEntity = Mapper.Map<StaffAdlObs>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
            return Ok();

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