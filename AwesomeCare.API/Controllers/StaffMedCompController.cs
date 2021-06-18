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
    [AllowAnonymous]
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
            return Ok(getEntities.Distinct().ToList());
        }

        /// <summary>
        /// Get All StaffMedComp
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRef/{Reference}")]
        [ProducesResponseType(type: typeof(List<GetStaffMedComp>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByRef(string Reference)
        {
            var getEntities = _StaffMedCompRepository.Table.Where(s => s.Reference == Reference).ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create StaffMedComp
        /// </summary>
        /// <param name="postStaffMedComp"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostStaffMedComp> postStaffMedComp)
        {
            if (postStaffMedComp == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var item in postStaffMedComp)
            {
                if (item.Attachment == null)
                    item.Attachment = "No Image";
            }

            var StaffMedComp = Mapper.Map<List<StaffMedComp>>(postStaffMedComp);
            await _StaffMedCompRepository.InsertEntities(StaffMedComp);
            return Ok();
        }
        /// <summary>
        /// Update StaffMedComp
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PutStaffMedComp> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Entity = _dbContext.Set<StaffMedComp>();
            var filterEntity = Entity.Where(c => c.Reference == model.FirstOrDefault().Reference);
            foreach (StaffMedComp item in filterEntity)
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
                    var postEntity = Mapper.Map<StaffMedComp>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                }
            }
            var result = _dbContext.SaveChanges();
            return Ok();

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